using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewDiplom.Models
{
    public class Right
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        public byte Give_out { get; set; }

        [Required(ErrorMessage = "Обязательное для заполнения*")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        public int FunctionId { get; set; }
        public Function Function { get; set; }

        public string View
        {
            get
            {
                return String.Format("{0} {1}",
                    Role == null ? String.Empty : Role.Name_Role,
                    Function == null ? String.Empty : Function.Name_function);
            }
        }

        public ICollection<Account> Accounts { get; set; }
        public Right()
        {
            Accounts = new List<Account>();
        }
    }
}
