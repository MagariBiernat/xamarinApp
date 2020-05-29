using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> RegisterUser(String username, String password)
        {

            //1 success
            //0 different username
            //else something is wrong

            return 1;
        }


    }
}
