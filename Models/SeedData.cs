using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETestCRM.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order
                    {
                        Name = "Разработка коммерчекого предложения",
                        Description = "Разработать коммерческое предложение для компании Hoff о новых условиях сотрудничества",
                        EndDate = new DateTime(2020, 06, 30),
                        CreationDate = new DateTime(2020, 06, 18),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Высокий",
                        Status = "К выполнению"
                        //CreatorId = "b8445966-c2a8-44e5-9d5b-83898cf54e16",
                        //CreatorFullName = "Дмитрий Дмитриевич Дмитренко",
                        //RespUserId = "94ac7d40-e73f-4035-af77-d1b96277a33e",
                        //RespUserFullName = "Павел Павлович Павлов"
                    },
                    new Order
                    {
                        Name = "Разработка методических указаний",
                        Description = "Разработать методические указания для специалистов компании, включающие рекомендации по использованию опции Каршеринг",
                        EndDate = new DateTime(2020, 06, 29),
                        CreationDate = new DateTime(2020, 06, 18),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Высокий",
                        Status = "К выполнению"
                        //CreatorId = "b8445966-c2a8-44e5-9d5b-83898cf54e16",
                        //CreatorFullName = "Дмитрий Дмитриевич Дмитренко",
                        //RespUserId = "94ac7d40-e73f-4035-af77-d1b96277a33e",
                        //RespUserFullName = "Павел Павлович Павлов"
                    },
                    new Order
                    {
                        Name = "Подготовка квартального отчета",
                        Description = "Подготовить квартальный отчет за апрель, май, июнь 2020 года с детализацией доходов и расходов компании в рамках продукта СКОР",
                        EndDate = new DateTime(2020, 07, 10),
                        CreationDate = new DateTime(2020, 06, 18),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Высокий",
                        Status = "К выполнению"
                        //CreatorId = "b8445966-c2a8-44e5-9d5b-83898cf54e16",
                        //CreatorFullName = "Дмитрий Дмитриевич Дмитренко",
                        //RespUserId = "94ac7d40-e73f-4035-af77-d1b96277a33e",
                        //RespUserFullName = "Павел Павлович Павлов"
                    },
                    new Order
                    {
                        Name = "Разработка проекта агентского договора",
                        Description = "Разработать проект агентского договора для адаптации нового сервиса компании Ремонты",
                        EndDate = new DateTime(2020, 07, 30),
                        CreationDate = new DateTime(2020, 06, 18),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Средний",
                        Status = "К выполнению"
                        //CreatorId = "b8445966-c2a8-44e5-9d5b-83898cf54e16",
                        //CreatorFullName = "Дмитрий Дмитриевич Дмитренко",
                        //RespUserId = "94ac7d40-e73f-4035-af77-d1b96277a33e",
                        //RespUserFullName = "Павел Павлович Павлов"
                    },
                    new Order
                    {
                        Name = "Разработка проекта договора комиссии",
                        Description = "Разработать проект договора комиссии для адаптации нового сервиса компании Переезд",
                        EndDate = new DateTime(2020, 07, 30),
                        CreationDate = new DateTime(2020, 06, 18),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Высокий",
                        Status = "К выполнению"
                        //CreatorId = "b8445966-c2a8-44e5-9d5b-83898cf54e16",
                        //CreatorFullName = "Дмитрий Дмитриевич Дмитренко",
                        //RespUserId = "94ac7d40-e73f-4035-af77-d1b96277a33e",
                        //RespUserFullName = "Павел Павлович Павлов"
                    },
                    new Order
                    {
                        Name = "Разработка проекта договора ипотечного брокеринга",
                        Description = "Разработать проект договора ипотечного брокеринга для адаптации нового сервиса компании Выездной брокер",
                        EndDate = new DateTime(2020, 06, 17),
                        CreationDate = new DateTime(2020, 06, 16),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Низкий",
                        Status = "К выполнению"
                        //CreatorId = "b8445966-c2a8-44e5-9d5b-83898cf54e16",
                        //CreatorFullName = "Дмитрий Дмитриевич Дмитренко",
                        //RespUserId = "94ac7d40-e73f-4035-af77-d1b96277a33e",
                        //RespUserFullName = "Павел Павлович Павлов"
                    },
                    new Order
                    {
                        Name = "Разработка графика дежурств",
                        Description = "Разработать график дежурств для групп специалистов по недвижимости",
                        EndDate = new DateTime(2020, 06, 24),
                        CreationDate = new DateTime(2020, 06, 18),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Низкий",
                        Status = "К выполнению"
                        //CreatorId = "b8445966-c2a8-44e5-9d5b-83898cf54e16",
                        //CreatorFullName = "Дмитрий Дмитриевич Дмитренко",
                        //RespUserId = "94ac7d40-e73f-4035-af77-d1b96277a33e",
                        //RespUserFullName = "Павел Павлович Павлов"
                    },
                    new Order
                    {
                        Name = "Разработка графика работы технического персонала",
                        Description = "Разработать график работы технического персонала для повышения эффективности использования времени",
                        EndDate = new DateTime(2020, 07, 30),
                        CreationDate = new DateTime(2020, 06, 18),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Высокий",
                        Status = "К выполнению"
                        //CreatorId = "b8445966-c2a8-44e5-9d5b-83898cf54e16",
                        //CreatorFullName = "Дмитрий Дмитриевич Дмитренко",
                        //RespUserId = "94ac7d40-e73f-4035-af77-d1b96277a33e",
                        //RespUserFullName = "Павел Павлович Павлов"
                    },
                    new Order
                    {
                        Name = "Разработка проекта договора о совместной деятельности",
                        Description = "Разработать проект договора о совместной деятельности для юридической интеграции нового специалиста в структуру компании",
                        EndDate = new DateTime(2020, 06, 16),
                        CreationDate = new DateTime(2020, 06, 15),
                        UpdateDate = new DateTime(2020, 06, 15),
                        Priority = "Средний",
                        Status = "К выполнению"
                        //CreatorId = "b8445966-c2a8-44e5-9d5b-83898cf54e16",
                        //CreatorFullName = "Дмитрий Дмитриевич Дмитренко",
                        //RespUserId = "94ac7d40-e73f-4035-af77-d1b96277a33e",
                        //RespUserFullName = "Павел Павлович Павлов"
                    },
                    new Order
                    {
                        Name = "Разработка проекта договора подряда",
                        Description = "Разработать проект договора строительного подряда для выполнения работ по ремонту переговорных комнат на шестом этаже",
                        EndDate = new DateTime(2020, 08, 20),
                        CreationDate = new DateTime(2020, 06, 18),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Высокий",
                        Status = "К выполнению"
                    },
                    new Order
                    {
                        Name = "Разработка технического задания",
                        Description = "Разработать техническое задания на реализацию проекта разработки веб-приложения для видеосвязи",
                        EndDate = new DateTime(2020, 06, 26),
                        CreationDate = new DateTime(2020, 06, 18),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Высокий",
                        Status = "К выполнению"
                    },
                    new Order
                    {
                        Name = "Разработка проекта договора займа",
                        Description = "Разработать проект договора займа для реализации возможности выдавать займ от компании юридическому лицу под залог недвижимого и движимого имущества",
                        EndDate = new DateTime(2020, 07, 22),
                        CreationDate = new DateTime(2020, 06, 18),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Низкий",
                        Status = "К выполнению"
                    },
                    new Order
                    {
                        Name = "Подготовка годового отчета",
                        Description = "Подготовить годовой отчет по деятельности компании Западная Сибирь",
                        EndDate = new DateTime(2021, 01, 30),
                        CreationDate = new DateTime(2020, 06, 18),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Высокий",
                        Status = "К выполнению"
                    },
                    new Order
                    {
                        Name = "Выезд в г. Петропавловск",
                        Description = "Выполнить выезд в город Петропавловск для открытия нового офиса в рамках развития франчайзинговой сети",
                        EndDate = new DateTime(2020, 09, 30),
                        CreationDate = new DateTime(2020, 06, 18),
                        UpdateDate = new DateTime(2020, 06, 18),
                        Priority = "Средний",
                        Status = "К выполнению"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
