
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinProject.Services;
using XamarinProject.Views;

namespace XamarinProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            DataStore data = new DataStore();

            // send query to make user offline
        }

        protected override void OnResume()
        {
            DataStore data = new DataStore();

            //send query to make user online again

        }
    }
}
