using System.Windows;
using SchoolManagement.Models.EntityLayer;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Views
{
    /// <summary>
    /// Interaction logic for AddEditGrade.xaml
    /// </summary>
    public partial class AddEditAbsenceView : Window
    {
        public AddEditAbsenceVM AddEditAbsenceVm { get; set; }

        public AddEditAbsenceView()
        {
            InitializeComponent();
            AddEditAbsenceVm = DataContext as AddEditAbsenceVM;
        }

        public Absence Absence
        {
            get => AddEditAbsenceVm.SelectedAbsence;
            set
            {
                AddEditAbsenceVm.SelectedAbsence = value;
            }
        }

        private void BtnConfirm_OnClick(object sender, RoutedEventArgs e)
        {
            AddEditAbsenceVm.SaveChanges();
            this.DialogResult = true;
        }
    }
}
