using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITSupportPlatform.Services;

namespace ITSupportPlatform.Controls
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string pageName = Request.Url.Segments[Request.Url.Segments.Length - 1].ToLower();
                StringBuilder menuList = new StringBuilder();
                string menuFormat = "<li role='presentation' {2}><a href='{0}'>{1}</a></li>";
                SiteMapNodeCollection nodes = this.SiteMapDataSource1.Provider.RootNode.ChildNodes;
                foreach (SiteMapNode subNode in nodes)
                {
                    if (Permission.CheckPermission(subNode.Roles))
                    {
                        menuList.AppendFormat(menuFormat, subNode.Url, subNode.Title
                            , subNode.Url.ToLower().EndsWith(pageName) ? "class='active'" : "");
                    }
                    // subNode.r
                }
                MenuList.InnerHtml = menuList.ToString();
            }
        }
    }
}