using SchoolManagement.Models.BusinessLogic;
using SchoolManagement.ViewModels;
using SchoolManagement.Views;
using System.ComponentModel;
using System.Windows;

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

        void OnLoginChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginVM.UserLoginResponse))
            {
                UserLoginResponse userLoginResponse = LoginVM.UserLoginResponse;
                MainWindowViewModel.UserLoginResponse = userLoginResponse;

                SetVisibilityMenuRoles(userLoginResponse.IsRegistered);
                SetVisibilityAdmin(userLoginResponse.IsAdmin);
                SetVisibilityTeacher(userLoginResponse.IsTeacher);
                SetVisibilityHomeroomTeacher(userLoginResponse.IsHomeroomTeacher);
                SetVisibilityStudent(userLoginResponse.IsStudent);
            }
        }

        public void SetVisibilityMenuRoles(bool visibility)
        {
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

        private void MniManageSpecializations_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new ManageSpecialisationsView();
        }

        private void MniManageSubjects_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new ManageSubjectsView();
        }

        private void MniManageHomerooms_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new ManageHomeroomsView();
        }

        private void MniManageStudents_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new ManageStudentsView();
        }

        private void MniManageSht_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new ManageShtsView();
        }

        private void MniTeacherCatalog_Click(object sender, RoutedEventArgs e)
        {
            TeacherCatalogView tcv = new TeacherCatalogView();
            tcv.TeacherCatalogVm.FieldTeacher = MainWindowViewModel.UserLoginResponse.Teacher;
            frmMain.Content = tcv;
        }

        private void MniTeacherFile_Click(object sender, RoutedEventArgs e)
        {

            TeacherFileView tfv = new TeacherFileView();
            tfv.TeacherFileVM.FieldTeacher = MainWindowViewModel.UserLoginResponse.Teacher;
            frmMain.Content = tfv;
        }

        private void MniHomeroomTeacherCatalog_OnClick(object sender, RoutedEventArgs e)
        {
            HomeroomTeacherCatalogView htcv = new HomeroomTeacherCatalogView();
            htcv.HomeroomTeacherCatalogVM.FieldHomeroom = MainWindowViewModel.UserLoginResponse.Homeroom;
            frmMain.Content = htcv;
        }

        private void MniHomeroomTeacherStats_OnClick(object sender, RoutedEventArgs e)
        {
            HomeroomTeacherStatsView htsv = new HomeroomTeacherStatsView();
            htsv.HomeroomTeacherStatsVM.FieldHomeroom = MainWindowViewModel.UserLoginResponse.Homeroom;
            frmMain.Content = htsv;
        }

        private void MniHomeroomTeacherExpelList_OnClick(object sender, RoutedEventArgs e)
        {
            ExpelListView htcv = new ExpelListView();
            htcv.ExpelListVm.FieldHomeroom = MainWindowViewModel.UserLoginResponse.Homeroom;
            frmMain.Content = htcv;
        }

        private void MniStudentCatalog_OnClick(object sender, RoutedEventArgs e)
        {
            StudentCatalogView scv = new StudentCatalogView();
            scv.StudentCatalogVM.FieldStudent = MainWindowViewModel.UserLoginResponse.Student;
            frmMain.Content = scv;
        }

        private void MniStudentFiles_OnClick(object sender, RoutedEventArgs e)
        {
            StudentFileView sfv = new StudentFileView();
            sfv.StudentFileVM.FieldStudent = MainWindowViewModel.UserLoginResponse.Student;
            frmMain.Content = sfv;
        }
    }
}
