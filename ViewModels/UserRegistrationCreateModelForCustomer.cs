using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.ViewModels
{
    public class UserRegistrationCreateModelForCustomer
    {
        [Key]
        public int User_ID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name can not more than 50 character")]
        public string First_Name { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name can not more than 50 character")]
        public string Last_Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Enter Valid Email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        //[Required]
        //[ForeignKey("Role_ID")]
        //public int Role_ID { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format.")]
        public string ConatactNo { get; set; }
        public IFormFile Profile_Image { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public bool Active_Flag { get; set; }

        [Required]
        public bool Delete_Flag { get; set; }

        [Required]
        public float sortedfield { get; set; }

    }
}
