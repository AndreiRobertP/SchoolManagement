using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SchoolManagement.ViewModels
{
    public class ManageShtsVM : BasePropertyChanged
    {
        public ShtBLL ShtBLL { get; set; } = new ShtBLL();
        public SubjectsBLL SubjectsBLL { get; set; } = new SubjectsBLL();
        public HomeroomBLL HomeroomBLL { get; set; } = new HomeroomBLL();
        public TeacherBLL TeacherBLL { get; set; } = new TeacherBLL();

        public ObservableCollection<Sht> Shts { get; set; } = new ObservableCollection<Sht>();
        public ObservableCollection<Homeroom> Homerooms { get; set; } = new ObservableCollection<Homeroom>();
        public ObservableCollection<Teacher> Teachers { get; set; } = new ObservableCollection<Teacher>();
        public ObservableCollection<Subject> Subjects { get; set; } = new ObservableCollection<Subject>();

        private Sht _selectedSht = null!;
        public Sht SelectedSht
        {
            get { return _selectedSht; }
            set
            {
                _selectedSht = value;
                OnPropertyChanged();

                UpdateFieldFromSelected(); //BUG -> Se reseteaza SelectedSht reactualizand FieldHomerooom
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

                if(value != null)
                    UpdateShtsByHomeroom(value);
            }
        }

        //When choosing a Sht, the avaliable Homerooms should change
        void UpdateShtsByHomeroom(Homeroom homeroom) {
            Shts.Clear();

            if (homeroom == null)
                return;

            foreach (var item in ShtBLL.GetShtsByHomeroom(homeroom)) {
                Shts.Add(item);
            }
        }

        private Subject _fieldSubject = null!;
        public Subject FieldSubject
        {
            get { return _fieldSubject; }
            set
            {
                _fieldSubject = value;
                OnPropertyChanged();
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

        private bool _fieldHasThesis = false;
        public bool FieldHasThesis
        {
            get { return _fieldHasThesis; }
            set
            {
                _fieldHasThesis = value;
                OnPropertyChanged();
            }
        }

        private void UpdateFieldFromSelected()
        {
            if (SelectedSht == null)
                return;

            FieldHasThesis = SelectedSht.HasThesis;

            try { FieldSubject = Subjects.Where(t => t.SubjectId == SelectedSht.Subject.SubjectId).Single(); }
            catch { FieldSubject = SelectedSht.Subject; }

            try { FieldTeacher = Teachers.Where(t => t.TeacherId == SelectedSht.Teacher.TeacherId).Single(); }
            catch { FieldTeacher = SelectedSht.Teacher; }
        }

        private void UpdateSelectedFromField()
        {
            if (SelectedSht == null)
                return;

            SelectedSht.Subject = FieldSubject;
            SelectedSht.Homeroom = FieldHomeroom;
            SelectedSht.Teacher = FieldTeacher;
            SelectedSht.HasThesis = FieldHasThesis;
        }

        private Sht NewFromField()
        {
            return new Sht
            {
                Subject = FieldSubject,
                Homeroom = FieldHomeroom,
                Teacher = FieldTeacher,
                HasThesis = FieldHasThesis,
                IsActive = true
            };
        }

        public ManageShtsVM()
        {
            UpdateListOfItems();
            UpdateListOfHomerooms();
        }

        public void UpdateListOfItems()
        {
            Subjects.Clear();
            foreach (Subject Subject in SubjectsBLL.GetSubjects())
            {
                Subjects.Add(Subject);
            }

            Teachers.Clear();
            foreach (Teacher Teacher in TeacherBLL.GetTeachers())
            {
                Teachers.Add(Teacher);
            }

            UpdateShtsByHomeroom(FieldHomeroom);
        }

        public void UpdateListOfHomerooms() {
            Homerooms.Clear();
            foreach (Homeroom homeroom in HomeroomBLL.GetHomerooms())
            {
                Homerooms.Add(homeroom);
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
                        foreach (var Sht in Shts)
                        {
                            if (Sht.Subject.SubjectId == FieldSubject.SubjectId)
                            {
                                MessageBox.Show("Exista deja aceasta materie");
                                return;
                            }
                        }
                        Sht tmpNew = NewFromField();
                        if (!tmpNew.CheckValid())
                            return;

                        ShtBLL.AddSht(tmpNew);
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
                        if (SelectedSht == null)
                            return;

                        if (!SelectedSht.CheckValid())
                            return;

                        foreach (var Sht in Shts)
                        {
                            if (Sht.Subject.SubjectId == FieldSubject.SubjectId && Sht.ShtId != SelectedSht.ShtId)
                            {
                                MessageBox.Show("Exista deja aceasta materie");
                                return;
                            }
                        }

                        UpdateSelectedFromField();
                        ShtBLL.UpdateSht(SelectedSht);
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
                        if (SelectedSht == null)
                            return;

                        ShtBLL.RemoveSht(SelectedSht);
                        UpdateListOfItems();
                    }
                , () => true
                ));
            }
        }
    }
}
