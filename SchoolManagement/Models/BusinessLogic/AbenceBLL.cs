using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Models.BusinessLogic
{
    public class AbsenceBLL
    {
        public ObservableCollection<Absence> GetAbsences()
        {
            ObservableCollection<Absence> collection = new ObservableCollection<Absence>();
            using (var context = new SchoolManagementContext())
            {
                var Absences = context.Absences.Include(g => g.Student).Include(g => g.Sht).ToList();

                foreach (var Absence in Absences)
                    collection.Add(Absence);
            }
            return collection;
        }

        public void AddAbsence(Absence newAbsence)
        {
            using (var context = new SchoolManagementContext())
            {
                Sht sht = context.Shts.Where(s => s.ShtId == newAbsence.Sht.ShtId && s.IsActive).Single();
                Student student = context.Students.Where(s => s.StudentId == newAbsence.Student.StudentId && s.IsActive).Single();

                newAbsence.Sht = sht;
                newAbsence.Student = student;

                context.Absences.Add(newAbsence);
                context.SaveChanges();
            }
        }

        public void RemoveAbsence(Absence deleteAbsence)
        {
            using (var context = new SchoolManagementContext())
            {
                Sht sht = context.Shts.Where(s => s.ShtId == deleteAbsence.Sht.ShtId).Single();
                Student student = context.Students.Where(s => s.StudentId == deleteAbsence.Student.StudentId).Single();

                deleteAbsence.Sht = sht;
                deleteAbsence.Student = student;
                deleteAbsence.IsActive = false;

                context.Update(deleteAbsence);
                context.SaveChanges();
            }
        }

        public ObservableCollection<Absence> GetAbsencesByShtAndStudentAndSemester(Sht sht, Student student, int semester)
        {
            ObservableCollection<Absence> collection = new ObservableCollection<Absence>();
            using (var context = new SchoolManagementContext())
            {
                var Absences = context.Absences.Where(f => f.Sht.ShtId == sht.ShtId && f.Student.StudentId == student.StudentId && f.Semester == semester).Include(g => g.Student).Include(g => g.Sht).ToList();

                foreach (var Absence in Absences)
                    collection.Add(Absence);
            }
            return collection;
        }

        public ObservableCollection<Absence> GetAbsencesStudentAndSemester(Student student, int semester)
        {
            ObservableCollection<Absence> collection = new ObservableCollection<Absence>();
            using (var context = new SchoolManagementContext())
            {
                var Absences = context.Absences.Where(f => f.Student.StudentId == student.StudentId && f.Semester == semester).Include(g => g.Student).Include(g => g.Sht).ToList();

                foreach (var Absence in Absences)
                    collection.Add(Absence);
            }
            return collection;
        }
    }
}
