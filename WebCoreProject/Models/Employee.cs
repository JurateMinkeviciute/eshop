using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [NotMapped]
        public string EncrypredId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Dept Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
