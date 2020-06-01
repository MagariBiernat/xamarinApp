using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinProject.Models;
using XamarinProject.Services;

namespace XamarinProject.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        //logged in user
        public string Username { get; set; }

        public string MessageValue { get; set; }

        public UserProfileModel Item { get; set; }

        public Command SendMessage { get; set; }

        public MessageViewModel(string _username, UserProfileModel item)
        {

            Username = _username;

            Title = item.Username;

            Item = item;

            SendMessage = new Command(async () => await ExecuteSendAMessage());
        }

        async Task<bool> ExecuteSendAMessage()
        {
            MessageModel message = new MessageModel()
            {
                Value = MessageValue,
                From = Username,
                To = Item.Username
            }
            var response = await DataService.SendAMessage(message);

            return false;
        }
    }
}
