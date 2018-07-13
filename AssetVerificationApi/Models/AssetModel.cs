using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace AssetVerificationApi.Models
{
    [Table("Asset")]
    //[JsonObject(IsReference =true)]
    public class AssetModel
    {
        [Key]
        public int AssetID { get; set; }
        //public int SiteID { get; set; }
        //public string UniqueIdentifier { get; set; }
        //Asset Properties
        //public string PropertyOne { get; set; }
        //public string PropertyTwo { get; set; }
        //public string PropertyThree { get; set; }
        public string ImageName { get; set; }
        public int? ParentAssetID { get; set; }
        public int? ChildID { get; set; }

        [ForeignKey("ParentAssetID")]
        public ParentAssetModel ParentAsset { get; set; }

        [ForeignKey("ChildID")]
        public AssetChild AssetChild { get; set; }
        
        
        

    }
}
