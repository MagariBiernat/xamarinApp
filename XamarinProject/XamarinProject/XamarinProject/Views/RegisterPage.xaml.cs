using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinProject.ViewModels;

namespace XamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        RegisterViewModel viewModel;


        public RegisterPage()
        {
            InitializeComponent();

            viewModel = new RegisterViewModel();
        }


        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            String username = usernameEntry.Text.ToString();
            String passwordFirst = passwordFirstEntry.Text.ToString();
            String passwordSecond = passwordSecondEntry.Text.ToString();

            if (validateForm(username, passwordFirst, passwordSecond))
            {
                if (passwordsMatch(passwordFirst, passwordSecond))
                {
                    var response = await viewModel.RegisterUser(username, passwordFirst);
                    if (response == 1)
                    {
                        await DisplayAlert("Success", "Your account was created successfuly", "Noice");
                        await Navigation.PopAsync();
                    }
                    else if (response == 0)
                    {
                        await DisplayAlert("Error", "Username is taken", "Alright");
                    }
                    else
                        await DisplayAlert("Sorry", "Contant administrator coz something is certainly wrong, mate", "F");
                }else
                {
                    await DisplayAlert("Error", "Passwords are not equal", "Ok fam");
                }
            }
            else
                await DisplayAlert("Error", "Please fill forms correctly!", "Ok");
        }

        private bool validateForm(String username, String pwd, String pwd2)
        {
            return username != "" && pwd != "" && pwd2 != "";
        }

        private bool passwordsMatch(String pwd, String pwd2)
        {
            return pwd == pwd2;
        }

        private async void goBackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}