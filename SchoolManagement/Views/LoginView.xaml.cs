using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.IO;

namespace SchoolManagement.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();

            Uri uri = new Uri("https://images.pexels.com/photos/5088017/pexels-photo-5088017.jpeg", UriKind.Absolute);
            BitmapImage img = new BitmapImage(uri);
            ImgLoginBgRight.Source = img;
        }
    }
}
