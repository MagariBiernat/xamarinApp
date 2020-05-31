using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinProject.Models;

namespace XamarinProject.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public RegisterViewModel()
        {
            Title = "Register";
        }

        public async Task<int> RegisterUser(String username, String password)
        {
            User user = new User() { Username = username, Password = password };

            int response = -1;
            try
            {
                response = await DataService.RegisterUser(user);

            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            //1 success
            //0 different username
            //else something is wrong

            return response;
        }


    }
}
