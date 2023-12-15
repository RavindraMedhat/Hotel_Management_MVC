using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.ViewModels
{
    public class RoomViewModelForDetails
    {
        [Key]
        public int Room_ID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can not more than 50 character")]
        public string Category_Name { get; set; }
        [Required]
        [ForeignKey("Branch_ID")]
        public int Branch_ID { get; set; }
        [Required]
        public List<string> Image_URl { get; set; }

        [Required]
        [MaxLength(7, ErrorMessage = "Room_NO can not more than 7 character")]
        public string Room_No { get; set; }

        [Required]
        [MaxLength(300, ErrorMessage = "Room_Description can not more than 300 character")]

        public string Room_Description { get; set; }

        [Required]
        public float Room_Price { get; set; }
        [Required]

        public bool Iminity_Pool { get; set; }
        [Required]

        public bool Iminity_Bath { get; set; }
        [Required]

        public int Iminity_NoOfBed { get; set; }
    }
}
