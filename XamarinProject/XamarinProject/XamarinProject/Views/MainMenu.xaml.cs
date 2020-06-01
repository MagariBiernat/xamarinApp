using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using XamarinProject.Services;

namespace XamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : TabbedPage
    {
        public string Username { get; set; }

        DataStore Data;
        public MainMenu(string _username)
        {
            InitializeComponent();

            Username = _username;

            Data = new DataStore();

            MessagingCenter.Subscribe<ProfilePage>(this, "logout", async (obj) =>
            {
                await logOut();
            });

            MessagingCenter.Subscribe<ItemsPage>(this, "logout", async (obj) =>
            {
                await logOut();
            });



        }

        private async Task logOut()
        {
            if (Application.Current.Properties["username"].ToString() != null)
                await Data.userIsOnline(Application.Current.Properties["username"].ToString());

            Application.Current.Properties["username"] = null;

            await Navigation.PopAsync();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (this.CurrentPage.Title == "Chat")
            {
                MessagingCenter.Send(this, "usernameChat", Username);
            }
            if(this.CurrentPage.Title == "Profile")
            {
                MessagingCenter.Send(this, "usernameProfile", Username);
            }
            
        }



    }
}