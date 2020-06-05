using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinProject.Models;
using XamarinProject.ViewModels;

namespace XamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        ChatPageViewModel viewModel;
        public ChatPage(string usernameSender)
        {
            InitializeComponent();

            viewModel = new ChatPageViewModel(usernameSender, Application.Current.Properties["username"].ToString());

            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;

        }


        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void ButtonAnswer_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync($"{viewModel.Title}", "Type a message");

            await viewModel.ExecuteSendAMessage(result);
        }

        private void ItemsCollectionView_ScrollToRequested(object sender, ScrollToRequestEventArgs e)
        {
            ItemsCollectionView.ScrollTo(viewModel.Items)
        }
    }
}