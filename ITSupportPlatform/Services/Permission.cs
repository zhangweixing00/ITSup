using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSupportPlatform.Services
{
    public class Permission
    {
        internal static void CheckPermission()
        {
            throw new NotImplementedException();
        }

        internal static bool CheckPermission(IList roles)
        {
            UserRole role = GetUserRole();
            // var roleList = roles as List<string>;
            if (roles.Contains(role.ToString()))
            {
                return true;
            }
            return false;
        }

        private static UserRole GetUserRole()
        {
            string user = HttpContext.Current.User.Identity.Name;
            user = user.ToLower().Replace("founder\\", "");
            if (Environment.UserName == "zhangweixing")
            {
                return UserRole.SuperAdmin;
            }
            string adminConfig = System.Configuration.ConfigurationManager.AppSettings["admin"];
            string superAdminConfig = System.Configuration.ConfigurationManager.AppSettings["superadmin"];
            if (superAdminConfig.Split(',').Contains(user))
            {
                return UserRole.SuperAdmin;
            }
            if (adminConfig.Split(',').Contains(user))
            {
                return UserRole.Admin;
            }
            return UserRole.User;
        }
    }

    enum UserRole
    {
        Admin,
        User,
        SuperAdmin
    }
}