using SchoolManagement.Models.EntityLayer;
using SchoolManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
