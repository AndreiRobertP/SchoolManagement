using Microsoft.EntityFrameworkCore;
using SchoolManagement.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.EntityLayer
{
    public class Student : BasePropertyChanged
    {
        private int _studentId;
        [Key]
        public int StudentId { get { return _studentId; } set { _studentId = value; OnPropertyChanged(); } }

        private string _username = "NewStudent";
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(); } }

        private string _name = "";
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        private bool _isActive;
        public bool IsActive { get { return _isActive; } set { _isActive = value; OnPropertyChanged(); } }

        private Homeroom _homeroom = null!;
        public Homeroom Homeroom { get { return _homeroom; } set { _homeroom = value; OnPropertyChanged(); } }

        public bool CheckValid() {
            if (Homeroom == null) return false;

            return true;
        }
    }
}
