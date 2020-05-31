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
    public partial class TabbedUsers : TabbedPage
    {
        private string Username { get; set; }
        public TabbedUsers()
        {
            InitializeComponent();
        }

        public TabbedUsers(string _username)
        {
            InitializeComponent();

            Username = _username;
        }
        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();


        }
    }
}