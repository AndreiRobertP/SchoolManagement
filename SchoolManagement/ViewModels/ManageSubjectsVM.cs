using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagement.ViewModels
{
    public class ManageSubjectsVM : BasePropertyChanged
    {
        public SubjectsBLL SubjectBLL { get; set; } = new SubjectsBLL();


        public ObservableCollection<Subject> Subjects { get; set; } = new ObservableCollection<Subject>();

        private Subject _selectedSubject;
        public Subject SelectedSubject
        {
            get { return _selectedSubject; }
            set
            {
                _selectedSubject = value;
                OnPropertyChanged();

                UpdateFieldFromSelected();
            }
        }

        private void UpdateFieldFromSelected()
        {
            if (SelectedSubject == null)
                return;

            FieldNameSubject = SelectedSubject.NameSubject;
        }

        private void UpdateSelectedFromField()
        {
            if (SelectedSubject == null)
                return;

            SelectedSubject.NameSubject = FieldNameSubject;
        }

        private Subject NewFromField()
        {
            return new Subject
            {
                NameSubject = FieldNameSubject,
                IsActive = true
            };
        }

        private string _fieldNameSubject = "";
        public string FieldNameSubject
        {
            get { return _fieldNameSubject; }
            set
            {
                _fieldNameSubject = value;
                OnPropertyChanged();
            }
        }

        public ManageSubjectsVM()
        {
            UpdateListOfItems();
        }

        public void UpdateListOfItems()
        {
            Subjects.Clear();

            foreach (Subject Subject in SubjectBLL.GetSubjects())
            {
                Subjects.Add(Subject);
            }
        }

        //Commands
        private RelayCommand _cmdAdd;
        public RelayCommand CmdAdd
        {
            get
            {
                return _cmdAdd ?? (_cmdAdd = new RelayCommand(
                    () =>
                    {
                        foreach (var Subject in Subjects)
                        {
                            if (Subject.NameSubject == FieldNameSubject)
                            {
                                MessageBox.Show("Exista deja aceasta specializare");
                                return;
                            }
                        }

                        Subject tmpNew = NewFromField();
                        if (!tmpNew.CheckValid())
                            return;

                        SubjectBLL.AddSubject(tmpNew);
                        UpdateListOfItems();
                    }
                , () => true
                ));
            }
        }

        private RelayCommand _cmdEdit;
        public RelayCommand CmdEdit
        {
            get
            {
                return _cmdEdit ?? (_cmdEdit = new RelayCommand(
                    () =>
                    {
                        if (SelectedSubject == null)
                            return;

                        if (!SelectedSubject.CheckValid())
                            return;

                        foreach (var Subject in Subjects)
                        {
                            if (Subject.NameSubject == FieldNameSubject && Subject.SubjectId != SelectedSubject.SubjectId)
                            {
                                MessageBox.Show("Exista deja aceasta materie");
                                return;
                            }
                        }

                        UpdateSelectedFromField();
                        SubjectBLL.UpdateSubject(SelectedSubject);
                        UpdateListOfItems();
                    }
                , () => true
                ));
            }
        }

        private RelayCommand _cmdDelete;
        public RelayCommand CmdDelete
        {
            get
            {
                return _cmdDelete ?? (_cmdDelete = new RelayCommand(
                    () =>
                    {
                        if (SelectedSubject == null)
                            return;

                        SubjectBLL.RemoveSubject(SelectedSubject);
                        UpdateListOfItems();
                    }
                , () => true
                ));
            }
        }
    }
}
