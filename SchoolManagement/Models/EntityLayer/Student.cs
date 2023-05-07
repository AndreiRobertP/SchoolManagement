using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.EntityLayer
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = "NewTeacher";

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public Homeroom Homeroom { get; set; } = null!;
    }
}
