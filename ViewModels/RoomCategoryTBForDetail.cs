using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.ViewModels
{
    public class RoomCategoryTBForDetail
    {
        [Key]
        public int Category_ID { get; set; }
        [Required]

        public String Branch_Name { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name can not more than 50 character")]
        public string Category_Name { get; set; }
        [Required]
        [MaxLength(300, ErrorMessage = "Description can not more than 300 character")]
        public string Description { get; set; }
        [Required]
        public bool Active_Flag { get; set; }
        [Required]
        public bool Delete_Flag { get; set; }

        [Required]
        public float sortedfield { get; set; }
    }
}
