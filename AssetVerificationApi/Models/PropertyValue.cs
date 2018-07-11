using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssetVerificationApi.Models
{
    [Table("PropertyValue")]
    public class PropertyValue
    {
        [Key]
        public int ID { get; set; }
        public string Value { get; set; }

        public int? OptionID { get; set; }
        public int? AssetID { get; set; }
        public int? ParentAssetID { get; set; }
        public int? ChildID { get; set; }
        public int? PropertyID { get; set; }

        [ForeignKey("AssetID")]
        public AssetModel Asset { get; set; }

        [ForeignKey("ParentAssetID")]
        public ParentAssetModel ParentAsset { get; set; }

        [ForeignKey("ChildID")]
        public AssetChild AssetChild { get; set; }

        [ForeignKey("PropertyID")]
        public PropertyModel AssetProperty { get; set; }

        [ForeignKey("OptionID")]
        public OptionModel Option { get; set; }
    }
}