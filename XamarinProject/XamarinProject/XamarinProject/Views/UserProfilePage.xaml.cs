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
    public partial class UserProfilePage : ContentPage
    {
        UserProfileViewModel viewModel;

        public UserProfilePage(UserProfileViewModel viewModel)
        {
            InitializeComponent();

            viewModel.Username = Application.Current.Properties["username"].ToString();

            BindingContext = this.viewModel = viewModel;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MessageSend(viewModel.Username, viewModel.Item));
        }
    }
}