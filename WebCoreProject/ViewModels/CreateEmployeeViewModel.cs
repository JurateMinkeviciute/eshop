﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebCoreProject.Models;

namespace WebCoreProject.ViewModels
{
    public class CreateEmployeeViewModel
    {
        //public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage ="Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Display(Name="Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage ="Select Department")]
        public Dept? Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}