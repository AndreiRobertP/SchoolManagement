using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchoolManagement.Models.BusinessLogic
{
    public class FileBLL
    {
        public ObservableCollection<File> GetFiles()
        {
            ObservableCollection<File> collection = new ObservableCollection<File>();
            using (var context = new SchoolManagementContext())
            {
                var Files = context.Files.ToList();

                foreach (var File in Files)
                    collection.Add(File);
            }
            return collection;
        }

        public void AddFile(File newFile)
        {
            using (var context = new SchoolManagementContext())
            {
                Sht sht = context.Shts.Where(s => s.ShtId == newFile.Sht.ShtId).Single();

                newFile.Sht = sht;

                context.Files.Add(newFile);
                context.SaveChanges();
            }
        }

        public void RemoveFile(File deleteFile)
        {
            using (var context = new SchoolManagementContext())
            {
                context.Remove(deleteFile);
                context.SaveChanges();
            }
        }

        public ObservableCollection<File> GetFilesBySht(Sht sht) {
            ObservableCollection<File> collection = new ObservableCollection<File>();
            using (var context = new SchoolManagementContext())
            {
                var Files = context.Files.Where(f => f.Sht.ShtId == sht.ShtId).ToList();

                foreach (var File in Files)
                    collection.Add(File);
            }
            return collection;
        }
    }
}
