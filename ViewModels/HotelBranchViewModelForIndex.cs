using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.ViewModels
{
    public class HotelBranchViewModelForIndex
    {
        [Key]
        public int Branch_ID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can not more than 50 character")]
        public string Branch_Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can not more than 50 character")]
        public string Hotel_Name { get; set; }

        public List<String> Image_URl { get; set; }
        public string Address { get; set; }

    }
}
