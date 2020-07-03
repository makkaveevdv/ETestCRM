using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETestCRM.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;
        private UserManager<AppUser> userManager;
        public EFOrderRepository(ApplicationDbContext ctx, UserManager<AppUser> usrMgr) { context = ctx; userManager = usrMgr; }
        public IQueryable<Order> Orders => context.Orders;

        public void SaveOrder(Order order)
        {
            if(order.OrderId == 0) { context.Orders.Add(order); } else
            {
                Order dbEntry = context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
                
                if (dbEntry != null)
                {
                    dbEntry.Name = order.Name;
                    dbEntry.Description = order.Description;
                    dbEntry.EndDate = order.EndDate;
                    dbEntry.UpdateDate = order.UpdateDate;
                    if (dbEntry.CreationDate == null) dbEntry.CreationDate = order.CreationDate;
                    dbEntry.Priority = order.Priority;
                    dbEntry.Status = order.Status;
                    if (dbEntry.CreatorId != null) { dbEntry.CreatorId = order.CreatorId; dbEntry.CreatorFullName = order.CreatorFullName; }
                    dbEntry.RespUserId = order.RespUserId;
                    dbEntry.RespUserFullName = order.RespUserFullName;
                }
            }
            context.SaveChanges();
        }

        public bool UpdateOrderStatus(Order order)
        {
            if (order.OrderId != 0)
            {
                Order dbEntry = context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
                if (dbEntry != null)
                {
                    if (dbEntry.Status != order.Status)
                    {
                        dbEntry.UpdateDate = order.UpdateDate;
                        dbEntry.Status = order.Status;
                        context.SaveChanges();
                        return true;
                    }                    
                }                
            }
            return false;            
        }

        public Order DeleteOrder(int orderId)
        {
            Order dbEntry = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (dbEntry != null)
            {
                context.Orders.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
