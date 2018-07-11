//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using AssetVerificationApi.Models;

//namespace AssetVerificationApi.Context_
//{
//    public class InitializeDb : DropCreateDatabaseAlways<Context>   
//    {
//        protected override void Seed(Context context)
//        {
//            var ParentAsset_1 = new ParentAssetModel{
//                Name = "Asset A"
//            };
//            var ParentAsset_2 = new ParentAssetModel
//            {
//                Name = "Asset A"
//            };
//            var ParentAsset_3 = new ParentAssetModel
//            {
//                Name = "Asset A"
//            };
//            context.ParentAsset.Add(ParentAsset_1);
//            context.ParentAsset.Add(ParentAsset_2);
//            context.ParentAsset.Add(ParentAsset_3);

//            var A_1_1 = new LevelOneModel {
//                Name = "Asset A_1_1",
//                ParentAsset = ParentAsset_1
//            };
//            var A_1_2 = new LevelOneModel {
//                Name = "Asset A_1_2",
//                ParentAsset = ParentAsset_1
//            };
//            var A_2_1 = new LevelOneModel
//            {
//                Name = "Asset A_2_1",
//                ParentAsset = ParentAsset_2
//            };
//            var A_2_2 = new LevelOneModel
//            {
//                Name = "Asset A_2_2",
//                ParentAsset = ParentAsset_2
//            };
//            context.LevelOneModel.Add(A_1_1);
//            context.LevelOneModel.Add(A_1_2);
//            context.LevelOneModel.Add(A_2_1);
//            context.LevelOneModel.Add(A_2_2);

//            var A_1_1_1 = new LevelTwoModel {
//                Name = "Asset A_1_1_1",
//                LevelOneModel = A_1_1
//            };
//            context.LevelTwoModel.Add(A_1_1_1);
//            context.SaveChanges();
//            base.Seed(context);
//        }
//    }
//}