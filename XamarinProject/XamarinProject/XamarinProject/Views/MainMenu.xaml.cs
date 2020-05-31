using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace XamarinProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : TabbedPage
    {
        public string Username { get; set; }
        public MainMenu()
        {
            InitializeComponent();
  
        }

        public MainMenu(string _username)
        {
            InitializeComponent();

            this.Username = _username;

            MessagingCenter.Subscribe<ProfilePage>(this, "logout", async (obj) =>
            {
                await Navigation.PopAsync();
            });

        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (this.CurrentPage.Title == "Chat")
            {
                MessagingCenter.Send(this, "usernameChat", this.Username);
            }
            if(this.CurrentPage.Title == "Profile")
            {
                MessagingCenter.Send(this, "usernameProfile", this.Username);
            }
            
        }



    }
}