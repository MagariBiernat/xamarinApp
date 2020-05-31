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
    public partial class UsersAllPage : ContentPage
    {
        public UsersAllViewModel viewModel;
        public UsersAllPage()
        {
            InitializeComponent();

            viewModel = new UsersAllViewModel(false);

            viewModel.Username = Application.Current.Properties["username"].ToString();


            BindingContext = viewModel;
            
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;


            
        }
    }
}