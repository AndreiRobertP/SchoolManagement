using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Models.EntityLayer
{
    public class Homeroom
    {
        [Key]
        public int HomeroomId { get; set; }

        [Required]
        public string NameHomeroom { get; set; } = "NewClass";

        [Required]
        public int Year { get; set; }

        [Required]
        public bool IsActive { get; set; }


        //Relations
        public Teacher Teacher { get; set; } = null!;
        public Specialization Specialization { get; set; } = null!;
    }
}
