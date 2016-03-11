using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ITSupportPlatform.Services
{
    public class ExportHelper
    {
        public static void ExportToWeb<T>(List<T> dataInfo, string strFileName, string sheetName = "S1", List<ExportInfo> assistInfo = null)
        {

            HttpContext curContext = HttpContext.Current;

            // 设置编码和附件格式   
            //curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            //curContext.Response.Charset = "";
            //curContext.Response.AppendHeader("Content-Disposition",
            //    "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));
            curContext.Response.Clear();
            //curContext.Response.BinaryWrite(Export<T>(dataInfo, sheetName, assistInfo).GetBuffer());
            //curContext.Response.End();

            if (curContext.Request.Browser.Browser == "IE")
                strFileName = HttpUtility.UrlEncode(strFileName);
            curContext.Response.AddHeader("Content-Disposition", "attachment;fileName=" + strFileName);
            curContext.Response.BinaryWrite(Export<T>(dataInfo, sheetName, assistInfo).GetBuffer());

        }

        public static MemoryStream Export<T>(List<T> dataInfo, string sheetName = "S1", List<ExportInfo> assistInfo = null)
        {
            IWorkbook workbook = new XSSFWorkbook();
            
            ISheet sheet = workbook.CreateSheet(sheetName);
            Type t = typeof(T);
            if (assistInfo == null)
            {
                assistInfo = new List<ExportInfo>();
                PropertyInfo[] pInfos = t.GetProperties();
                foreach (var item in pInfos)
                {
                    assistInfo.Add(new ExportInfo() { ColumnName = item.Name, PropName = item.Name });
                }
            }
            int rowIndex = 0;
            IRow headerRow = sheet.CreateRow(rowIndex++);
            int colIndex = 0;
            foreach (var colInfo in assistInfo)
            {
                headerRow.CreateCell(colIndex++).SetCellValue(colInfo.ColumnName);
            }

            foreach (var dataItem in dataInfo)
            {
                IRow row = sheet.CreateRow(rowIndex++);
                colIndex = 0;
                foreach (var colInfo in assistInfo)
                {
                    PropertyInfo pInfo = t.GetProperty(colInfo.PropName);
                    object value = pInfo.GetValue(dataItem, null);
                    if (value == null || string.IsNullOrEmpty(value.ToString()))
                    {
                        row.CreateCell(colIndex++).SetCellValue("");
                        continue;
                    }

                    switch (pInfo.PropertyType.Name)
                    {
                        case "String"://字符串类型   
                            row.CreateCell(colIndex++).SetCellValue(value.ToString());
                            break;
                        case "System.DateTime"://日期类型   
                            DateTime dateV;
                            DateTime.TryParse(value.ToString(), out dateV);
                            row.CreateCell(colIndex++).SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型   
                            bool boolV = false;
                            bool.TryParse(value.ToString(), out boolV);
                            row.CreateCell(colIndex++).SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型   
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(value.ToString(), out intV);
                            row.CreateCell(colIndex++).SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型   
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(value.ToString(), out doubV);
                            row.CreateCell(colIndex++).SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理   
                            row.CreateCell(colIndex++).SetCellValue("");
                            break;
                        default:
                            row.CreateCell(colIndex++).SetCellValue("");
                            break;
                    }
                }
            }
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            //using (FileStream fs = new FileStream("D:\\222.xlsx", FileMode.Create, FileAccess.Write))
            //{
            //    byte[] data = ms.ToArray();
            //    fs.Write(data, 0, data.Length);
            //    fs.Flush();
            //}
            
            
            return ms;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    workbook.Write(ms);
            //   // ms.Flush();
            //    //ms.Position = 0;
            //    return ms;
            //}
        }


        public static void ExportToFile(DataSet dataSet, string fileFullPath)
        {
            List<DataTable> dts = new List<DataTable>();
            foreach (DataTable dt in dataSet.Tables) dts.Add(dt);
            ExportToFile(dts, fileFullPath);
        }
        public static void ExportToFile(DataTable dataTable, string fileFullPath)
        {
            List<DataTable> dts = new List<DataTable>();
            dts.Add(dataTable);
            ExportToFile(dts, fileFullPath);
        }

        public static void ExportToFile(IEnumerable<DataTable> dataTables, string fileFullPath)
        {
            IWorkbook workbook = new XSSFWorkbook();
            int i = 0;
            foreach (DataTable dt in dataTables)
            {
                string sheetName = string.IsNullOrEmpty(dt.TableName)
                    ? "Sheet " + (++i).ToString()
                    : dt.TableName;
                ISheet sheet = workbook.CreateSheet(sheetName);

                IRow headerRow = sheet.CreateRow(0);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string columnName = string.IsNullOrEmpty(dt.Columns[j].ColumnName)
                        ? "Column " + j.ToString()
                        : dt.Columns[j].ColumnName;
                    headerRow.CreateCell(j).SetCellValue(columnName);
                }

                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    DataRow dr = dt.Rows[a];
                    IRow row = sheet.CreateRow(a + 1);
                    for (int b = 0; b < dt.Columns.Count; b++)
                    {
                        row.CreateCell(b).SetCellValue(dr[b] != DBNull.Value ? dr[b].ToString() : string.Empty);
                    }
                }
            }

            using (FileStream fs = File.Create(fileFullPath))
            {
                workbook.Write(fs);
            }
        }

        public static List<DataTable> GetDataTablesFrom(string xlsxFile)
        {
            if (!File.Exists(xlsxFile))
                throw new FileNotFoundException("文件不存在");

            List<DataTable> result = new List<DataTable>();
            Stream stream = new MemoryStream(File.ReadAllBytes(xlsxFile));
            IWorkbook workbook = new XSSFWorkbook(stream);
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                DataTable dt = new DataTable();
                ISheet sheet = workbook.GetSheetAt(i);
                IRow headerRow = sheet.GetRow(0);

                int cellCount = headerRow.LastCellNum;
                for (int j = headerRow.FirstCellNum; j < cellCount; j++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(j).StringCellValue);
                    dt.Columns.Add(column);
                }

                int rowCount = sheet.LastRowNum;
                for (int a = (sheet.FirstRowNum + 1); a < rowCount; a++)
                {
                    IRow row = sheet.GetRow(a);
                    if (row == null) continue;

                    DataRow dr = dt.NewRow();
                    for (int b = row.FirstCellNum; b < cellCount; b++)
                    {
                        if (row.GetCell(b) == null) continue;
                        dr[b] = row.GetCell(b).ToString();
                    }

                    dt.Rows.Add(dr);
                }
                result.Add(dt);
            }
            stream.Close();

            return result;
        }

        public class ExportInfo
        {
            public string PropName { get; set; }
            public string ColumnName { get; set; }
        }
    }
}