﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.UI.MVC.Models
{
    //[JsonObject(MemberSerialization.OptIn)]
    public class APIDataModel
    {

        public string ID { get; set; }
        public decimal Deaths_1M_Pop { get; set; }

    }

    public class APIList
    {
        public List<APIDataModel> ApiList { get; set; }
    }
}