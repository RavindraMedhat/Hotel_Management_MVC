using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Models
{
    public class RoomTB
    {
        [Key]
        public int Room_ID { get; set; }
        [Required]
        [ForeignKey("Category_ID")]
        public int Category_ID { get; set; }
        [Required]
        [ForeignKey("Branch_ID")]
        public int Branch_ID { get; set; }

        [Required]
        [ForeignKey("Hotel_ID")]
        public int Hotel_ID { get; set; }

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
        [Required]
        public bool Active_Flag { get; set; }
        [Required]
        public bool Delete_Flag { get; set; }

        [Required]
        public float sortedfield { get; set; }
    }
}
