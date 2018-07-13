using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssetVerificationApi.Models
{
    [Table("ParentAsset")]
    public class ParentAssetModel
    {
        [Key]
        public int ParentAssetID { get; set; }
        public string Name { get; set; }

        public string ImageName { get; set; }
        public ICollection<AssetChild> AssetChild { get; set; }

        //public ICollection<PropertyModel> Properties { get; set; }

        //public ICollection<OptionModel> Options { get; set; }
    }
}
