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
    public partial class UsersOnlinePage : ContentPage
    {
        public UsersAllViewModel viewModel;

        StackLayout sLayout;
        public UsersOnlinePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new UsersAllViewModel(true);
            sLayout = layout;
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            if (viewModel.Items.Count == 0)
            {
                sLayout.IsVisible = true;
            }
            else
                sLayout.IsVisible = false;

            viewModel.IsBusy = true;

            base.OnAppearing();
        }
    }
}