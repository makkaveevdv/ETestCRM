using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETestCRM.Models
{
    public class GetUserOnline : IGetUserOnline
    {
        private UserManager<AppUser> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetUserOnline(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> usrMgr)
        {
            _httpContextAccessor = httpContextAccessor;
            userManager = usrMgr;
        }

        public async Task<string> GetUserOnlineId()
        {
            AppUser userOnline = await userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name); 
            return userOnline.Id;
        }

        public async Task<AppUser> GetAuthorizedUser()
        {
            AppUser userOnline = await userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            return userOnline;
        }
    }
}
