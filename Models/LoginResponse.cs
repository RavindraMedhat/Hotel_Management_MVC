using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string Redirect { get; set; }

        public int RedirctID { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
