using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Models
{
    public class Booking
    {
        [Key]
        public int Booking_ID { get; set; }
        [Required]
        [ForeignKey("User_ID")]
        public int User_ID { get; set; }
        [Required]
        [ForeignKey("Room_ID")]

        public int Room_ID { get; set; }
        public string Group_ID { get; set; }
        [Required]
        [ForeignKey("Branch_ID")]
        public int Branch_ID { get; set; }
        [Required]
        public DateTime Check_In_Date { get; set; }
        [Required]
        public DateTime Check_Out_Date { get; set; }
        [Required]
        public DateTime Booking_Date { get; set; }
        [Required]
        public string Payment_Status { get; set; }
        [Required]
        public float Payed_Amount { get; set; }
        [Required]
        public string Payment_Mode { get; set; }
        public string Coupon_Code { get; set; }
        [Required]
        public string Booking_Status { get; set; }
        public int Discount { get; set; }
        [Required]
        public string Customer_status { get; set; }
        [Required]
        public bool Active_Flag { get; set; }
        [Required]
        public bool Delete_Flag { get; set; }
        [Required]
        public float Sortedfield { get; set; }

    }
}
