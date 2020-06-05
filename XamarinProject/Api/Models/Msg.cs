using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Msg
    {
        public string Value { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool Read { get; set; }
    }
}
