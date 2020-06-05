using System;
using System.Collections.Generic;
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
    public partial class MessageSend : ContentPage
    {

        MessageViewModel viewModel;

        public MessageSend(string _username, UserProfileModel item)
        {
            InitializeComponent();

            viewModel = new MessageViewModel(_username, item);

            BindingContext = viewModel;

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void sendMessage_Clicked(object sender, EventArgs e)
        {
            if (viewModel.MessageValue != "")
            {
                viewModel.SendMessage.Execute(true);
                await Navigation.PopModalAsync();
            }
            else
                await DisplayAlert("error", "please type something", "aight");

           
        }
    }
}