﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdlånsWeb.DataHandling
{
    public class Data
    {
        public ConvertCourseData ConvertCourseData { get; set; }
        public ConvertItemData ConvertItemData { get; set; }
        public ConvertUserData ConvertUserData { get; set; }
        public ConvertLoginData Convertlogindata { get; set; }
        public ConvertBookingData ConvertBookingData { get; set; }

        public Data()
        {
            ConvertCourseData = new ConvertCourseData();
            ConvertItemData = new ConvertItemData();
            ConvertUserData = new ConvertUserData();
            Convertlogindata = new ConvertLoginData();
            ConvertBookingData = new ConvertBookingData();
        }
    }
}