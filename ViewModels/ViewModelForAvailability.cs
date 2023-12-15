using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.ViewModels
{
    public class ViewModelForAvailability
    {
        [Key]
        public int Booking_ID { get; set; }

        [Required]
        [ForeignKey("Room_ID")]
        public int Room_ID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Customer_Name { get; set; }

        [Required]
        public string Availability { get; set; }

    }
}
