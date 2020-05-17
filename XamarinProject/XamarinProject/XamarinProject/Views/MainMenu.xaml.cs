using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {
        public string Username { get; set; }
        public MainMenu()
        {
            InitializeComponent();
        }

        private async void LogOutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


    }
}