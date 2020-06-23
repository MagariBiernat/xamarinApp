using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using XamarinProject.ViewModels;

namespace XamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel viewModel;

        public ProfilePage()
        {
            InitializeComponent();

            viewModel = new ProfileViewModel();



            viewModel.Username = Application.Current.Properties["username"].ToString();


            BindingContext = viewModel;
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "logout");
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();


            try
            {
                if (Application.Current.Properties["username"].ToString() != null)
                    new Task(async () =>
                    {
                        await viewModel.DataService.userIsOffline(Application.Current.Properties["username"].ToString());
                    }
                    );
                Application.Current.Properties["username"] = null;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                MessagingCenter.Send(this, "logout");

            }

            return true;

        }

        private async void goAbout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AboutPage());
        }
    }
}