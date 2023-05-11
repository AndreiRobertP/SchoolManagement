using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Models.BusinessLogic
{
    public class GradeBLL
    {
        public ObservableCollection<Grade> GetGrades()
        {
            ObservableCollection<Grade> collection = new ObservableCollection<Grade>();
            using (var context = new SchoolManagementContext())
            {
                var Grades = context.Grades.ToList();

                foreach (var Grade in Grades)
                    collection.Add(Grade);
            }
            return collection;
        }

        public void AddGrade(Grade newGrade)
        {
            using (var context = new SchoolManagementContext())
            {
                Sht sht = context.Shts.Where(s => s.ShtId == newGrade.Sht.ShtId && s.IsActive).Single();
                Student student = context.Students.Where(s => s.StudentId == newGrade.Student.StudentId && s.IsActive).Single();

                newGrade.Sht = sht;
                newGrade.Student = student;

                context.Grades.Add(newGrade);
                context.SaveChanges();
            }
        }

        public void RemoveGrade(Grade deleteGrade)
        {
            using (var context = new SchoolManagementContext())
            {
                Sht sht = context.Shts.Where(s => s.ShtId == deleteGrade.Sht.ShtId).Single();
                Student student = context.Students.Where(s => s.StudentId == deleteGrade.Student.StudentId).Single();

                deleteGrade.Sht = sht;
                deleteGrade.Student = student;
                deleteGrade.IsActive = false;

                context.Update(deleteGrade);
                context.SaveChanges();
            }
        }

        public ObservableCollection<Grade> GetGradesByShtAndStudentAndSemester(Sht sht, Student student, int semester)
        {
            ObservableCollection<Grade> collection = new ObservableCollection<Grade>();
            using (var context = new SchoolManagementContext())
            {
                var Grades = context.Grades.Where(f => f.Sht.ShtId == sht.ShtId && f.Student.StudentId == student.StudentId && f.Semester == semester).ToList();

                foreach (var Grade in Grades)
                    collection.Add(Grade);
            }
            return collection;
        }
    }
}
