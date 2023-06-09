﻿using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolManagement.ViewModels
{
    public class ExpelableStudent
    {
        public Student Student { get; set; }
        public int TotalAbsences { get; set; }
        public int NotMotivatedAbsences { get; set; }

        public ExpelableStudent(Student student, int totalAbsences, int notMotivatedAbsences)
        {
            Student = student;
            TotalAbsences = totalAbsences;
            NotMotivatedAbsences = notMotivatedAbsences;
        }
    }

    public class ExpelListVM : BasePropertyChanged
    {
        public AbsenceBLL AbsenceBLL { get; set; } = new AbsenceBLL();
        public StudentBLL StudentBLL { get; set; } = new StudentBLL();

        public ObservableCollection<ExpelableStudent> ExpelableStudents { get; set; } = new ObservableCollection<ExpelableStudent>();
        public int TotalAbsencesThreshold { get; } = 20;

        private Homeroom _fieldHomeroom = null!;
        public Homeroom FieldHomeroom
        {
            get { return _fieldHomeroom; }
            set
            {
                _fieldHomeroom = value;
                OnPropertyChanged();

                if (value != null)
                    UpdateStudents();
            }
        }

        private int _fieldSemester = 2;
        public int FieldSemester
        {
            get { return _fieldSemester; }
            set
            {
                _fieldSemester = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FieldSemesterBinary));

                UpdateStudents();
            }
        }

        public bool FieldSemesterBinary
        {
            get { return FieldSemester == 2; }
            set { FieldSemester = (value ? 2 : 1); }
        }

        public void UpdateStudents()
        {
            ExpelableStudents.Clear();

            if (FieldHomeroom == null)
                return;

            foreach (var student in StudentBLL.GetStudentsByHomeroom(FieldHomeroom))
            {
                var absences = AbsenceBLL.GetAbsencesStudentAndSemester(student, FieldSemester);

                int total = absences.Count();
                int notMotivated = absences.Count(a => a.IsActive);

                if (total > TotalAbsencesThreshold)
                    ExpelableStudents.Add(new ExpelableStudent(student, total, notMotivated));
            }
        }
    }
}
