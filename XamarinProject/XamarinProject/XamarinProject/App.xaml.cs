
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

        protected override async void OnSleep()
        {
            DataStore data = new DataStore();

            if (Application.Current.Properties["username"].ToString() != null)
                await data.userIsOffline(Application.Current.Properties["username"].ToString());

            // send query to make user offline
        }

        protected override async void OnResume()
        {
            DataStore data = new DataStore();

            if (Application.Current.Properties["username"].ToString() != null)
                await data.userIsOnline(Application.Current.Properties["username"].ToString());

            //send query to make user online again

        }
    }
}
