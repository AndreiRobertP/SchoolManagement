using Azure.Identity;
using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows;

namespace SchoolManagement.ViewModels
{
    public class ManageTeachersVM : BasePropertyChanged
    {
        public TeacherBLL TeacherBLL { get; set; } = new TeacherBLL();


        public ObservableCollection<Teacher> Teachers { get; set; } = new ObservableCollection<Teacher>();
        
        private Teacher _selectedTeacher;
        public Teacher SelectedTeacher
        {
            get { return _selectedTeacher; }
            set
            {
                _selectedTeacher = value;
                OnPropertyChanged();

                UpdateFieldFromSelected();
            }
        }

        private void UpdateFieldFromSelected() {
            if (SelectedTeacher == null)
                return;

            FieldUsername = SelectedTeacher.Username;
            FieldName = SelectedTeacher.Name;
        }

        private void UpdateSelectedFromField()
        {
            if (SelectedTeacher == null)
                return;

            SelectedTeacher.Username = FieldUsername;
            SelectedTeacher.Name = FieldName;
        }

        private Teacher NewFromField() {
            return new Teacher
            {
                Name = FieldName,
                Username = FieldUsername,
                IsActive = true
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

        public ManageTeachersVM()
        {
            UpdateListOfItems();
        }

        public void UpdateListOfItems()
        {
            Teachers.Clear();

            foreach (Teacher teacher in TeacherBLL.GetTeachers())
            {
                Teachers.Add(teacher);
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
                        foreach (var teacher in Teachers)
                        {
                            if (teacher.Username == FieldUsername)
                            {
                                MessageBox.Show("Exista username");
                                return;
                            }
                        }

                        TeacherBLL.AddTeacher(NewFromField());
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
                        if (SelectedTeacher == null)
                            return;

                        foreach (var teacher in Teachers)
                        {
                            if (teacher.Username == FieldName && teacher.TeacherId != SelectedTeacher.TeacherId)
                            {
                                MessageBox.Show("Exista username");
                                return;
                            }
                        }

                        UpdateSelectedFromField();
                        TeacherBLL.UpdateTeacher(SelectedTeacher);
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
                        if (SelectedTeacher == null)
                            return;

                        TeacherBLL.RemoveTeacher(SelectedTeacher);
                        UpdateListOfItems();
                    }
                , () => true
                ));
            }
        }
    }
}
