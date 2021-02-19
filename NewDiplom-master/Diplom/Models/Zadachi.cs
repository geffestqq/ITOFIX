using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class Zadachi
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательное для заполнения*"), StringLength(30)]


        public string Task_Name { get; set; }

        [Required(ErrorMessage = "Обязательное для заполнения*"), StringLength(30)]

        public string Task_Detail { get; set; }

        [Required(ErrorMessage = "Обязательное для заполнения*"), StringLength(11)]
        [DataType(DataType.Date)]
        public string Date_Open { get; set; }
        [DataType(DataType.Date)]

        [Required(ErrorMessage = "Обязательное для заполнения*"), StringLength(11)]
        public string Date_Close { get; set; }

#nullable enable
        public int? ZadachiParentId { get; set; }
        public Zadachi? ZadachiParent { get; set; }
#nullable disable

        [Required]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        public string View => String.Format("{0} {1}",
                    Task_Name,
                    Date_Open);

        public List<TaskDistribution> TaskDistributions { get; set; }
        public Zadachi()
        {
            TaskDistributions = new List<TaskDistribution>();
        }
    }
}
