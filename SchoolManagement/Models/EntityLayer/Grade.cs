using SchoolManagement.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.EntityLayer
{
    public class Grade : BasePropertyChanged
    {
        private int _gradeId;
        [Key]
        public int GradeId { get { return _gradeId; } set { _gradeId = value; OnPropertyChanged(); } }

        private int _shtId;
        public int ShtId { get { return _shtId; } set { _shtId = value; OnPropertyChanged(); } }

        private int _studentId;
        public int StudentId { get { return _studentId; } set { _studentId = value; OnPropertyChanged(); } }

        private float _value = 0;
        public float Value { get { return _value; } set { _value = value; OnPropertyChanged(); } }

        private DateTime _givenDate;
        public DateTime GivenDate { get { return _givenDate; } set { _givenDate = value; OnPropertyChanged(); } }

        private bool _isThesis = false;
        public bool IsThesis { get { return _isThesis; } set { _isThesis = value; OnPropertyChanged(); } }

        private bool _isActive = true;
        public bool IsActive { get { return _isActive; } set { _isActive = value; OnPropertyChanged(); } }

        private Sht _sht = null!;
        public Sht Sht { get { return _sht; } set { _sht = value; OnPropertyChanged(); } }

        private Student _student = null!;
        public Student Student { get { return _student; } set { _student = value; OnPropertyChanged(); } }
    }
}
