using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolManagement.Models.BusinessLogic
{
    public class StudentBLL
    {
        public ObservableCollection<Student> GetStudents()
        {
            ObservableCollection<Student> collection = new ObservableCollection<Student>();
            using (var context = new SchoolManagementContext())
            {
                var Students = context.Students.Where(t => t.IsActive).Include(t => t.Homeroom).ToList();

                foreach (var Student in Students)
                    collection.Add(Student);
            }
            return collection;
        }

        public void AddStudent(Student newStudent)
        {
            using (var context = new SchoolManagementContext())
            {
                Homeroom homeroom = context.Homerooms.Where(h => h.HomeroomId == newStudent.Homeroom.HomeroomId && h.IsActive).First();

                newStudent.Homeroom = homeroom;

                context.Students.Add(newStudent);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(Student newStudent)
        {
            using (var context = new SchoolManagementContext())
            {
                context.Students.Update(newStudent);
                context.SaveChanges();
            }
        }


        public void RemoveStudent(Student deleteStudent)
        {
            using (var context = new SchoolManagementContext())
            {
                deleteStudent.IsActive = false;
                context.Students.Update(deleteStudent);
                context.SaveChanges();
            }
        }

        public ObservableCollection<Student> GetStudentsBySht(Sht sht)
        {
            return GetStudentsByHomeroom(sht.Homeroom);
        }

        public ObservableCollection<Student> GetStudentsByHomeroom(Homeroom homeroom)
        {
            ObservableCollection<Student> collection = new ObservableCollection<Student>();
            using (var context = new SchoolManagementContext())
            {
                var Students = context.Students.Where(t => t.IsActive && t.Homeroom.HomeroomId == homeroom.HomeroomId).Include(t => t.Homeroom).ToList();

                foreach (var Student in Students)
                    collection.Add(Student);
            }
            return collection;
        }
    }
}
