using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool isOnline { get; set; }
    }
}
