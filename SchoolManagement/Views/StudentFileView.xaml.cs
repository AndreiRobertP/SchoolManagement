using System.Windows.Controls;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Views
{
    /// <summary>
    /// Interaction logic for StudentFileView.xaml
    /// </summary>
    public partial class StudentFileView : Page
    {
        public StudentFileVM StudentFileVM { get; set; }

        public StudentFileView()
        {
            InitializeComponent();

            StudentFileVM = DataContext as StudentFileVM;
        }
    }
}
