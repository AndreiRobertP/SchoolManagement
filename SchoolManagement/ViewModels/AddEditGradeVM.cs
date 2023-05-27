using System;
using System.Collections.ObjectModel;
using System.Windows;
using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.Models.EntityLayer;

namespace SchoolManagement.ViewModels
{
    public class AddEditGradeVM : BasePropertyChanged
    {
        public ObservableCollection<int> Values { get; set; } = new ObservableCollection<int>(){1,2,3,4,5,6,7,8,9,10};
        public GradeBLL GradeBll { get; set; } = new GradeBLL();

        private Grade _selectedGrade;
        public Grade SelectedGrade
        {
            get { return _selectedGrade; }
            set
            {
                _selectedGrade = value;
                OnPropertyChanged();

                UpdateFieldFromSelected();
            }
        }

        public void UpdateFieldFromSelected()
        {
            if (SelectedGrade == null)
                return;

            FieldActive = SelectedGrade.IsActive;
            FieldThesis = SelectedGrade.IsThesis;
            FieldDate = SelectedGrade.GivenDate;
            FieldValue = SelectedGrade.Value;

            OnPropertyChanged(nameof(FieldName));
            OnPropertyChanged(nameof(FieldSemester));
            OnPropertyChanged(nameof(FieldSubject));
        }

        public string FieldName
        {
            get{
                if (SelectedGrade == null) return "";
                if (SelectedGrade.Student == null) return "";
                return "Elev: " + SelectedGrade.Student.Name;
            }
        }

        public string FieldSemester
        {
            get
            {
                if (SelectedGrade == null) return "";
                return "Semestru: "+ SelectedGrade.Semester.ToString();
            }
        }

        public string FieldSubject
        {
            get
            {
                if (SelectedGrade == null) return "";
                if (SelectedGrade.Sht == null) return "";
                if (SelectedGrade.Sht.Subject == null) return "";
                return "Materie: " + SelectedGrade.Sht.Subject.NameSubject;
            }
        }

        private DateTime _fieldDate;
        public DateTime FieldDate
        {
            get { return _fieldDate; }
            set
            {
                _fieldDate = value;
                OnPropertyChanged();
            }
        }

        private int _fieldValue;
        public int FieldValue
        {
            get { return _fieldValue; }
            set
            {
                _fieldValue = value;
                OnPropertyChanged();
            }
        }

        private bool _fieldThesis;
        public bool FieldThesis
        {
            get { return _fieldThesis; }
            set
            {
                _fieldThesis = value;
                OnPropertyChanged();
            }
        }

        private bool _fieldActive;
        public bool FieldActive
        {
            get { return _fieldActive; }
            set
            {
                _fieldActive = value;
                OnPropertyChanged();
            }
        }

        public void SaveChanges()
        {
            SelectedGrade.GivenDate = FieldDate;
            SelectedGrade.Value = FieldValue;
            SelectedGrade.IsThesis = FieldThesis;
            SelectedGrade.IsActive = FieldActive;

            if (!SelectedGrade.CheckValid())
            {
                return;
            }

            try
            {
                if (SelectedGrade.GradeId == 0)
                    GradeBll.AddGrade(SelectedGrade);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Nu s-a putut adauga nota", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
