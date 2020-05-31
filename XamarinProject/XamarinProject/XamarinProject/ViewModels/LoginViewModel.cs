using Android.OS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinProject.Models;

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
            User user = new User() { Username = username, Password = password };
            try
            {
                loggedIn = await DataService.LogInAsync(user);
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return loggedIn;
        }
    }
}
