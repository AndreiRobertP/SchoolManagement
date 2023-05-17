using System.Windows.Controls;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Views
{
    /// <summary>
    /// Interaction logic for StudentCatalog.xaml
    /// </summary>
    public partial class StudentCatalogView : Page
    {
        public StudentCatalogVM StudentCatalogVM { get; set; }

        public StudentCatalogView()
        {
            InitializeComponent();

            StudentCatalogVM = DataContext as StudentCatalogVM;
        }
    }
}
