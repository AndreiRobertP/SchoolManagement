using SchoolManagement.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.EntityLayer
{
    public class Specialization: BasePropertyChanged
    {
        private int _SpecializationId;
        [Key]
        public int SpecializationId { get { return _SpecializationId; } set { _SpecializationId = value; OnPropertyChanged(); } }

        private string _nameSpecialization = "";
        public string NameSpecialization { get { return _nameSpecialization; } set { _nameSpecialization = value; OnPropertyChanged(); } }

        private bool _isActive = true;
        public bool IsActive { get { return _isActive; } set { _isActive = value; OnPropertyChanged(); } }

        public bool CheckValid()
        {
            return true;
        }
    }
}
