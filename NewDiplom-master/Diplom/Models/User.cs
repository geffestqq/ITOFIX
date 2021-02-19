using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Diplom.Models
{
    public class User : IdentityUser
    {
        public User()
        {

        }
        public User(string name) : base(name)
        {

        }
        public int? PluralityId { get; set; }

        public Plurality Plurality { get; set; }
    }
}
