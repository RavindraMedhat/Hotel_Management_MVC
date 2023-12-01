using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Models
{
    public class RelationshipTB
    {
        [Key]
        public int Relationship_ID { get; set; }

        [Required]
        [ForeignKey("Role_ID")]
        public int Role_ID { get; set; }
        [Required]
        [ForeignKey("User_ID")]
        public int User_ID { get; set; }

        public int Hotel_ID { get; set; }
        public int Branch_ID { get; set; }
    }
}
