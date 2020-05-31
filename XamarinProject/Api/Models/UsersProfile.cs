using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class UsersProfile
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool isOnline { get; set; }
    }
}
