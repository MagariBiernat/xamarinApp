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

        public LoginPage()
        {
            InitializeComponent();

            viewModel = new LoginViewModel();

            BindingContext = viewModel;

            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            usernameEntry.Text = "";
            passwordEntry.Text = "";

        }


        private async void LogInButton_Clicked(object sender, EventArgs e)
        {
            if(validateIfEmpty())
            {
                if (await viewModel.ExecuteLoggingIn( usernameEntry.Text.ToString(), passwordEntry.Text.ToString() ))
                {
                    string Username = usernameEntry.Text.ToString();
                    Application.Current.Properties["username"] = Username;
                    var page = new MainMenu(Username);
                    await Navigation.PushAsync(page);
                }
                else
                {
                    await DisplayAlert("Error", "Wrong username or password", "Ok");
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
            if (usernameEntry.Text.ToString().Length > 0 && passwordEntry.Text.ToString().Length > 0 )
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