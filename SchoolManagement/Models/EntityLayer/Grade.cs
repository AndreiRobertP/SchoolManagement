using SchoolManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SchoolManagement.Models.EntityLayer
{
    public class Grade : BasePropertyChanged
    {
        private int _gradeId;
        [Key]
        public int GradeId { get { return _gradeId; } set { _gradeId = value; OnPropertyChanged(); } }

        private int _value = 0;
        public int Value { get { return _value; } set { _value = value; OnPropertyChanged(); } }

        private DateTime _givenDate;
        public DateTime GivenDate { get { return _givenDate; } set { _givenDate = value; OnPropertyChanged(); } }

        private bool _isThesis = false;
        public bool IsThesis { get { return _isThesis; } set { _isThesis = value; OnPropertyChanged(); } }

        private bool _isActive = true;
        public bool IsActive { get { return _isActive; } set { _isActive = value; OnPropertyChanged(); } }

        private int _semester = 1;
        public int Semester { get { return _semester; } set { _semester = value; OnPropertyChanged(); } }

        private Sht _sht = null!;
        public Sht Sht { get { return _sht; } set { _sht = value; OnPropertyChanged(); } }

        private Student _student = null!;
        public Student Student { get { return _student; } set { _student = value; OnPropertyChanged(); } }

        public bool CheckValid()
        {
            if (Sht == null) return false;
            if (Student == null) return false;
            if (IsThesis && !Sht.HasThesis) return false;

            return true;
        }

        public static int ComputeMeanWithoutThesis(Grade[] grades)
        {
            double avg = grades.Average(g => g.Value);
            return (int) Math.Round(avg, 0, MidpointRounding.AwayFromZero);
        }

        public static int ComputeMeanWithThesis(Grade thesis, Grade[] grades)
        {
            double avg = grades.Average(g => g.Value);
            avg = Math.Round(avg, 2, MidpointRounding.AwayFromZero);

            double avgWithThesis = 0.75 * avg + 0.25 * thesis.Value;
            return (int)Math.Round(avgWithThesis, 0, MidpointRounding.AwayFromZero);
        }
    }
}