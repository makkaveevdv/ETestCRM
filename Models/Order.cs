using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace ETestCRM.Models
{    
    public class Order
    {
        public long OrderId { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите наименование задачи")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите описание задачи")]
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите приоритет задачи")]
        public string Priority { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите статус задачи")]
        public string Status { get; set; }

        public string CreatorId { get; set; } = null;
        public string CreatorFullName { get; set; } = null;
        public string RespUserId { get; set; } = null;
        public string RespUserFullName { get; set; } = null;


    }
}
