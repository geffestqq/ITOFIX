using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewDiplom.Models
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательное для заполнения*")]
        [StringLength(30)]
        [RegularExpression(@"^[а-яА-Я]{1,30}$",
         ErrorMessage = "Некоректное значение поля")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Обязательное для заполнения*")]
        [StringLength(30)]
        [RegularExpression(@"^[а-яА-Я]{1,30}$",
         ErrorMessage = "Некоректное значение поля")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное для заполнения*")]
        [StringLength(30)]
        [RegularExpression(@"^[а-яА-Я]{1,30}$",
         ErrorMessage = "Некоректное значение поля")]
        public string Second_Name { get; set; }

        [Required(ErrorMessage = "Обязательное для заполнения*")]
        public int Employee_Number { get; set; }

        public string View => $"{Surname} {Name} {Second_Name}";


        public ICollection<Plurality> Pluralitys { get; set; }
        public Employee()
        {
            Pluralitys = new List<Plurality>();
        }

    }
}
