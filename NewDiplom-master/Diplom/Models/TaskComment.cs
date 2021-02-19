using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom.Models
{
    public class TaskComment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное для заполнения*")]
        [StringLength(100)]
        public string Text_comment { get; set; }

        public int Type_CommentsId { get; set; }
        public TypeComment Type_Comments { get; set; }
        public int TaskDistributionsId { get; set; }
        public TaskDistribution TaskDistributions { get; set; }
    }
}
