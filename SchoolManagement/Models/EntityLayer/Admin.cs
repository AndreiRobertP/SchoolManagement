using Microsoft.EntityFrameworkCore;
using SchoolManagement.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace SchoolManagement.Models.EntityLayer
{
    public class Admin : BasePropertyChanged
    {
        private int _id;
        [Key]
        public int Id { get { return _id; } set { _id = value; OnPropertyChanged(); } }

        private string _username = "NewTeacher";
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(); } }

        private string _name = "";
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }

        private bool _isActive = true;
        public bool IsActive { get { return _isActive; } set { _isActive = value; OnPropertyChanged(); } }

        public bool CheckValid()
        {
            return true;
        }
    }
}
