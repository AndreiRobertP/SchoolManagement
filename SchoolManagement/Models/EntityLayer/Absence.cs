using SchoolManagement.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.EntityLayer
{
    public class Absence: BasePropertyChanged
    {
        private int _absenceId;
        [Key]
        public int AbsenceId { get { return _absenceId; } set { _absenceId = value; OnPropertyChanged(); } }

        private int _shtId;
        public int ShtId { get { return _shtId; } set { _shtId = value; OnPropertyChanged(); } }

        private int _studentId;
        public int StudentId { get { return _studentId; } set { _studentId = value; OnPropertyChanged(); } }

        private DateTime _givenDate;
        public DateTime GivenDate { get { return _givenDate; } set { _givenDate = value; OnPropertyChanged(); } }

        private bool _isActive;
        public bool IsActive { get { return _isActive; } set { _isActive = value; OnPropertyChanged(); } }

        private Sht _sht = null!;
        public Sht Sht { get { return _sht; } set { _sht = value; OnPropertyChanged(); } }

        private Student _student = null!;
        public Student Student { get { return _student; } set { _student = value; OnPropertyChanged(); } }
    }
}
