using SchoolManagement.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models.EntityLayer
{
    public class File: BasePropertyChanged
    {
        private int _fileId;
        [Key]
        public int FileId { get { return _fileId; } set { _fileId = value; OnPropertyChanged(); } }

        private string _filename = "";
        public string Filename { get { return _filename; } set { _filename = value; OnPropertyChanged(); } }

        private DateTime _givenDate;
        public DateTime GivenDate { get { return _givenDate; } set { _givenDate = value; OnPropertyChanged(); } }

        private byte[] _binarydata = null!;
        public byte[] Binarydata { get { return _binarydata; } set { _binarydata = value; OnPropertyChanged(); } }

        private Sht _sht = null!;
        public Sht Sht { get { return _sht; } set { _sht = value; OnPropertyChanged(); } }

        public bool CheckValid()
        {
            if (Sht == null) return false;

            return true;
        }
    }
}
