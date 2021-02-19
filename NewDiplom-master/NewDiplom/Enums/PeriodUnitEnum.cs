using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewDiplom.Enums
{
    public enum PeriodUnitEnum : int
    {
        [Display(Name = "День")]
        Day = 0,
        [Display(Name = "Неделя")]
        Week = 1,
        [Display(Name="Месяц")]
        Month = 2
    }
}
