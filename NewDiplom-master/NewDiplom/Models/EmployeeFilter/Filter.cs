using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewDiplom.Models.EmployeeFilter
{
    public class Filter
    {
        public string FilterSurname { get; set; }
        public string FilterName { get; set; }
        public string FilterSecond_Name { get; set; }

        public Filter()
        {
            FilterSurname = string.Empty;
            FilterName = string.Empty;
            FilterSecond_Name = string.Empty;
        }
    }
}
