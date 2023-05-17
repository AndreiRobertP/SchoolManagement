using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolManagement.Models.BusinessLogic
{
    public class SubjectsBLL
    {
        public ObservableCollection<Subject> GetSubjects()
        {
            ObservableCollection<Subject> collection = new ObservableCollection<Subject>();
            using (var context = new SchoolManagementContext())
            {
                var Subjects = context.Subjects.Where(s => s.IsActive).ToList();

                foreach (var Subject in Subjects)
                    collection.Add(Subject);
            }
            return collection;
        }

        public void AddSubject(Subject newSubject)
        {
            using (var context = new SchoolManagementContext())
            {
                context.Subjects.Add(newSubject);
                context.SaveChanges();
            }
        }

        public void UpdateSubject(Subject newSubject)
        {
            using (var context = new SchoolManagementContext())
            {
                context.Subjects.Update(newSubject);
                context.SaveChanges();
            }
        }


        public void RemoveSubject(Subject deleteSubject)
        {
            using (var context = new SchoolManagementContext())
            {
                deleteSubject.IsActive = false;
                context.Subjects.Update(deleteSubject);
                context.SaveChanges();
            }
        }
    }
}
