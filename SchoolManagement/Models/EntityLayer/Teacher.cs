using Microsoft.EntityFrameworkCore;
using SchoolManagement.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.EntityLayer
{
    public class Teacher : BasePropertyChanged
    {
        [Key]
        public int Id { get; set; }

        private string _username = "";
        [Required]
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _name = "";
        [Required]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private bool _isActive = true;
        [Required]
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }
    }
}
