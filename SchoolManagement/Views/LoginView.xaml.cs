using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginVM LoginVM{get; set;}
        public LoginView()
        {
            InitializeComponent();
            LoginVM = DataContext as LoginVM;

            Uri uri = new Uri("https://images.pexels.com/photos/5088017/pexels-photo-5088017.jpeg", UriKind.Absolute);
            BitmapImage img = new BitmapImage(uri);
            ImgLoginBgRight.Source = img;
        }
    }
}
