using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Models
{
    public class HotelTB
    {
        [Key]
        public int Hotel_ID { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage ="Name can not more than 50 character")]
        public string Hotel_Name { get; set; }

        [Required]
        [MaxLength(300, ErrorMessage = "Description can not more than 300 character")]
        public string Hotel_Description { get; set; }

        //config che bro
        //[Required]
        //public string Hotel_Images { get; set; }

        [Required]
        public string Hotel_map_coordinate { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Address can not more than 50 character")]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format.")]
        public string Contact_No { get; set; }
      
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Enter Valid Email")]
        public string Email_Adderss { get; set; }
       
        [Required]
        [MaxLength(50, ErrorMessage = "Contect Person Name can not more than 50 character")]
        public string Contect_Person { get; set; }
        
        [Required]
        public string Standard_check_In_Time { get; set; }
        
        [Required]
        public string Standard_check_out_Time { get; set; }

        [Required]
        public bool Active_Flag { get; set; }
        
        [Required]
        public bool Delete_Flag { get; set; }
        
        [Required]
        public float sortedfield { get; set; }

    }
}
