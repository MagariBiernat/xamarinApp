using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinProject.Models;

namespace XamarinProject.ViewModels
{
    public class ChatPageViewModel : BaseViewModel
    {
        public string Username { get; set; }

        public ObservableCollection<MessageModel> Items {get;set;}

        public List<int> messagesIds { get; set; }

        public int ScrollTo { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Xamarin.Forms.FlexDirection Direction { get; set; }

        public ChatPageViewModel(string usernameSender, string username)
        {
            Title = usernameSender;

            Username = username;

            Items = new ObservableCollection<MessageModel>();

            messagesIds = new List<int>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadAllMessages());
        }

        public async Task ExecuteSendAMessage(string MessageValue)
        {
            try
            {
                MessageModel message = new MessageModel()
                {
                    Value = MessageValue,
                    From = Username,
                    To = Title,
                    Read = false
                };

                await DataService.SendAMessage(message);
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = true;
                //await ExecuteLoadAllMessages();
            }
        }

        async Task ExecuteLoadAllMessages()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = await DataService.ReceiveUnreadMessages(Title , Username);

                foreach(var item in items)
                {
                    Items.Add(new MessageModel()
                    {
                        Value = item.Value,
                        From = item.From,
                        To = item.To,
                        Read = item.Read,
                        Direction = (FlexDirection)(item.From == Username ? 1 : 0)
                    });
                }

            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                ScrollTo = Items.Count - 1;
                IsBusy = false;
            }
        }
    }
}
