using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace XamarinProject.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<string> Items { get; set; }
        public string Username { get; set; }
        public ItemsViewModel()
        {
            Title = "Chat";

            Items = new ObservableCollection<string>();
        }
    }
}
