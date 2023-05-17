using System.Windows.Controls;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Views
{
    /// <summary>
    /// Interaction logic for HomeroomTeacherCatalogView.xaml
    /// </summary>
    public partial class HomeroomTeacherCatalogView : Page
    {
        public HomeroomTeacherCatalogVM HomeroomTeacherCatalogVM { get; set; }

        public HomeroomTeacherCatalogView()
        {
            InitializeComponent();

            HomeroomTeacherCatalogVM = DataContext as HomeroomTeacherCatalogVM;
        }
    }
}
