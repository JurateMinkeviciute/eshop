using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreProject.Models;
using System.ComponentModel.DataAnnotations;

namespace WebCoreProject.ViewModels
{
    public class CreatePhoneViewModel
    {
        [Display(Name = "Display Name")]
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Model { get; set; }
        public System.Nullable<decimal> Price { get; set; }
        public System.Nullable<decimal> OldPrice { get; set; }
        public int Quantity { get; set; }
        public PhoneColor Color { get; set; }
        public PhoneStorage Storage { get; set; }
        [Required, MinLength(40, ErrorMessage = "Description min length 20 characters")]
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public IFormFile Photo { get; set; }
    }
}
