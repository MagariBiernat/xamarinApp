using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinProject.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public RegisterViewModel()
        {
            Title = "Register";
        }

        public async Task<bool> RegisterUser(String username, String password)
        {
            return true;
        }


    }
}
