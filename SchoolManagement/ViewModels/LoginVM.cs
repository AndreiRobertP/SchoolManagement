using SchoolManagement.Models.BusinessLogic;
using System;
using System.Windows;

namespace SchoolManagement.ViewModels
{
    public class LoginVM : BasePropertyChanged
    {
        public String DisplayUsername { get; set; }
        public String DisplayPassword { get; set; }


        private UserLoginResponse _userLoginResponse;
        public UserLoginResponse UserLoginResponse
        {
            get {
                return _userLoginResponse ?? new UserLoginResponse();
            }
            
            set
            {
                _userLoginResponse = value;
                OnPropertyChanged();
            }
        }

        //BLL
        private UserBLL UserBLL { get; set; } = new UserBLL();

        //Commands
        private RelayCommand _cmdLogin;
        public RelayCommand CmdLogin
        {
            get
            {
                return _cmdLogin ?? (_cmdLogin = new RelayCommand(
                    () =>
                    {
                        UserLoginResponse = UserBLL.GetUserTypeLogin(DisplayUsername);

                        if (!UserLoginResponse.IsRegistered)
                        {
                            MessageBox.Show("The username entered doesn't exist in the database", "Wrong credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                , () => true
                ));
            }
        }
    }
}
