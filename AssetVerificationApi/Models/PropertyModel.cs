using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssetVerificationApi.Models
{
    [Table("Property")]
    public class PropertyModel
    {
        [Key]
        public int PropertyID { get; set; }
        public string Name { get; set; }
        //public string DisplayName { get; set; }

        public int? ChildID { get; set; }
        //public int? ParentAssetID { get; set; }

        //[ForeignKey("ParentAssetID")]
        //public ParentAssetModel ParentAsset { get; set; }

        [ForeignKey("ChildID")]
        public AssetChild AssetChild { get; set; }

        public ICollection<OptionModel> Optons { get; set; }
    }
}