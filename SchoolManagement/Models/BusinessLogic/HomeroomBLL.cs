using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolManagement.Models.BusinessLogic
{
    public class HomeroomBLL
    {
        public ObservableCollection<Homeroom> GetHomerooms()
        {
            ObservableCollection<Homeroom> collection = new ObservableCollection<Homeroom>();
            using (var context = new SchoolManagementContext())
            {
                var Homerooms = context.Homerooms.Where(t => t.IsActive).Include(h => h.Teacher).Include(h => h.Specialization).ToList();

                foreach (var Homeroom in Homerooms)
                    collection.Add(Homeroom);
            }
            return collection;
        }

        public void AddHomeroom(Homeroom newHomeroom)
        {
            using (var context = new SchoolManagementContext())
            {
                var specialization = context.Specializations.Where(s => s.SpecializationId == newHomeroom.Specialization.SpecializationId).Single();
                var teacher = context.Teachers.Where(t => t.TeacherId == newHomeroom.Teacher.TeacherId && t.IsActive).Single();

                newHomeroom.Specialization = specialization;
                newHomeroom.Teacher = teacher;

                context.Homerooms.Add(newHomeroom);
                context.SaveChanges();
            }
        }

        public void UpdateHomeroom(Homeroom newHomeroom)
        {
            using (var context = new SchoolManagementContext())
            {
                context.Homerooms.Update(newHomeroom);
                context.SaveChanges();
            }
        }


        public void RemoveHomeroom(Homeroom deleteHomeroom)
        {
            using (var context = new SchoolManagementContext())
            {
                deleteHomeroom.IsActive = false;
                context.Homerooms.Update(deleteHomeroom);
                context.SaveChanges();
            }
        }
    }
}
