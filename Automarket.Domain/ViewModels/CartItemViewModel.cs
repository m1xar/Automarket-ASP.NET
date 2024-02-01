using Automarket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.ViewModels
{
    public class CartItemViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CarId { get; set; }

        public User User { get; set; }

        public Car Car { get; set; }
    }
}
