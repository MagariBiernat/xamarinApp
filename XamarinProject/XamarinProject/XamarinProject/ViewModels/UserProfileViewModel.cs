using System;
using System.Collections.Generic;
using System.Text;
using XamarinProject.Models;

namespace XamarinProject.ViewModels
{
    
    public class UserProfileViewModel : BaseViewModel
    {
        public UserProfileModel Item { get; set; }
        public string Username { get; set; }
    
        public UserProfileViewModel(UserProfileModel item = null)
        {
            Title = item.Username;

            Item = item;


        }
    }
}
