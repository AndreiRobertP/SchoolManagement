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
using SchoolManagement.Models.EntityLayer;
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
