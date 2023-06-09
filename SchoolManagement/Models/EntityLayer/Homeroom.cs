﻿using SchoolManagement.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.EntityLayer
{
    public class Homeroom : BasePropertyChanged
    {
        int _homeroomId;
        [Key]
        public int HomeroomId { get { return _homeroomId; } set { _homeroomId = value; OnPropertyChanged(); } }

        private string _nameHomeroom = "NewClass";
        public string NameHomeroom { get { return _nameHomeroom; } set { _nameHomeroom = value; OnPropertyChanged(); } }

        private int _year = 0;
        public int Year { get { return _year; } set { _year = value; OnPropertyChanged(); } }

        private bool _isActive = true;
        public bool IsActive { get { return _isActive; } set { _isActive = value; OnPropertyChanged(); } }

        private Teacher _teacher = null!;
        public Teacher Teacher { get { return _teacher; } set { _teacher = value; OnPropertyChanged(); } }

        private Specialization _specialization = null!;
        public Specialization Specialization { get { return _specialization; } set { _specialization = value; OnPropertyChanged(); } }

        public bool CheckValid() {
            if (Teacher == null) return false;
            if (Specialization == null) return false;

            return true;
        }
    }
}
