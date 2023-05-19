using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagement.ViewModels
{
    public class HomeroomTeacherStatsVM : BasePropertyChanged
    {
        public ShtBLL ShtBLL { get; set; } = new ShtBLL();
        public StudentBLL StudentBLL { get; set; } = new StudentBLL();
        public MeanBLL MeanBLL { get; set; } = new MeanBLL();

        public ObservableCollection<StudentAndGeneralMean> StudentAndGeneralMeans { get; set; } = new();
        public ObservableCollection<AwardedStudent> AwardedStudents { get; set; } = new();
        public ObservableCollection<FlunkedStudent> FlunkedStudents { get; set; } = new();
        public ObservableCollection<RepeaterStudent> RepeaterStudents { get; set; } = new();

        private ObservableCollection<Student> Students { get; set; } = new();
        private ObservableCollection<Sht> Shts { get; set; } = new();
        private ObservableCollection<StudentShtAndMean> Means { get; set; } = new();

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

        private void UpdateBase()
        {
            Students = StudentBLL.GetStudentsByHomeroom(FieldHomeroom);
            Shts = ShtBLL.GetShtsByHomeroom(FieldHomeroom);

            foreach (var student in Students)
            {
                foreach (var sht in Shts)
                {
                    var first = MeanBLL.GetMeanByShtAndStudentAndSemester(sht, student, 1);
                    var second = MeanBLL.GetMeanByShtAndStudentAndSemester(sht, student, 2);

                    if (first == null || second == null)
                    {
                        MessageBox.Show(
                            $"Exista medii anuale neincheiate ( {student.Name} : {sht.Subject.NameSubject} )",
                            "Statistici indisponibile", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    Means.Add(new StudentShtAndMean(student, sht, Mean.ComputeYearlyMeanPerSht(first.Value, second.Value)));

                }
            }

            UpdateStudentAndGeneralMeans();
            UpdateAwardedStudents();
            UpdateFlunkedStudents();
            UpdateRepeaterStudents();
        }

        private void UpdateStudentAndGeneralMeans()
        {
            StudentAndGeneralMeans.Clear();

            foreach (var student in Students)
            {
                double[] yearlyMeansPerShtForStudent = Means.Where(m => m.Student == student).Select(m => m.MeanValue).ToArray();
                StudentAndGeneralMeans.Add(new StudentAndGeneralMean(student, Mean.ComputeGeneralMean(yearlyMeansPerShtForStudent)));
            }
        }

        private void UpdateAwardedStudents()
        {
            AwardedStudents.Clear();

            var firstAwardStudents = StudentAndGeneralMeans.Where(m => m.MeanValue >= 9.50);
            var secondAwardStudents = StudentAndGeneralMeans.Where(m => m.MeanValue >= 9.00 && m.MeanValue < 9.50);
            var thirdAwardStudents = StudentAndGeneralMeans.Where(m => m.MeanValue >= 8.50 && m.MeanValue < 9.00);
            var mentionAwardStudents = StudentAndGeneralMeans.Where(m => m.MeanValue >= 8.00 && m.MeanValue < 8.50);


            foreach (var studentAndMean in firstAwardStudents)
                AwardedStudents.Add(new AwardedStudent(studentAndMean.Student, "Premiul I"));

            foreach (var studentAndMean in secondAwardStudents)
                AwardedStudents.Add(new AwardedStudent(studentAndMean.Student, "Premiul II"));

            foreach (var studentAndMean in thirdAwardStudents)
                AwardedStudents.Add(new AwardedStudent(studentAndMean.Student, "Premiul III"));

            foreach (var studentAndMean in mentionAwardStudents)
                AwardedStudents.Add(new AwardedStudent(studentAndMean.Student, "Mentiune"));
        }

        private void UpdateFlunkedStudents()
        {
            FlunkedStudents.Clear();

            var flunkedStudents = Means.Where(m => m.MeanValue < 4.5);

            foreach (var studentShtAndMean in flunkedStudents)
            {
                FlunkedStudents.Add(new FlunkedStudent(studentShtAndMean.Student, studentShtAndMean.Sht.Subject.NameSubject));
            }
        }

        private void UpdateRepeaterStudents()
        {
            RepeaterStudents.Clear();

            foreach (var student in Students)
            {
                var flunkedSubjects = FlunkedStudents.Where(fs => fs.Student.StudentId == student.StudentId);
                if (flunkedSubjects.Count() >= 3)
                {
                    StringBuilder subjectsListBuilder = new StringBuilder();
                    foreach (var subject in flunkedSubjects)
                    {
                        subjectsListBuilder.Append(" | ");
                        subjectsListBuilder.Append(subject.FlunkedSubject);
                    }

                    RepeaterStudents.Add(new RepeaterStudent(student, subjectsListBuilder.ToString()));
                }
            }
        }
    }
}
