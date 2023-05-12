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
using System.Windows.Shapes;
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
