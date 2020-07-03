using ETestCRM.Models;
using ETestCRM.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETestCRM.Controllers
{
    [Authorize]
    public class MyTeamOrdersController : Controller
    {
        private IOrderRepository repository;
        private UserManager<AppUser> userManager;
        private Task<AppUser> CurrentUser => userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        public AppUser currentRespUser;

        public int PageSize = 10;
        public MyTeamOrdersController(IOrderRepository repo, UserManager<AppUser> userMgr)
        {
            repository = repo;
            userManager = userMgr;
        }
        [Authorize]
        public async Task<IActionResult> Index(string respuser, string sortTime, int orderPage = 1)
        {
            DateTime dateSort = sortTime switch
            {
                "all" => dateSort = DateTime.MaxValue,
                "today" => dateSort = DateTime.Today,
                "toweek" => dateSort = DateTime.Today.AddDays(7),
                _ => dateSort = DateTime.MaxValue
            };

            AppUser userOnline = await CurrentUser;

            if (respuser != null)
            {
                currentRespUser = await userManager.FindByNameAsync(respuser);
            }

            OrderListViewModel orderListViewModel = new OrderListViewModel
            {
                Orders = repository.Orders
               .Where(o => o.CreatorId == userOnline.Id)
               .Where(or => respuser == null || or.RespUserId == currentRespUser.Id)
               .Where(p => p.EndDate <= dateSort)
               .OrderBy(s => s.CreationDate)
               .Skip((orderPage - 1) * PageSize)
               .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = orderPage,
                    ItemsPerPage = PageSize,
                    CurrentRespUser = respuser, 
                    CurrentTime = sortTime,                    
                    TotalItems = respuser == null ? repository.Orders
                   .Where(o => o.CreatorId == userOnline.Id)
                   .Where(p => p.EndDate <= dateSort).Count() : 
                   repository.Orders
                   .Where(o => o.CreatorId == userOnline.Id)
                   .Where(or => respuser == null || or.RespUserId == currentRespUser.Id)
                   .Where(p => p.EndDate <= dateSort).Count()
                }
            };
            ViewBag.SelectedRespUser = respuser;
            return View(orderListViewModel);
        }

        [Authorize]
        public async Task<IActionResult> EditOrder(long orderId)
        {
            AppUser userOnline = await CurrentUser;
            ViewBag.ActionTitle = "Редактирование задачи";
            ViewBag.MyTeam = new SelectList(userManager.Users.Where(u => u.ManagerId == userOnline.Id).ToList(), "Id");
            return View("EditOrder", repository.Orders.SingleOrDefault(o => o.OrderId == orderId));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditOrder(int orderId, string name, string description, DateTime enddate,
            string priority, string status, string respperson)
        {
            AppUser userOnline = await CurrentUser;
            Order order = new Order
            {
                OrderId = orderId,
                Name = name,
                Description = description,
                EndDate = enddate,
                CreationDate = DateTime.Today,
                UpdateDate = DateTime.Today,
                Priority = priority,
                Status = status,
                CreatorId = userOnline.Id,
                CreatorFullName = userOnline.FullName,
                RespUserId = respperson != null ? (userManager.Users.FirstOrDefault(c => c.UserName == respperson)).Id : null,
                RespUserFullName = respperson != null ? (userManager.Users.FirstOrDefault(c => c.UserName == respperson)).FullName : null
            };

            if (ModelState.IsValid)
            {
                repository.SaveOrder(order);
                TempData["message"] = "Задача сохранена";
                return RedirectToAction("Index", "MyTeamOrders");
            }
            else
            {
                return View("EditOrder", order);
            }
        }
        public async Task <ViewResult> CreateOrder() 
        {
            AppUser userOnline = await CurrentUser;
            ViewBag.ActionTitle = "Создание новой задачи";
            ViewBag.MyTeam = new SelectList(userManager.Users.Where(u => u.ManagerId == userOnline.Id).ToList(), "Id");
            ViewBag.userOnline = userOnline;
            return View("EditOrder", new Order());
        }

        [HttpPost]
        public IActionResult DeleteOrder(int orderId)
        {
            Order deletedOrder = repository.DeleteOrder(orderId);
            if (deletedOrder != null)
            {
                TempData["message"] = "Задача удалена";
            }

            return RedirectToAction("Index", "MyTeamOrders");
        }
    }
}
