using Hotel_Management_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.ViewModels
{
    public class BillRes
    {
        public Billing bill { get; set; }
        public List<Details> details { get; set; }
        public string Email { get; set; }
        public string CustomerName { get; set; }
        public string HotelName { get; set; }
        public string HotelBranch { get; set; }
    }
    public class Details
    {
        public string detail { get; set; }
        public float amount { get; set; }
    }
}
