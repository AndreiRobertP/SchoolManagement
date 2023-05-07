using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Models.EntityLayer
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        
        [Required]
        public string NameSubject { get; set; } = "NewSubject";

        [Required]
        public bool IsActive { get; set; } = true;

    }
}
