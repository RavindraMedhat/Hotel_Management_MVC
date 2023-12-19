using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Models
{
    public class Billing
    {
        [Key]
        public int Bill_ID { get; set; }
        [Required]
        public string Group_ID { get; set; }
        [Required]

        public DateTime Bill_Date { get; set; }
        [Required]

        public float Total_Amount { get; set; }
        [Required]

        public float Discount_Amount { get; set; }
        [Required]

        public float Final_Amount { get; set; }
        [Required]

        public float Payed_Amount { get; set; }



    }
}
