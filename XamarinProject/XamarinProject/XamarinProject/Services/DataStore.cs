using Android.OS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinProject.Models;

namespace XamarinProject.Services
{
    public class DataStore
    {
        HttpClient _client;
        IEnumerable<UserProfileModel> profile;

        public DataStore()
        {
            _client = new HttpClient(GetInsecureHandler());
            _client.BaseAddress = new Uri($"https://192.168.1.107:45455/api/");
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        //logging in

        public async Task<bool> LogInAsync(User user)
        {
            if (user != null || !IsConnected)
            {
                var serializedUser = JsonConvert.SerializeObject(user);
                //fix url
                var response = await _client.PostAsync($"Users/login", new StringContent(serializedUser, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }

        //register

        public async Task<int> RegisterUser(User user)
        {
            if (user != null || !IsConnected)
            {
                var serializedUser = JsonConvert.SerializeObject(user);

                var response = await _client.PostAsync($"Users", new StringContent(serializedUser, Encoding.UTF8, "application/json"));

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    return 1;
                else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return 0;
                }
            }

            return -1;
        }

        //get all users

        public async Task<IEnumerable<UserProfileModel>> GetAllUsersAsync()
        {
            var response = await _client.GetAsync($"Users");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserProfileModel>>(content);
            }
            return new List<UserProfileModel>();
        }


        //update user is offline
        //on every sleep send

        public async Task userIsOffline(string username)
        {
            if (username != null || !IsConnected)
            {
                await _client.GetAsync($"Users/offline/{username}");
            }
        }

        //is online

        public async Task userIsOnline(string username)
        {
            if (username != null || !IsConnected)
            {
                await _client.GetAsync($"Users/online/{username}");
            }
        }

        //messages..

        public async Task<bool> SendAMessage(MessageModel message)
        {
            if ( message != null || !IsConnected)
            {
                var serializedMessage = JsonConvert.SerializeObject(message);

                var response = await _client.PostAsync($"Msgs", new StringContent(serializedMessage, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }


        public async Task<IEnumerable<MessageModel>> ReceiveUnreadMessages(string username, string usernameFrom)
        {
            if (username != "" || !IsConnected)
            {
                var response = await _client.GetAsync($"Msgs/messages/{username}/{usernameFrom}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<MessageModel>>(content);
                }
            }

            return new List<MessageModel>();
        }

        public async Task<IEnumerable<string>> ReceiveMessages(string usernameTo)
        {
            if (usernameTo != "" || !IsConnected)
            {
                var response = await _client.GetAsync($"Msgs/{usernameTo}");

                if(response.IsSuccessStatusCode)
                {
                        var content = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<string>>(content);
                }
            }

            return new List<string>();
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain,
            errors) =>
            {
                return true;
            };
            return handler;
        }




    }
}
