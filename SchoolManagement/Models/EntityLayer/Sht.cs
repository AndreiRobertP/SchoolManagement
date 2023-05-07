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
        public int SubjectId { get; set; }

        [Required]
        public int HomeroomId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int HasThesis { get; set; }
    }
}
