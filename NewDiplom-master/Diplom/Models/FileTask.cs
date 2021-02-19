using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom.Models
{
    public class FileTask
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательное для заполнения*"), StringLength(50)]
        public string Path_File { get; set; }

        [Required(ErrorMessage = "Обязательное для заполнения*"), StringLength(50)]

        public string File_Detail { get; set; }

        public int TaskDistributionId { get; set; }
        public TaskDistribution TaskDistribution { get; set; }

    }
}
