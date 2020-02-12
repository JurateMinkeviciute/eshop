using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreProject.Models
{
    public class Phone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public System.Nullable<decimal> OldPrice { get; set; }
        public int Quantity { get; set; }
        public PhoneColor Color { get; set; }
        public PhoneStorage Storage { get; set; }
        [Column(TypeName = "Text")]
        public string Description { get; set; }
        public string PhotoPath { get; set; }
    }
}
