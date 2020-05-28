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
        public string Username { get; set; }
        ItemsViewModel viewModel;

        StackLayout sLayout;
        public ItemsPage()
        {
            InitializeComponent();

            Username = "defalt";
            sLayout = layout;
            MessagingCenter.Subscribe<MainMenu, string>(this, "usernameChat", (obj, message) =>
            {
                this.Username = message;
            });


            BindingContext = viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            if(viewModel.Items.Count == 0)
            {
                sLayout.IsVisible = true;
            } else
                sLayout.IsVisible = false;

            // viewModel.IsBusy = true;
            base.OnAppearing();
        }

        // tap existing chat and move to chat page


        //private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    var layout = (BindableObject)sender;
        //    var item = (ItemStudents)layout.BindingContext;
        //    await Navigation.PushAsync(new StudentDetailsPage(new StudentDetailsViewModel(item)));
        //}

        private async void New_Chat_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("asdf", "adfafas", "ok");
        }
    }
}