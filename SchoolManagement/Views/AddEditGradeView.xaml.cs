using System.Windows;
using SchoolManagement.Models.EntityLayer;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Views
{
    /// <summary>
    /// Interaction logic for AddEditGrade.xaml
    /// </summary>
    public partial class AddEditGradeView : Window
    {
        public AddEditGradeVM AddEditGradeVm { get; set; }

        public AddEditGradeView()
        {
            InitializeComponent();
            AddEditGradeVm = DataContext as AddEditGradeVM;
        }

        public Grade Grade
        {
            get => AddEditGradeVm.SelectedGrade;
            set
            {
                AddEditGradeVm.SelectedGrade = value;
            }
        }

        private void BtnConfirm_OnClick(object sender, RoutedEventArgs e)
        {
            AddEditGradeVm.SaveChanges();
            this.DialogResult = true;
        }
    }
}
