using Android.OS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;
using XamarinProject.Models;

namespace XamarinProject.ViewModels
{
   
    public class UsersAllViewModel : BaseViewModel
    {
        bool isOnlineModel = false;

        public string Username { get; set; }

        public ObservableCollection<UserProfileModel> Items;

        public Command LoadItemsCommand { get; set; }


        public UsersAllViewModel(bool isOnline)
        {

            isOnlineModel = isOnline;

            Items = new ObservableCollection<UserProfileModel>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadUsers());

            
        }

        async Task ExecuteLoadUsers()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataService.GetAllUsersAsync();
                foreach(var item in items)
                {
                    if(item.Username != Username)
                    {
                        if (isOnlineModel == true)
                        {
                            if (item.isOnline == true)
                            {
                                Items.Add(item);
                            }
                        }
                        else
                             Items.Add(item);
                    }
                    
                }
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                isVisible = !(Items.Count > 0);
                IsBusy = false;
            }
        }

    }
}
