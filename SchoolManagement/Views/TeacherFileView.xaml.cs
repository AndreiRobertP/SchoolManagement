using System.Windows.Controls;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Views
{
    /// <summary>
    /// Interaction logic for TeacherFileView.xaml
    /// </summary>
    public partial class TeacherFileView : Page
    {
        public TeacherFileVM TeacherFileVM { get; set; }

        public TeacherFileView()
        {
            InitializeComponent();

            TeacherFileVM = DataContext as TeacherFileVM;
        }
    }
}
