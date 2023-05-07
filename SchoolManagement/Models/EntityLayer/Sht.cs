using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Models.EntityLayer
{
    public class Sht
    {
        [Key]
        public int ShtId { get; set; }

        [Required]
        public int HasThesis { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;

        //Relations
        public Subject Subject { get; set; } = null!;
        public Homeroom Homeroom { get; set; } = null!;
        public Teacher Teacher { get; set; } = null!;
    }
}
