using Automarket.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Automarket.Domain.Models;
using System.ComponentModel.DataAnnotations;


namespace Automarket.Domain.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Max Length is 40")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [MaxLength(400, ErrorMessage = "Max Length is 400")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [MaxLength(40, ErrorMessage = "Max Length is 40")]
        [Required(ErrorMessage = "Model name is required")]
        public string Model { get; set; }
        [Range(1, 500, ErrorMessage = "Speed must be between 1 and 500")]
        [Required(ErrorMessage = "Speed is required")]
        public double Speed { get; set; }
        [Range(1000, 1000000, ErrorMessage = "Speed must be between 1000 and 1000000")]
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        public DateTime DateCreate { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public TypeCar TypeCar { get; set; }
        [Required(ErrorMessage = "Image is required")]

        public IFormFile ImageView { get; set; }

        public string Image { get; set; }
        public CarViewModel(Car car)
        {
            Id = car.Id;
            Name = car.Name;
            Description = car.Description;
            Model = car.Model;
            Speed = car.Speed;
            Price = car.Price;
            DateCreate = car.DateCreate;
            TypeCar = car.TypeCar;
            Image = car.Image;
        }
        
        public CarViewModel() { }

    }

    
}
