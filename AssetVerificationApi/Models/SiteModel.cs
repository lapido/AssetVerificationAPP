using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssetVerificationApi.Models
{
    [Table("Site")]
    public class SiteModel
    {
        [Key]
        public int SiteID {get; set; }
        public string Address { get; set; }
        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
    }
}