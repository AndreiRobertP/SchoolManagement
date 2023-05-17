using System.Windows.Controls;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Views
{
    /// <summary>
    /// Interaction logic for TeacherCatalogView.xaml
    /// </summary>
    public partial class TeacherCatalogView : Page
    {
        public TeacherCatalogVM TeacherCatalogVm { get; set; }

        public TeacherCatalogView()
        {
            InitializeComponent();

            TeacherCatalogVm = DataContext as TeacherCatalogVM;
        }
    }
}
