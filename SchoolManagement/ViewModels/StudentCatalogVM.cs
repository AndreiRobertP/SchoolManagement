
using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.Models.EntityLayer;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolManagement.ViewModels
{
    public class StudentCatalogVM : BasePropertyChanged
    {
        public HomeroomBLL HomeroomBLL { get; set; } = new HomeroomBLL();
        public GradeBLL GradeBLL { get; set; } = new GradeBLL();
        public AbsenceBLL AbsenceBLL { get; set; } = new AbsenceBLL();
        public ShtBLL ShtBLL { get; set; } = new ShtBLL();
        public MeanBLL MeanBLL { get; set; } = new MeanBLL();


        public ObservableCollection<Sht> Shts { get; set; } = new ObservableCollection<Sht>();
        public ObservableCollection<Grade> Grades { get; set; } = new ObservableCollection<Grade>();
        public ObservableCollection<Absence> Absences { get; set; } = new ObservableCollection<Absence>();
        public ObservableCollection<Mean> MeansStudent { get; set; } = new ObservableCollection<Mean>();

        private Homeroom _fieldHomeroom = null!;
        public Homeroom FieldHomeroom
        {
            get { return _fieldHomeroom; }
            set
            {
                _fieldHomeroom = value;
                OnPropertyChanged();

                if (value != null)
                    UpdateBase();
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
                    FieldHomeroom = HomeroomBLL.GetHomeroomsByStudent(value);
                }
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
                {
                    UpdateDependent();
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

                UpdateDependent();
            }
        }

        public bool FieldSemesterBinary
        {
            get { return FieldSemester == 2; }
            set { FieldSemester = (value ? 1 : 0) + 1; }
        }

        private void UpdateBase()
        {
            UpdateShts();
            UpdateMeans();
        }

        private void UpdateShts()
        {
            Shts.Clear();

            if (FieldHomeroom == null)
                return;

            if (FieldStudent == null)
                return;


            foreach (Sht Sht in ShtBLL.GetShtsByHomeroom(FieldHomeroom))
            {
                Shts.Add(Sht);
            }
        }

        private void UpdateDependent()
        {
            UpdateGrades();
            UpdateListOfAbsences();

            OnPropertyChanged(nameof(StatsShtSemI));
            OnPropertyChanged(nameof(StatsShtSemII));
        }

        private void UpdateGrades()
        {
            Grades.Clear();

            if (FieldSht == null!)
                return;

            if (FieldStudent == null)
                return;

            foreach (Grade Grade in GradeBLL.GetGradesByShtAndStudentAndSemester(FieldSht, FieldStudent, FieldSemester))
            {
                Grades.Add(Grade);
            }
        }

        public void UpdateListOfAbsences()
        {
            Absences.Clear();

            if (FieldSht == null!)
                return;

            if (FieldStudent == null)
                return;

            foreach (Absence Absence in AbsenceBLL.GetAbsencesByShtAndStudentAndSemester(FieldSht, FieldStudent, FieldSemester))
            {
                Absences.Add(Absence);
            }
        }

        public void UpdateMeans()
        {
            if (FieldStudent == null)
                return;

            MeansStudent.Clear();
            foreach (Mean Mean in MeanBLL.GetMeansByStudent(FieldStudent))
            {
                MeansStudent.Add(Mean);
            }

            OnPropertyChanged(nameof(StatsShtSemI));
            OnPropertyChanged(nameof(StatsShtSemII));

            OnPropertyChanged(nameof(StatsGeneralSemI));
            OnPropertyChanged(nameof(StatsGeneralSemII));
            OnPropertyChanged(nameof(StatsGeneralYear));
        }

        public string StatsShtSemI
        {
            get
            {

                if (FieldSht == null)
                    return "[Materie neselectata]";

                if (FieldStudent == null)
                    return "[Elev neselectat]";

                Mean? mean = MeansStudent.SingleOrDefault(m => m.Sht.ShtId == FieldSht.ShtId && m.Semester == 1);

                if (mean == null)
                    return "[Neincheiat]";

                return mean.Value.ToString();
            }
        }

        public string StatsShtSemII
        {
            get
            {

                if (FieldSht == null)
                    return "[Materie neselectata]";

                if (FieldStudent == null)
                    return "[Elev neselectat]";


                Mean? mean = MeansStudent.SingleOrDefault(m => m.Sht.ShtId == FieldSht.ShtId && m.Semester == 2);

                if (mean == null)
                    return "[Neincheiat]";

                return mean.Value.ToString();
            }
        }

        private bool AreAllShtsClosedSemester(int semester)
        {
            foreach (var sht in Shts)
            {
                if (!MeansStudent.Any(m => m.Sht.ShtId == sht.ShtId && m.Semester == 1))
                    return false;
            }

            return true;
        }

        public string StatsGeneralSemI
        {
            get
            {
                if (FieldStudent == null)
                    return "[Student neselectat]";

                if (!AreAllShtsClosedSemester(1))
                    return "[Materii neincheiate]";

                if (!MeansStudent.Any())
                    return "[Nu exista materii incheiate]";

                double semI = MeansStudent.Where(m => m.Semester == 1).Average(m => m.Value);
                semI = Math.Round(semI, 2, MidpointRounding.AwayFromZero);
                return semI.ToString();
            }
        }

        public string StatsGeneralSemII
        {
            get
            {
                if (FieldStudent == null)
                    return "[Student neselectat]";

                if (!AreAllShtsClosedSemester(1))
                    return "[Materii neincheiate]";

                if (!MeansStudent.Any())
                    return "[Nu exista materii incheiate]";

                double semII = MeansStudent.Where(m => m.Semester == 2).Average(m => m.Value);
                semII = Math.Round(semII, 2, MidpointRounding.AwayFromZero);
                return semII.ToString();
            }
        }

        public string StatsGeneralYear
        {
            get
            {
                if (FieldStudent == null)
                    return "[Student neselectat]";

                if (!AreAllShtsClosedSemester(1))
                    return "[Materii neincheiate]";

                if (!MeansStudent.Any())
                    return "[Nu exista materii incheiate]";

                double semI = MeansStudent.Where(m => m.Semester == 1).Average(m => m.Value);
                semI = Math.Round(semI, 2, MidpointRounding.AwayFromZero);


                double semII = MeansStudent.Where(m => m.Semester == 2).Average(m => m.Value);
                semII = Math.Round(semII, 2, MidpointRounding.AwayFromZero);

                double year = (semI + semII) * 0.5;
                year = Math.Round(year, 2, MidpointRounding.AwayFromZero);
                return year.ToString();
            }
        }
    }
}
