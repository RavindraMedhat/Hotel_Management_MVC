using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Models
{
    public class LoginRequest
    {

        [Required]
        public String EmailID { get; set; }
        [Required]
        public String Password { get; set; }
    }
}
