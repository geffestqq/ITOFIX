using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewDiplom.Models
{
    public class Function
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        [StringLength(30)]
        [RegularExpression(@"^[а-яА-Я]{1,30}$",
         ErrorMessage = "Некоректное значение поля")]
        public string Name_function { get; set; }        
        public ICollection<Right> Rights { get; set; }
        public Function()
        {
           Rights = new List<Right>();
        }
    }
}
