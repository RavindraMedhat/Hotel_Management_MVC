using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.ViewModels
{
    public class HotelViewModelForIndex
    {
        [Key]
        public int Hotel_ID { get; set; }
        [NotMapped]
        public string EncryptedID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name can not more than 50 character")]
        public string Hotel_Name { get; set; }

        public List<String> Image_URl { get; set; }
    }
}
