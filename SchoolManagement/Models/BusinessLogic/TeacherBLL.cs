using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolManagement.Models.BusinessLogic
{
    public class TeacherBLL
    {
        public ObservableCollection<Teacher> GetTeachers()
        {
            ObservableCollection<Teacher> collection = new ObservableCollection<Teacher>();
            using (var context = new SchoolManagementContext())
            {
                var teachers = context.Teachers.Where(t => t.IsActive).ToList();

                foreach (var teacher in teachers)
                    collection.Add(teacher);
            }
            return collection;
        }

        public void AddTeacher(Teacher newTeacher)
        {
            using (var context = new SchoolManagementContext())
            {
                context.Teachers.Add(newTeacher);
                context.SaveChanges();
            }
        }

        public void UpdateTeacher(Teacher newTeacher)
        {
            using (var context = new SchoolManagementContext())
            {
                context.Teachers.Update(newTeacher);
                context.SaveChanges();
            }
        }


        public void RemoveTeacher(Teacher deleteTeacher)
        {
            using (var context = new SchoolManagementContext())
            {
                deleteTeacher.IsActive = false;
                context.Teachers.Update(deleteTeacher);
                context.SaveChanges();
            }
        }
    }
}
