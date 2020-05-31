using Android.Content.Res;
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
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;
        Entry username, password;

        public LoginPage()
        {
            InitializeComponent();

            //hooks
            username = usernameEntry;
            password = passwordEntry;

            viewModel = new LoginViewModel();

            BindingContext = viewModel;
        }

        private async void LogInButton_Clicked(object sender, EventArgs e)
        {
            if(validateIfEmpty())
            {
                if (await viewModel.ExecuteLoggingIn( username.Text.ToString(), password.Text.ToString() ))
                {
                    string Username = username.Text.ToString();
                    Application.Current.Properties["username"] = Username;
                    var page = new MainMenu(Username);
                    await Navigation.PushAsync(page);
                }
                else
                {
                    await DisplayAlert("Error", "Wrong email or password", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "Fields can't be empty", "Ok sorry fam");
            }  
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }



        private bool validateIfEmpty()
        {
            if (username.Text.ToString() != "" && password.Text.ToString() != "")
                return true;
            return false;
        }


        protected override bool OnBackButtonPressed()
        {
            // TODO:
            //insert a funny logging out Page with animation
            return base.OnBackButtonPressed();
        }
    }
}