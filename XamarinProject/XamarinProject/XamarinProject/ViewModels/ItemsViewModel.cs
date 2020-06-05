using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinProject.Models;

namespace XamarinProject.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<string> Items { get; set; }
        public string Username { get; set; }
        

        public Command LoadItemsCommand { get; set; }
        public ItemsViewModel()
        {
            Title = "Chat";

            Items = new ObservableCollection<string>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadNewMessages());

            
        }

        async Task ExecuteLoadNewMessages()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = await DataService.ReceiveMessages(Username);

                foreach(var item in items)
                {
                    Items.Add(item);
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                isVisible = Items.Count == 0;
                
                IsBusy = false;
            }
        }
    }
}
