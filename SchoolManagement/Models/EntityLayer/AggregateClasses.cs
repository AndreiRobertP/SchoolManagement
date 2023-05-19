using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Models.EntityLayer
{
    public class StudentAndGeneralMean
    {
        public Student Student { get; set; }
        public double MeanValue { get; set; }

        public StudentAndGeneralMean(Student student, double meanValue)
        {
            Student = student;
            MeanValue = meanValue;
        }
    }

    public class AwardedStudent
    {
        public Student Student { get; set; }
        public String Position { get; set; }

        public AwardedStudent(Student student, string position)
        {
            Student = student;
            Position = position;
        }
    }

    public class FlunkedStudent
    {
        public Student Student { get; set; }
        public String FlunkedSubject { get; set; }

        public FlunkedStudent(Student student, string flunkedSubject)
        {
            Student = student;
            FlunkedSubject = flunkedSubject;
        }
    }

    public class RepeaterStudent
    {
        public Student Student { get; set; }
        public String FlunkedSubjects { get; set; }

        public RepeaterStudent(Student student, string flunkedSubjects)
        {
            Student = student;
            FlunkedSubjects = flunkedSubjects;
        }
    }

    public class StudentShtAndMean
    {
        public Student Student { get; set; }
        public Sht Sht { get; set; }
        public double MeanValue { get; set; }

        public StudentShtAndMean(Student student, Sht sht, double meanValue)
        {
            Student = student;
            Sht = sht;
            MeanValue = meanValue;
        }
    }
}
