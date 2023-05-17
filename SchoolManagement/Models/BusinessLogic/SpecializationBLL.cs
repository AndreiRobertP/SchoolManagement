using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolManagement.Models.BusinessLogic
{
    public class SpecializationBLL
    {
        public ObservableCollection<Specialization> GetSpecializations()
        {
            ObservableCollection<Specialization> collection = new ObservableCollection<Specialization>();
            using (var context = new SchoolManagementContext())
            {
                var Specializations = context.Specializations.Where(s => s.IsActive).ToList();

                foreach (var Specialization in Specializations)
                    collection.Add(Specialization);
            }
            return collection;
        }

        public void AddSpecialization(Specialization newSpecialization)
        {
            using (var context = new SchoolManagementContext())
            {
                context.Specializations.Add(newSpecialization);
                context.SaveChanges();
            }
        }

        public void UpdateSpecialization(Specialization newSpecialization)
        {
            using (var context = new SchoolManagementContext())
            {
                context.Specializations.Update(newSpecialization);
                context.SaveChanges();
            }
        }


        public void RemoveSpecialization(Specialization deleteSpecialization)
        {
            using (var context = new SchoolManagementContext())
            {
                deleteSpecialization.IsActive = false;
                context.Specializations.Update(deleteSpecialization);
                context.SaveChanges();
            }
        }
    }
}