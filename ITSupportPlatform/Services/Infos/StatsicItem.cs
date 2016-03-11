using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSupportPlatform.Services.Infos
{
    public class StatsicItem
    {

        public string InstanceID { get; set; }
        public string FormID { get; set; }
        public string CName { get; set; }
        public string QName { get; set; }
        public string CreateByUserName { get; set; }
        public string CreateDeptName { get; set; }
        public string ContentTxt { get; set; }
        public string ApproveByUserName { get; set; }
        public string ApproveOption { get; set; }
        public string SumitTime { get; set; }
        public string FinishedTime { get; set; }
        public string WFStatus { get; set; }

    }
}