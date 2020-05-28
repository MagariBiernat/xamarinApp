using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinProject.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public bool loggedIn { get; set; }
        public LoginViewModel()
        {
            loggedIn = true;
        }

        public async Task<bool> ExecuteLoggingIn(string username, string password)
        {
            return loggedIn;
        }
    }
}
