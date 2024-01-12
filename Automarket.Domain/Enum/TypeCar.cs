using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Enum
{
    public enum TypeCar
    {
        [Display(Name = "Passenger Car")]
        PassengerCar = 0,
        [Display(Name = "Sedan")]
        Sedan = 1,
        [Display(Name = "HatchBack")]
        HatchBack = 2,
        [Display(Name = "Minivan")]
        Minivan = 3,
        [Display(Name = "Sports Car")]
        SportsCar = 4,
        [Display(Name = "SUV")]
        Suv = 5
    }
}
