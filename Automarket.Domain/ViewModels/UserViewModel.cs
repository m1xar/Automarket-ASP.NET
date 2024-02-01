using Automarket.Domain.Enum;
using Automarket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        public List<CartItem>? CartItems { get; set; }
    }
}
