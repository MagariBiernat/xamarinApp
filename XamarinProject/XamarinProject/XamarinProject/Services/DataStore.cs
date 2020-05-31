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
            _client.BaseAddress = new Uri($"https://192.168.1.106:45455/api/");
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        //logging in

        public async Task<bool> LogInAsync(User user)
        {
            if ( user != null || !IsConnected)
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
            if( user!= null || !IsConnected)
            {
                var serializedUser = JsonConvert.SerializeObject(user);


                var response = await _client.PostAsync($"Users", new StringContent(serializedUser, Encoding.UTF8, "application/json"));

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    return 1;
                else if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
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
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserProfileModel>>(content);
            }
            return new List<UserProfileModel>();
        }

        //messages..

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
