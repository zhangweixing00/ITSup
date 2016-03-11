using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITSupportPlatform.Services;
using ITSupportPlatform.Services.Infos;

namespace ITSupportPlatform.Pages
{
    public partial class Statsis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbStartTime.Text = DateTime.Now.AddDays(-7).ToShortDateString();
                tbEndTime.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void LoadData()
        {
            // GVList.DataSource=
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            List<StatsicItem> itemInfos = new ITSupportHelper().GetNotifyInfos(tbStartUser.Text, tbEndUser.Text,
            tbStartTime.Text, tbEndTime.Text, tbQuestion.Text);
            foreach (var item in itemInfos)
            {
                item.WFStatus = item.WFStatus == "1" ? "进行中":"结束";
            }
            var assistObj = new List<ExportHelper.ExportInfo>();

            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "FormID", ColumnName = "编号" });
            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "CName", ColumnName = "系统名称" });
            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "QName", ColumnName = "问题类型" });
            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "CreateByUserName", ColumnName = "发起人" });
            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "SumitTime", ColumnName = "系统名称" });
            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "CreateDeptName", ColumnName = "问题类型" });
            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "ContentTxt", ColumnName = "问题描述" });
            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "SumitTime", ColumnName = "发起时间" });
            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "CreateDeptName", ColumnName = "发起部门" });
            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "WFStatus", ColumnName = "状态" });
            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "ApproveByUserName", ColumnName = "处理人" });
            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "ApproveOption", ColumnName = "处理意见" });
            assistObj.Add(new ExportHelper.ExportInfo() { PropName = "FinishedTime", ColumnName = "完成时间" });


        ExportHelper.ExportToWeb<StatsicItem>(itemInfos,DateTime.Now.ToFileTime().ToString()+".xlsx","S1",assistObj);
        }
    }
}