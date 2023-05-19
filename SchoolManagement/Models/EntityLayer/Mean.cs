using System;
using SchoolManagement.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SchoolManagement.Models.EntityLayer
{
    public class Mean : BasePropertyChanged
    {
        private int _meanId;
        [Key]
        public int MeanId { get { return _meanId; } set { _meanId = value; OnPropertyChanged(); } }

        private float _value;
        public float Value { get { return _value; } set { _value = value; OnPropertyChanged(); } }

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

            return true;
        }

        public static double ComputeYearlyMeanPerSht(double first, double second)
        {
            return Math.Round(first * 0.5 + second * 0.5, 2, MidpointRounding.AwayFromZero);
        }

        public static double ComputeGeneralMean(double[] yearlyMeansPerSht)
        {
            double avg = yearlyMeansPerSht.Average();
            return Math.Round(avg, 2, MidpointRounding.ToZero);
        }
    }
}
