using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.ViewModels
{
    public class couponViewModelForIndex
    {

        [Required]
        public string Coupon_Name { get; set; }
        [Required]

        public DateTime Start_Date { get; set; }
        [Required]

        public DateTime Expiry_Date { get; set; }
        [Required]
        public int Discount_Percentage { get; set; }
        [Required]
        public int noOfAvailableCoupen { get; set; }

    }
}
