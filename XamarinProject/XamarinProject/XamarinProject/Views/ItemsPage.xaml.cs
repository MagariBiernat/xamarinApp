using Android.Views;
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
    public partial class ItemsPage : ContentPage
    {

        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            viewModel = new ItemsViewModel();

            viewModel.Username = Application.Current.Properties["username"].ToString();

            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {     
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
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
            catch(Exception ex)
            { 
            }
            finally
            {
                MessagingCenter.Send(this, "logout");

            }

            return true;
        }

        // tap existing chat and move to chat page

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var item = (string)layout.BindingContext;
            await Navigation.PushModalAsync(new NavigationPage(new ChatPage(item)));
        }

        private async void New_Chat_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TabbedUsers(viewModel.Username));
        }

    }
}