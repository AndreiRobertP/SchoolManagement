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
    public class ManageHomeroomsVM : BasePropertyChanged
    {
        public HomeroomBLL HomeroomBLL { get; set; } = new HomeroomBLL();
        public TeacherBLL TeacherBLL { get; set; } = new TeacherBLL();
        public SpecializationBLL SpecializationBLL { get; set; } = new SpecializationBLL();

        public ObservableCollection<Homeroom> Homerooms { get; set; } = new ObservableCollection<Homeroom>();
        public ObservableCollection<Teacher> Teachers { get; set; } = new ObservableCollection<Teacher>();
        public ObservableCollection<Specialization> Specializations { get; set; } = new ObservableCollection<Specialization>();


        private Homeroom _selectedHomeroom;
        public Homeroom SelectedHomeroom
        {
            get { return _selectedHomeroom; }
            set
            {
                _selectedHomeroom = value;
                OnPropertyChanged();

                UpdateFieldFromSelected();
            }
        }

        private void UpdateFieldFromSelected()
        {
            if (SelectedHomeroom == null)
                return;

            FieldNameHomeroom = SelectedHomeroom.NameHomeroom;
            FieldYear = SelectedHomeroom.Year;
            FieldTeacher = SelectedHomeroom.Teacher;
            FieldSpecialization = SelectedHomeroom.Specialization;
        }

        private void UpdateSelectedFromField()
        {
            if (SelectedHomeroom == null)
                return;

            SelectedHomeroom.NameHomeroom = FieldNameHomeroom;
            SelectedHomeroom.Year = FieldYear;
            SelectedHomeroom.Teacher = FieldTeacher;
            SelectedHomeroom.Specialization = FieldSpecialization;
        }

        private Homeroom NewFromField()
        {
            return new Homeroom
            {
                NameHomeroom = FieldNameHomeroom,
                Year = FieldYear,
                Teacher = FieldTeacher,
                Specialization = FieldSpecialization,
                IsActive = true
            };
        }

        public ManageHomeroomsVM()
        {
            UpdateListOfItems();
        }

        public void UpdateListOfItems()
        {
            Homerooms.Clear();
            foreach (Homeroom homeroom in HomeroomBLL.GetHomerooms())
            {
                Homerooms.Add(homeroom);
            }

            Specializations.Clear();
            foreach (Specialization Specialization in SpecializationBLL.GetSpecializations())
            {
                Specializations.Add(Specialization);
            }

            Teachers.Clear();
            foreach (Teacher Teacher in TeacherBLL.GetTeachers())
            {
                Teachers.Add(Teacher);
            }
        }

        private string _fieldNameHomeroom = "";
        public string FieldNameHomeroom
        {
            get { return _fieldNameHomeroom; }
            set
            {
                _fieldNameHomeroom = value;
                OnPropertyChanged();
            }
        }

        private int _fieldYear = 0;
        public int FieldYear
        {
            get { return _fieldYear; }
            set
            {
                _fieldYear = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FieldYearStr));
            }
        }

        private Teacher _fieldTeacher = null!;
        public Teacher FieldTeacher
        {
            get { return _fieldTeacher; }
            set
            {
                _fieldTeacher = value;
                OnPropertyChanged();
            }
        }

        private Specialization _fieldSpecialization = null!;
        public Specialization FieldSpecialization
        {
            get { return _fieldSpecialization; }
            set
            {
                _fieldSpecialization = value;
                OnPropertyChanged();
            }
        }

        public string FieldYearStr
        {
            get { return _fieldYear.ToString(); }
            set
            {
                try
                {
                    _fieldYear = int.Parse(value);
                }
                catch
                {
                    MessageBox.Show("Anul poate fi decat un numar intreg");
                }
                finally
                {
                    OnPropertyChanged();
                }
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
                        foreach (var Homeroom in Homerooms)
                        {
                            if (Homeroom.NameHomeroom == FieldNameHomeroom)
                            {
                                MessageBox.Show("Exista deja aceasta clasa");
                                return;
                            }
                        }

                        HomeroomBLL.AddHomeroom(NewFromField());
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
                        if (SelectedHomeroom == null)
                            return;

                        foreach (var Homeroom in Homerooms)
                        {
                            if (Homeroom.NameHomeroom == FieldNameHomeroom && Homeroom.HomeroomId != SelectedHomeroom.HomeroomId)
                            {
                                MessageBox.Show("Exista deja aceasta clasa");
                                return;
                            }
                        }

                        UpdateSelectedFromField();
                        HomeroomBLL.UpdateHomeroom(SelectedHomeroom);
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
                        if (SelectedHomeroom == null)
                            return;

                        HomeroomBLL.RemoveHomeroom(SelectedHomeroom);
                        UpdateListOfItems();
                    }
                , () => true
                ));
            }
        }
    }
}
