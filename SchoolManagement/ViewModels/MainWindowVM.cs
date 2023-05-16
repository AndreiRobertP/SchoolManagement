using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Models.BusinessLogic;

namespace SchoolManagement.ViewModels
{
    public class MainWindowVM: BasePropertyChanged
    {
        private UserLoginResponse _userLoginResponse = null!;
        public UserLoginResponse UserLoginResponse
        {
            get { return _userLoginResponse;}
            set
            {
                _userLoginResponse = value;
                OnPropertyChanged();
            }
        }
    }
}
