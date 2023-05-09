using SchoolManagement.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.EntityLayer
{
    public class Subject: BasePropertyChanged
    {
        private int _subjectId;
        [Key]
        public int SubjectId { get { return _subjectId; } set { _subjectId = value; OnPropertyChanged(); } }

        private string _nameSubject = "NewSubject";
        public string NameSubject { get { return _nameSubject; } set { _nameSubject = value; OnPropertyChanged(); } }

        private bool _isActive = true;
        public bool IsActive { get { return _isActive; } set { _isActive = value; OnPropertyChanged(); } }

        public bool CheckValid()
        {
            return true;
        }
    }
}
