using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewDiplom.Models
{
    public class Plurality
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        public int PositionId { get; set; }
        public Position Position { get; set; }

        public string View => String.Format("{0} {1}",
                    Position == null ? String.Empty : Position.Position_name,
                    Employee == null ? String.Empty : Employee.View);

        public ICollection<Account> Accounts { get; set; }
        public ICollection<TaskDistribution> TaskDistributions { get; set; }
        public Plurality()
        {
            TaskDistributions = new List<TaskDistribution>();
            Accounts = new List<Account>();
        }
    }
}
