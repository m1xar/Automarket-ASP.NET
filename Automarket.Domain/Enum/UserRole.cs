using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Enum
{
    public enum UserRole
    {
        [Display(Name = "Customer")]
        Customer = 0,
        [Display(Name = "Moderator")]
        Moderator = 1,
        [Display(Name = "Admin")]
        Admin = 2
    }
}
