using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        [StringLength(30)]
        [RegularExpression(@"^[а-яА-Я\s]{1,30}$",
         ErrorMessage = "Некоректное значение поля")]
        public string Status_name { get; set; }

        public ICollection<TaskDistribution> TaskDistributions { get; set; }
        public ICollection<Zadachi> Zadachis { get; set; }
        public Status()
        {
            TaskDistributions = new List<TaskDistribution>();
            Zadachis = new List<Zadachi>();
        }
    }
}
