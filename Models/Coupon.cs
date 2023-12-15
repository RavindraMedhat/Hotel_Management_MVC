using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Models
{
    public class Coupon
    {
        [Key]
        public int Coupon_ID { get; set; }

        [Required]
        [ForeignKey("Hotel_ID")]
        public int Hotel_ID { get; set; }
        [Required]
        public string Coupon_Name { get; set; }
        [Required]
        public string Coupon_Code { get; set; }

        [Required]
        public DateTime Start_Date { get; set; }
        [Required]

        public DateTime Expiry_Date { get; set; }
        [Required]

        public int Discount_Percentage { get; set; }
        [Required]
        public bool Active_Flag { get; set; }
        [Required]
        public bool Delete_Flag { get; set; }
        [Required]
        public float Sortedfield { get; set; }
    }
}
