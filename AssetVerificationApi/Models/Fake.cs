﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetVerificationApi.Models
{
    public class Fake
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        
    }
}
