using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.ViewModel
{
    public class BookingForDetail
    {
        [Required]
        public string Customer_Name { get; set; }
        [Required]
        public string Room_No { get; set; }
        [Required]
        public string Hotel_name { get; set; }
        [Required]
        public string Branch_name { get; set; }
        [Required]
        public string Group_ID { get; set; }
        [Required]
        public DateTime Booking_Date { get; set; }
        [Required]
        public DateTime Check_In_Date { get; set; }
        [Required]
        public DateTime Check_Out_Date { get; set; }
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

    }
}
