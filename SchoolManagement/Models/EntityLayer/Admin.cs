using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Models.EntityLayer
{
    public class Admin
    {
        // =====================
        // Basic props for user
        // =====================

        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = "NewTeacher";
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public bool IsActive { get; set; } = false;

    }
}
