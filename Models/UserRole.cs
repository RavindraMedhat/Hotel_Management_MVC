using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Models
{
    public class UserRole
    {
        [Key]
        public int Role_ID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name can not more than 50 character")]
        public string Role_Name { get; set; }
        [Required]
        public bool Active_Flag { get; set; }

        [Required]
        public bool Delete_Flag { get; set; }

        [Required]
        public float sortedfield { get; set; }
    }
}
