using System;
using System.Collections.ObjectModel;
using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.Models.EntityLayer;

namespace SchoolManagement.ViewModels
{
    public class AddEditAbsenceVM : BasePropertyChanged
    {
        public AbsenceBLL AbsenceBll { get; set; } = new AbsenceBLL();

        private Absence _selectedAbsence;
        public Absence SelectedAbsence
        {
            get { return _selectedAbsence; }
            set
            {
                _selectedAbsence = value;
                OnPropertyChanged();

                UpdateFieldFromSelected();
            }
        }

        public void UpdateFieldFromSelected()
        {
            if (SelectedAbsence == null)
                return;

            FieldActive = SelectedAbsence.IsActive;
            FieldDate = SelectedAbsence.GivenDate;

            OnPropertyChanged(nameof(FieldName));
            OnPropertyChanged(nameof(FieldSemester));
            OnPropertyChanged(nameof(FieldSubject));
        }

        public string FieldName
        {
            get{
                if (SelectedAbsence == null) return "";
                if (SelectedAbsence.Student == null) return "";
                return "Elev: " + SelectedAbsence.Student.Name;
            }
        }

        public string FieldSemester
        {
            get
            {
                if (SelectedAbsence == null) return "";
                return "Semestru: "+ SelectedAbsence.Semester.ToString();
            }
        }

        public string FieldSubject
        {
            get
            {
                if (SelectedAbsence == null) return "";
                if (SelectedAbsence.Sht == null) return "";
                if (SelectedAbsence.Sht.Subject == null) return "";
                return "Materie: " + SelectedAbsence.Sht.Subject.NameSubject;
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
            SelectedAbsence.GivenDate = FieldDate;
            SelectedAbsence.IsActive = FieldActive;

            if (!SelectedAbsence.CheckValid())
            {
                return;
            }

            if(SelectedAbsence.AbsenceId == 0)
                AbsenceBll.AddAbsence(SelectedAbsence);
        }
    }
}
