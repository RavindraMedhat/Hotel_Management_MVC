using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Models
{
    public class RoomTBForIndex
    {
        [Key]
        public int Room_ID { get; set; }
        public int Category_ID { get; set; }


        [Required]
        [MaxLength(7, ErrorMessage = "Room_NO can not more than 7 character")]
        public string Room_No { get; set; }

        [Required]
        public List<string> Image_URl { get; set; }

        [Required]
        public float Room_Price { get; set; }
        
        [Required]
        public int Iminity_NoOfBed { get; set; }
        
    }
}
