using Microsoft.EntityFrameworkCore;
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
                Homeroom homeroom = context.Homerooms.Where(h => h.HomeroomId == newStudent.HomeroomId).First();

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
    }
}
