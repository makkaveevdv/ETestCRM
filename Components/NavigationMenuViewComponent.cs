using ETestCRM.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETestCRM.Components
{
    public class NavigationMenuViewComponent : ViewComponent 
    {
        private UserManager<AppUser> userManager;
        IGetUserOnline getUserOnline;
        public NavigationMenuViewComponent(UserManager<AppUser> usrMgr, IGetUserOnline getUsrOnline)
        {
            userManager = usrMgr;
            getUserOnline = getUsrOnline;
        }
        public IViewComponentResult Invoke()
        {
            string userOnlineId = getUserOnline.GetUserOnlineId().Result;
            AppUser userOnlineNow = getUserOnline.GetAuthorizedUser().Result;
            ViewBag.UserOnline = userOnlineNow;
            ViewBag.IsAdmin = userManager.IsInRoleAsync(userOnlineNow, "Superadmin").Result;
            return View(userManager.Users.Where(x => x.ManagerId == userOnlineId).OrderBy(x => x));
        }
    }
}
