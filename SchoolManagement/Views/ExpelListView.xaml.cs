using System.Windows.Controls;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Views
{
    /// <summary>
    /// Interaction logic for ExpelListView.xaml
    /// </summary>
    public partial class ExpelListView : Page
    {
        public ExpelListVM ExpelListVm { get; set; }

        public ExpelListView()
        {
            InitializeComponent();

            ExpelListVm = DataContext as ExpelListVM;
        }
    }
}
