using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETestCRM.Models
{
    public interface IGetUserOnline
    {
        Task<string> GetUserOnlineId();
        Task<AppUser> GetAuthorizedUser();
    }
}
