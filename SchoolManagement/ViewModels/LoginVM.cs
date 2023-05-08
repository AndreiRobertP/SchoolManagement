using Microsoft.Identity.Client;
using SchoolManagement.Models.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagement.ViewModels
{
    public class LoginVM : BasePropertyChanged
    {
        public String DisplayUsername { get; set; }
        public String DisplayPassword { get; set; }


        private UserPermisions _userPermisions;
        public UserPermisions UserPermisions
        {
            get {
                return _userPermisions ?? new UserPermisions();
            }
            
            set
            {
                _userPermisions = value;
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
                        UserPermisions = UserBLL.GetUserTypeLogin(DisplayUsername);

                        if (!UserPermisions.IsRegistered)
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
