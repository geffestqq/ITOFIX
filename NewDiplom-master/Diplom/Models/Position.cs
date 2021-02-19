using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        [RegularExpression(@"^[а-яА-Я]{1,30}$",
         ErrorMessage = "Некоректное значение поля")]
        [StringLength(30, ErrorMessage = "Некоректная длина поля")]
        public string Position_name { get; set; }

        public string View => $"{Position_name}";

        public ICollection<Plurality> Plurality { get; set; }
        public Position()
        {
            Plurality = new List<Plurality>();
        }
    }
}
