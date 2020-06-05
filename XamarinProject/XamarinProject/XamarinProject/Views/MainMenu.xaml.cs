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
                try
                {
                    if (Application.Current.Properties["username"].ToString() != null)
                        await Data.userIsOffline(Application.Current.Properties["username"].ToString());
                    Application.Current.Properties["username"] = null;
                }
                catch (Exception ex) { }
                finally
                {
                    await Navigation.PopAsync();
                }
            });

            MessagingCenter.Subscribe<ItemsPage>(this, "logout", async (obj) =>
            {
                await Navigation.PopAsync();
            });




        }





    }
}