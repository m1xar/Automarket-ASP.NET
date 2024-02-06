using Automarket.Domain.Enum;
using Automarket.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        public UserRole Role { get; set; }

        public List<CartItem>? CartItems { get; set; }
    }
}
