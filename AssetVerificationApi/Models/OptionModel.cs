using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssetVerificationApi.Models
{
    [Table("Option")]
    public class OptionModel
    {
        [Key]
        public int OptionID { get; set; }
        public string Name { get; set; }

        public int? PropertyID { get; set; }

        //[ForeignKey("ParentAssetID")]
        //public ParentAssetModel ParentAsset { get; set; }

        //[ForeignKey("ChildID")]
        //public AssetChild AssetChild { get; set; }

        [ForeignKey("PropertyID")]
        public PropertyModel AssetProperty { get; set; }
    }
}