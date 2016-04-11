using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSupportPlatform.Services.Infos;

namespace ITSupportPlatform.Services
{
    public class ITSupportHelper
    {
        public List<Infos.FourDXDetailItem> GetDetailStatsisInfos(string startTime, string endTime,string user)
        {
            var infos = ZLX.Framework.ToolKit.DP_Static.CustomProcess.GetListByProc<Infos.FourDXDetailItem>("Biz.OA_ITSupport_Manage_4DXDetailStatsis", new object[] { startTime,endTime,user });
            return infos;
        }
        public List<Infos.FourDXItem> GetStatsisInfos(string startTime, string endTime)
        {
            var infos = ZLX.Framework.ToolKit.DP_Static.CustomProcess.GetListByProc<Infos.FourDXItem>("Biz.OA_ITsupport_Statsis_UnProcess", new object[] { startTime,endTime });
            return infos;
        }


        public List<Infos.StatsicItem> GetNotifyInfos(string startUser, string endUser,
            string startTime, string endTime, string question)
        {
            var infos = ZLX.Framework.ToolKit.DP_Static.CustomProcess.GetListByProc<Infos.StatsicItem>("Biz.OA_ITSupport_Manage_Statsis", new object[] { });
            IEnumerable<Infos.StatsicItem> filterInfos = infos;

            //if (!string.IsNullOrWhiteSpace(startUser))
            //{
            //    filterInfos = filterInfos.Where(x => x.CreateByUserName.Contains(startUser));
            //}
            filterInfos = filterInfos.Where(x => string.IsNullOrWhiteSpace(startUser) || x.CreateByUserName.Contains(startUser));
            filterInfos = filterInfos.Where(x => string.IsNullOrWhiteSpace(endUser) || x.ApproveByUserName.Contains(endUser));
            filterInfos = filterInfos.Where(x => string.IsNullOrWhiteSpace(question) || x.ContentTxt.Contains(question));

            DateTime dt = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(startTime)&&DateTime.TryParse(startTime,out dt))
            {
                filterInfos = filterInfos.Where(x => DateTime.Parse(x.SumitTime)>=dt);
            }
            DateTime dt2 = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(endTime) &&DateTime.TryParse(endTime, out dt2))
            {
                filterInfos = filterInfos.Where(x => DateTime.Parse(x.SumitTime)<= dt2);
            }
            return filterInfos.ToList();
        }

        internal List<TimeRateInfo> GetTimeRateInfos(string startTime, string endTime)
        {
            var infos = ZLX.Framework.ToolKit.DP_Static.CustomProcess.GetListByProc<Infos.TimeRateInfo>("Biz.OA_ITSupport_Manage_TimeRate", new object[] { startTime, endTime });
            return infos;
        }
    }
}