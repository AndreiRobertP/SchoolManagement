using SchoolManagement.Models.EntityLayer;
using SchoolManagement.ViewModels;
using System.Windows.Controls;

namespace SchoolManagement.Views
{
    /// <summary>
    /// Interaction logic for ManageSubjectsView.xaml
    /// </summary>
    public partial class ManageTeachersView : Page
    {
        public ManageTeachersVM ManageTeachersVM { get; set; }
        public ManageTeachersView()
        {
            InitializeComponent();

            ManageTeachersVM = (DataContext as ManageTeachersVM);
        }

        private void DtgTeachers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ManageTeachersVM.SelectedTeacher = (Teacher)DtgTeachers.SelectedItem;
        }
    }
}
