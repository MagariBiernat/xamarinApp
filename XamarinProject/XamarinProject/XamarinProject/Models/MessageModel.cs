﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
    public class MessageModel
    {
        public int? Id { get; set; }
        public string Value { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool Read { get; set; }
        public Xamarin.Forms.FlexDirection Direction { get; set; }
    }
}
