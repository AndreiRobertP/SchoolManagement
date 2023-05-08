using Microsoft.EntityFrameworkCore;
using SchoolManagement.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.EntityLayer
{
    public class Teacher : BasePropertyChanged
    {
        private int _teacherId;
        [Key]
        public int TeacherId { get { return _teacherId; } set { _teacherId = value; OnPropertyChanged(); } }

        private string _username = "";
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(); } }

        private string _name = "";
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        private bool _isActive = true;
        public bool IsActive { get { return _isActive; } set { _isActive = value; OnPropertyChanged(); } }
    }
}
