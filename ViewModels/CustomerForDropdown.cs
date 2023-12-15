using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.ViewModels
{
    public class CustomerForDropdown
    {
        [Key]
        public int User_ID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name can not more than 50 character")]
        public string Name { get; set; }
    }
}
