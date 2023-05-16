using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolManagement.Models.BusinessLogic
{
    public class ShtBLL
    {
        public ObservableCollection<Sht> GetShts()
        {
            ObservableCollection<Sht> collection = new ObservableCollection<Sht>();
            using (var context = new SchoolManagementContext())
            {
                var Shts = context.Shts.Where(t => t.IsActive).Include(h => h.Teacher).Include(h => h.Subject).Include(h => h.Homeroom).ToList();

                foreach (var Sht in Shts)
                    collection.Add(Sht);
            }
            return collection;
        }

        public void AddSht(Sht newSht)
        {
            using (var context = new SchoolManagementContext())
            {
                var subject = context.Subjects.Where(s => s.SubjectId == newSht.Subject.SubjectId && s.IsActive).Single();
                var homeroom = context.Homerooms.Where(t => t.HomeroomId == newSht.Homeroom.HomeroomId && t.IsActive).Single();
                var teacher = context.Teachers.Where(t => t.TeacherId == newSht.Teacher.TeacherId && t.IsActive).Single();

                newSht.Subject = subject;
                newSht.Homeroom = homeroom;
                newSht.Teacher = teacher;

                context.Shts.Add(newSht);
                context.SaveChanges();
            }
        }

        public void UpdateSht(Sht newSht)
        {
            using (var context = new SchoolManagementContext())
            {
                //Quickfix
                var subject = context.Subjects.Where(s => s.SubjectId == newSht.Subject.SubjectId).Single();
                var homeroom = context.Homerooms.Where(t => t.HomeroomId == newSht.Homeroom.HomeroomId).Single();
                var teacher = context.Teachers.Where(t => t.TeacherId == newSht.Teacher.TeacherId).Single();

                newSht.Subject = subject;
                newSht.Homeroom = homeroom;
                newSht.Teacher = teacher;

                context.Shts.Update(newSht);
                context.SaveChanges();
            }
        }


        public void RemoveSht(Sht deleteSht)
        {
            using (var context = new SchoolManagementContext())
            {
                deleteSht.IsActive = false;
                context.Shts.Update(deleteSht);
                context.SaveChanges();
            }
        }

        public ObservableCollection<Sht> GetShtsByHomeroom(Homeroom homeroom)
        {
            ObservableCollection<Sht> collection = new ObservableCollection<Sht>();
            using (var context = new SchoolManagementContext())
            {
                var Shts = context.Shts.Where(t => t.IsActive && t.Homeroom.HomeroomId == homeroom.HomeroomId).Include(h => h.Teacher).Include(h => h.Subject).Include(h => h.Homeroom).Include(h => h.Homeroom.Specialization).ToList();

                foreach (var Sht in Shts)
                    collection.Add(Sht);
            }
            return collection;
        }

        public ObservableCollection<Sht> GetShtsByTeacher(Teacher teacher)
        {
            ObservableCollection<Sht> collection = new ObservableCollection<Sht>();
            using (var context = new SchoolManagementContext())
            {
                var Shts = context.Shts.Where(t => t.IsActive && t.Teacher.TeacherId == teacher.TeacherId).Include(h => h.Teacher).Include(h => h.Subject).Include(h => h.Homeroom).Include(h => h.Homeroom.Specialization).ToList();

                foreach (var Sht in Shts)
                    collection.Add(Sht);
            }
            return collection;
        }
    }
}
