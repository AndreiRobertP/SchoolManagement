using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.ViewModels;
using SchoolManagement.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SchoolManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //VMs
        public LoginVM LoginVM { get; set; }
        public MainWindowVM MainWindowViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel = DataContext as MainWindowVM;

            var loginView = new LoginView();
            frmMain.Content = loginView;

            LoginVM = loginView.LoginVM;
            LoginVM.PropertyChanged += OnLoginChange;
        }

        void OnLoginChange(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(LoginVM.UserPermisions)) {
                UserPermisions userPermisions = LoginVM.UserPermisions;

                SetVisibilityMenuRoles(userPermisions.IsRegistered);
                SetVisibilityAdmin(userPermisions.IsAdmin);
                SetVisibilityTeacher(userPermisions.IsTeacher);
                SetVisibilityHomeroomTeacher(userPermisions.IsHomeroomTeacher);
                SetVisibilityStudent(userPermisions.IsStudent);
            }
        }

        public void SetVisibilityMenuRoles(bool visibility) {
            MnuUserRolesMenu.Visibility = visibility == true ? Visibility.Visible : Visibility.Collapsed;
        }

        public void SetVisibilityAdmin(bool visibility)
        {
            MnuAdmin.Visibility = visibility == true ? Visibility.Visible : Visibility.Collapsed;
        }

        public void SetVisibilityTeacher(bool visibility)
        {
            MnuTeacher.Visibility = visibility == true ? Visibility.Visible : Visibility.Collapsed;
        }

        public void SetVisibilityHomeroomTeacher(bool visibility)
        {
            MnuHomeroomTeacher.Visibility = visibility == true ? Visibility.Visible : Visibility.Collapsed;
        }

        public void SetVisibilityStudent(bool visibility)
        {
            MnuStudent.Visibility = visibility == true ? Visibility.Visible : Visibility.Collapsed;
        }

        private void MniManageTeachers_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new ManageTeachersView();
        }
    }
}
