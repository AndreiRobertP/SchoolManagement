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
    public class ManageStudentsVM : BasePropertyChanged
    {

        public StudentBLL StudentBLL { get; set; } = new StudentBLL();
        public HomeroomBLL HomeroomBLL { get; set; } = new HomeroomBLL();

        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
        public ObservableCollection<Homeroom> Homerooms { get; set; } = new ObservableCollection<Homeroom>();

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();

                UpdateFieldFromSelected();
            }
        }

        private void UpdateFieldFromSelected()
        {
            if (SelectedStudent == null)
                return;

            FieldUsername = SelectedStudent.Username;
            FieldName = SelectedStudent.Name;

            try { FieldHomeroom = Homerooms.Where(t => t.HomeroomId == SelectedStudent.Homeroom.HomeroomId).Single(); }
            catch { FieldHomeroom = SelectedStudent.Homeroom; }
        }

        private void UpdateSelectedFromField()
        {
            if (SelectedStudent == null)
                return;

            SelectedStudent.Username = FieldUsername;
            SelectedStudent.Name = FieldName;
            SelectedStudent.Homeroom = FieldHomeroom;
        }

        private Student NewFromField()
        {
            return new Student
            {
                Name = FieldName,
                Username = FieldUsername,
                IsActive = true,
                Homeroom = FieldHomeroom
            };
        }

        private string _fieldUsername = "";
        public string FieldUsername
        {
            get { return _fieldUsername; }
            set
            {
                _fieldUsername = value;
                OnPropertyChanged();
            }
        }
        private string _fieldName = "";
        public string FieldName
        {
            get { return _fieldName; }
            set
            {
                _fieldName = value;
                OnPropertyChanged();
            }
        }

        private Homeroom _fieldHomeroom = null!;
        public Homeroom FieldHomeroom
        {
            get { return _fieldHomeroom; }
            set
            {
                _fieldHomeroom = value;
                OnPropertyChanged();
            }
        }

        public ManageStudentsVM()
        {
            UpdateListOfItems();
        }

        public void UpdateListOfItems()
        {
            Students.Clear();
            foreach (Student Student in StudentBLL.GetStudents())
            {
                Students.Add(Student);
            }

            Homerooms.Clear();
            foreach (Homeroom Homeroom in HomeroomBLL.GetHomerooms())
            {
                Homerooms.Add(Homeroom);
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
                        foreach (var Student in Students)
                        {
                            if (Student.Username == FieldUsername)
                            {
                                MessageBox.Show("Exista username");
                                return;
                            }
                        }

                        Student tmpNew = NewFromField();
                        if (!tmpNew.CheckValid())
                            return;

                        StudentBLL.AddStudent(tmpNew);
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
                        if (SelectedStudent == null)
                            return;

                        if (!SelectedStudent.CheckValid())
                            return;

                        foreach (var Student in Students)
                        {
                            if (Student.Username == FieldName && Student.StudentId != SelectedStudent.StudentId)
                            {
                                MessageBox.Show("Exista username");
                                return;
                            }
                        }

                        UpdateSelectedFromField();
                        StudentBLL.UpdateStudent(SelectedStudent);
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
                        if (SelectedStudent == null)
                            return;

                        StudentBLL.RemoveStudent(SelectedStudent);
                        UpdateListOfItems();
                    }
                , () => true
                ));
            }
        }
    }
}
