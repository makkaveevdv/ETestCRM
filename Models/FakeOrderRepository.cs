using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETestCRM.Models
{
    public class FakeOrderRepository /*: IOrderRepository*/
    {
        public IQueryable<Order> Orders => new List<Order>
        {
            new Order {
                Name = "Разработка коммерчекого предложения",
                Description = "Разработать коммерческое предложение для компании Hoff о новых условиях сотрудничества",
                EndDate = new DateTime(2020, 06, 30),
                CreationDate = new DateTime(2020, 06, 18)
            },
            new Order {
                Name = "Разработка методических указаний",
                Description = "Разработать методические указания для специалистов компании, включающие рекомендации по использованию опции Каршеринг",
                EndDate = new DateTime(2020, 06, 29),
                CreationDate = new DateTime(2020, 06, 18)
            },
            new Order {
                Name = "Подготовка квартального отчета",
                Description = "Подготовить квартальный отчет за апрель, май, июнь 2020 года с детализацией доходов и расходов компании в рамках продукта СКОР",
                EndDate = new DateTime(2020, 07, 10),
                CreationDate = new DateTime(2020, 06, 18)
            },
            new Order {
                Name = "Разработка проекта агентского договора",
                Description = "Разработать проект агентского договора для адаптации нового сервиса компании Ремонты",
                EndDate = new DateTime(2020, 07, 30),
                CreationDate = new DateTime(2020, 06, 18)
            }
        }.AsQueryable<Order>();
    }
}
