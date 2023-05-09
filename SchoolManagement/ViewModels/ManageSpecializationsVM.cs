using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows;

namespace SchoolManagement.ViewModels
{
    public class ManageSpecializationsVM : BasePropertyChanged
    {
        public SpecializationBLL SpecializationBLL { get; set; } = new SpecializationBLL();


        public ObservableCollection<Specialization> Specializations { get; set; } = new ObservableCollection<Specialization>();

        private Specialization _selectedSpecialization;
        public Specialization SelectedSpecialization
        {
            get { return _selectedSpecialization; }
            set
            {
                _selectedSpecialization = value;
                OnPropertyChanged();

                UpdateFieldFromSelected();
            }
        }

        private void UpdateFieldFromSelected()
        {
            if (SelectedSpecialization == null)
                return;

            FieldNameSpecialization = SelectedSpecialization.NameSpecialization;
        }

        private void UpdateSelectedFromField()
        {
            if (SelectedSpecialization == null)
                return;

            SelectedSpecialization.NameSpecialization = FieldNameSpecialization;
        }

        private Specialization NewFromField()
        {
            return new Specialization
            {
                NameSpecialization = FieldNameSpecialization,
                IsActive = true
            };
        }

        private string _fieldNameSpecialization = "";
        public string FieldNameSpecialization
        {
            get { return _fieldNameSpecialization; }
            set
            {
                _fieldNameSpecialization = value;
                OnPropertyChanged();
            }
        }

        public ManageSpecializationsVM()
        {
            UpdateListOfItems();
        }

        public void UpdateListOfItems()
        {
            Specializations.Clear();

            foreach (Specialization Specialization in SpecializationBLL.GetSpecializations())
            {
                Specializations.Add(Specialization);
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
                        foreach (var Specialization in Specializations)
                        {
                            if (Specialization.NameSpecialization == FieldNameSpecialization)
                            {
                                MessageBox.Show("Exista deja aceasta specializare");
                                return;
                            }
                        }

                        Specialization tmpNew = NewFromField();
                        if (!tmpNew.CheckValid())
                            return;

                        SpecializationBLL.AddSpecialization(tmpNew);
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
                        if (SelectedSpecialization == null)
                            return;

                        if (!SelectedSpecialization.CheckValid())
                            return;

                        foreach (var Specialization in Specializations)
                        {
                            if (Specialization.NameSpecialization == FieldNameSpecialization && Specialization.SpecializationId != SelectedSpecialization.SpecializationId)
                            {
                                MessageBox.Show("Exista materia");
                                return;
                            }
                        }

                        UpdateSelectedFromField();
                        SpecializationBLL.UpdateSpecialization(SelectedSpecialization);
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
                        if (SelectedSpecialization == null)
                            return;

                        SpecializationBLL.RemoveSpecialization(SelectedSpecialization);
                        UpdateListOfItems();
                    }
                , () => true
                ));
            }
        }
    }
}
