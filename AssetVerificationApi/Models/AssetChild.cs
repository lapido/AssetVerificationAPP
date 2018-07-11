using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssetVerificationApi.Models
{
    [Table("AssetChildren")]
    public class AssetChild
    {
        [Key]
        public int ChildID { get; set; }
        public string Name { get; set; }

        public int? ParentAssetID { get; set; }

        [ForeignKey("ParentAssetID")]
        public ParentAssetModel ParentAsset { get; set; }

        public ICollection<PropertyModel> Properties { get; set; }
    }
}