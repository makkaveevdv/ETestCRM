using ETestCRM.Models;
using ETestCRM.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETestCRM.Controllers
{
    [Authorize]
    public class MyOrdersController : Controller
    {
        private IOrderRepository repository;
        private UserManager<AppUser> userManager;
        private Task<AppUser> CurrentUser => userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        public string myId;

        public int PageSize = 10;
        public MyOrdersController(IOrderRepository repo, UserManager<AppUser> userMgr)
        {
            repository = repo;
            userManager = userMgr;
        }

        [Authorize]
        public async Task<IActionResult> Index(string sortTime, int orderPage = 1)
        {
            DateTime dateSort = sortTime switch
            {
                "all" => dateSort = DateTime.MaxValue,
                "today" => dateSort = DateTime.Today,
                "toweek" => dateSort = DateTime.Today.AddDays(7),
                _ => dateSort = DateTime.MaxValue
            };

            AppUser userOnline = await CurrentUser;
            myId = userOnline.Id;

            OrderListViewModel orderListViewModel = new OrderListViewModel
            {
                Orders = repository.Orders
               .Where(o => o.RespUserId == myId)
               .Where(p => p.EndDate <= dateSort)
               .OrderBy(s => s.CreationDate)
               .Skip((orderPage - 1) * PageSize)
               .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = orderPage,
                    ItemsPerPage = PageSize,
                    CurrentTime = sortTime,
                    TotalItems = repository.Orders.Where(o => o.RespUserId == myId).Where(p => p.EndDate <= dateSort).Count()
                }
            };

            return View(orderListViewModel);

        }

        [Authorize]
        public IActionResult UpdateMyOrder(long orderId)
        {
            return View(repository.Orders.SingleOrDefault(o => o.OrderId == orderId));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateMyOrder(int orderId, string status)
        {
            Order order = new Order
            {
                OrderId = orderId,                
                UpdateDate = DateTime.Today,
                Status = status
            };
            if (ModelState.IsValid)
            {
                if (repository.UpdateOrderStatus(order))
                {
                    TempData["message"] = "Статус задачи обновлен";
                } else { TempData["message"] = "Вы не изменили статус задачи"; }
                return RedirectToAction("Index", "MyOrders");
            }
            else
            {
                return View(order);
            }
        }

    }
}
