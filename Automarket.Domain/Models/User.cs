﻿using Automarket.Domain.Enum;
using Automarket.Domain.ViewModels;

namespace Automarket.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        public List<CartItem>? CartItems { get; set; }

    }
}
