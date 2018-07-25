using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssetVerificationApi.Models
{
    [Table("FieldParent")]
    public class FieldParent
    {
        [Key]
        public int ID { get; set; }
        public int ParentID {get; set; }   
    }
}
