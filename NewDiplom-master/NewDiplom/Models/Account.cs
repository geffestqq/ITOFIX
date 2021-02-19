using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewDiplom.Models
{
    public class Account
    {   
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        [StringLength(30 , MinimumLength = 6 , ErrorMessage = "Некоректная длина поля")]
        public string Login  { get; set; }
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Некоректная длина поля")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        [DataType(DataType.Date)]
        public string Date_Create { get; set; }
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        [StringLength(10)]
        [Phone(ErrorMessage = "Некоректный введен номер телефона")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Некоректное значение поля")]
        public string Phone_Number { get; set; }
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        [StringLength(30)]
        [EmailAddress(ErrorMessage = "Некоректный введенный адрес")]
        public string Email { get; set; }
        public int RightsId { get; set; }
        public Right Rights { get; set; }

        public int PluralityId { get; set; }
        public Plurality Plurality { get; set; }


    }
}
