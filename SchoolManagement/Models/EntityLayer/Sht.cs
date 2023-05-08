using SchoolManagement.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.EntityLayer
{
    public class Sht : BasePropertyChanged
    {
        private int _shtId;
        [Key]
        public int ShtId { get{return _shtId; } set{ _shtId = value; OnPropertyChanged();} }

        private bool _hasThesis = false;
        public bool HasThesis { get{return _hasThesis; } set{ _hasThesis = value; OnPropertyChanged();} }

        private bool _isActive = true;
        public bool IsActive { get{return _isActive; } set{ _isActive = value; OnPropertyChanged();} }

        //Relations
        private Subject _subject = null!;
        public Subject Subject { get{return _subject; } set{ _subject = value; OnPropertyChanged();} }

        private Homeroom _homeroom = null!;
        public Homeroom Homeroom { get{return _homeroom; } set{ _homeroom = value; OnPropertyChanged();} }

        private Teacher _teacher = null!;
        public Teacher Teacher { get{return _teacher; } set{ _teacher = value; OnPropertyChanged();} } 
    }
}
