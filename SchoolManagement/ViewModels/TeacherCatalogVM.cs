using Microsoft.Win32;
using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SchoolManagement.Views;

namespace SchoolManagement.ViewModels
{
    public class TeacherCatalogVM : BasePropertyChanged
    {
        public GradeBLL GradeBLL { get; set; } = new GradeBLL();
        public AbsenceBLL AbsenceBLL { get; set; } = new AbsenceBLL();
        public ShtBLL ShtBLL { get; set; } = new ShtBLL();
        public StudentBLL StudentBLL { get; set; } = new StudentBLL();

        public ObservableCollection<Sht> Shts { get; set; } = new ObservableCollection<Sht>();
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
        public ObservableCollection<Grade> Grades { get; set; } = new ObservableCollection<Grade>();
        public ObservableCollection<Absence> Absences { get; set; } = new ObservableCollection<Absence>();

        private Grade _selectedGrade = null!;
        public Grade SelectedGrade
        {
            get { return _selectedGrade; }
            set
            {
                _selectedGrade = value;
                OnPropertyChanged();
            }
        }

        private Absence _selectedAbsence = null!;
        public Absence SelectedAbsence
        {
            get { return _selectedAbsence; }
            set
            {
                _selectedAbsence = value;
                OnPropertyChanged();
            }
        }

        private Sht _fieldSht = null!;
        public Sht FieldSht
        {
            get { return _fieldSht; }
            set
            {
                _fieldSht = value;
                OnPropertyChanged();

                if (value != null)
                    UpdateListOfStudents();
            }
        }

        private Student _fieldStudent = null!;
        public Student FieldStudent
        {
            get { return _fieldStudent; }
            set
            {
                _fieldStudent = value;
                OnPropertyChanged();

                if (value != null)
                {
                    UpdateListOfGrades();
                    UpdateListOfAbsences();
                }
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

                UpdateListOfGrades();
                UpdateListOfAbsences();
            }
        }

        public bool FieldSemesterBinary {
            get { return FieldSemester == 2; }
            set { FieldSemester = (value ? 1 : 0) + 1; }
        }

        public void UpdateListOfShts()
        {
            Shts.Clear();
            Students.Clear();
            Grades.Clear();
            Absences.Clear();


            foreach (Sht sht in ShtBLL.GetShts())
            { //TODO GetShts by Teacher
                Shts.Add(sht);
            }

            UpdateListOfStudents();
        }

        public void UpdateListOfStudents()
        {
            Students.Clear();
            Grades.Clear();
            Absences.Clear();

            if (FieldSht == null)
                return;

            foreach (Student student in StudentBLL.GetStudentsBySht(FieldSht))
            {
                Students.Add(student);
            }

            UpdateListOfGrades();
            UpdateListOfAbsences();
        }

        public void UpdateListOfGrades()
        {
            Grades.Clear();

            if (FieldSht == null)
                return;

            if (FieldStudent == null)
                return;

            foreach (Grade grade in GradeBLL.GetGradesByShtAndStudentAndSemester(FieldSht, FieldStudent, FieldSemester))
            {
                Grades.Add(grade);
            }
        }

        public void UpdateListOfAbsences()
        {
            Absences.Clear();

            if (FieldSht == null)
                return;

            if (FieldStudent == null)
                return;

            foreach (Absence absence in AbsenceBLL.GetAbsencesByShtAndStudentAndSemester(FieldSht, FieldStudent, FieldSemester))
            {
                Absences.Add(absence);
            }
        }

        public TeacherCatalogVM()
        {
            UpdateListOfShts();
        }

        //Commands
        private RelayCommand? _cmdAddGrade;
        public RelayCommand CmdAddGrade
        {
            get
            {
                return _cmdAddGrade ?? (_cmdAddGrade = new RelayCommand(
                    () =>
                    {
                        if (FieldSht == null)
                        {
                            MessageBox.Show("Nu exista clasa selectata");
                            return;
                        }

                        if (FieldStudent == null)
                        {
                            MessageBox.Show("Nu exista student selectat");
                            return;
                        }

                        Grade newGrade = new Grade
                        {
                            Sht = FieldSht,
                            Student = FieldStudent,
                            Value = 10,
                            GivenDate = DateTime.Today,
                            IsThesis = false,
                            IsActive = true,
                            Semester = FieldSemester
                        };

                        AddEditGradeView addEditGradeView = new AddEditGradeView();
                        addEditGradeView.Grade = newGrade;
                        addEditGradeView.ShowDialog();

                        UpdateListOfGrades();
                    }
                , () => true
                ));
            }
        }

        private RelayCommand? _cmdDeleteGrade;
        public RelayCommand CmdDeleteGrade
        {
            get
            {
                return _cmdDeleteGrade ?? (_cmdDeleteGrade = new RelayCommand(
                    () =>
                    {
                        if (SelectedGrade == null)
                            return;

                        GradeBLL.RemoveGrade(SelectedGrade);
                        UpdateListOfGrades();
                    }
                , () => true
                ));
            }
        }

        //Commands
        private RelayCommand? _cmdAddAbsence;
        public RelayCommand CmdAddAbsence
        {
            get
            {
                return _cmdAddAbsence ?? (_cmdAddAbsence = new RelayCommand(
                    () =>
                    {
                        if (FieldSht == null)
                        {
                            MessageBox.Show("Nu exista clasa selectata");
                            return;
                        }

                        if (FieldStudent == null)
                        {
                            MessageBox.Show("Nu exista student selectat");
                            return;
                        }

                        Absence newAbsence = new Absence
                        {
                            Sht = FieldSht,
                            Student = FieldStudent,
                            GivenDate = DateTime.Today,
                            IsActive = true,
                            Semester = FieldSemester
                        };

                        AddEditAbsenceView addEditAbsenceView = new AddEditAbsenceView();
                        addEditAbsenceView.Absence = newAbsence;
                        addEditAbsenceView.ShowDialog();

                        UpdateListOfAbsences();
                    }
                , () => true
                ));
            }
        }

        private RelayCommand? _cmdDeleteAbsence;
        public RelayCommand CmdDeleteAbsence
        {
            get
            {
                return _cmdDeleteAbsence ?? (_cmdDeleteAbsence = new RelayCommand(
                    () =>
                    {
                        if (SelectedAbsence == null)
                            return;

                        AbsenceBLL.RemoveAbsence(SelectedAbsence);
                        UpdateListOfAbsences();
                    }
                , () => true
                ));
            }
        }
    }
}
