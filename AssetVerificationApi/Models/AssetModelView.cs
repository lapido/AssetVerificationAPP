using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssetVerificationApi.Models;

namespace AssetVerificationApi.ViewModels
{
    public class AssetModelView
    {
        public IEnumerable<AssetModel> Assets { get; set; }
        //public IEnumerable<LevelOneModel> LevelOnes { get; set; }
        //public IEnumerable<LevelTwoModel> LevelTwos { get; set; }
        //public IEnumerable<LevelThreeModel> LevelThrees { get; set; }
    }
}