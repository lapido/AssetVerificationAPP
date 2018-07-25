using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetVerificationApi.Models
{
    [Table("FieldData")]
    public class FieldDataModel
    {
        [Key]
        public int FieldDataID { get; set; }
        public int? AssetID { get; set; }

        public int? ChildID { get; set; }
        public int? ParentAssetID { get; set; }
        public int? UserID { get; set; }
        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
        
        [ForeignKey("AssetID")]
        public AssetModel AssetAssetModel { get; set; }

        [ForeignKey("ChildID")]
        public AssetChild AssetChild { get; set; }

        [ForeignKey("UserID")]
        public UserModel UserModel { get; set; }

        [ForeignKey("ParentAssetID")]
        public ParentAssetModel ParentAsset { get; set; }
    }
    
        [Table("FieldParent")]
    public class FieldParent
    {
        [Key]
        public int ID { get; set; }
        public int ParentID {get; set; }   
    }
}
