using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Models
{
    public class HotelBranchTBCreateModel
    {
        [Key]
        public int Branch_ID { get; set; }

        [Required]
        [ForeignKey("Hotel_ID")] 
        public int Hotel_ID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can not more than 50 character")]
        public string Branch_Name { get; set; }
        [Required]
        [MaxLength(300, ErrorMessage = "Description can not more than 300 character")]
        public string Branch_Description { get; set; }

        //config che bro
        //[Required]
        //public string Branch_Images { get; set; }

        [Required]
        public string Branch_map_coordinate { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Address can not more than 50 character")]
        public string Branch_Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format.")]
        public string Branch_Contact_No { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Enter Valid Email")]
        public string Branch_Email_Adderss { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Contect Person Name can not more than 50 character")]
        public string Branch_Contect_Person { get; set; }

        //[Required]
        //public string Standard_check_In_Time { get; set; }

        //[Required]
        //public string Standard_check_out_Time { get; set; }

        [Required]
        public bool Active_Flag { get; set; }

        [Required]
        public bool Delete_Flag { get; set; }

        [Required]
        public float sortedfield { get; set; }

        public List<IFormFile> Photos { get; set; }


    }
}
