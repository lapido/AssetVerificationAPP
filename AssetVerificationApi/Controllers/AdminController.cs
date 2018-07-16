using System.Web.Http;
using AssetVerificationApi.Context_;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System;

namespace AssetVerificationApi.Controllers
{
    public class AdminController : ApiController
    {
        private Context context = new Context();

        [HttpGet]
        [Route("api/admin/GetGroups")]
        public IHttpActionResult GetParentAssets()
        {
            var parents = context.ParentAsset.ToList();
            return Ok(parents);
        }

        [HttpGet]
        [Route("api/admin/AllAssets")]
        public IHttpActionResult GetAllAssets()
        {
            var assets = (from asset in context.AssetModel
                          join field in context.FieldDataModel on asset.AssetID equals field.AssetID
                          join parent in context.ParentAsset on asset.ParentAssetID equals parent.ParentAssetID
                          join child in context.AssetChild on asset.ChildID equals child.ChildID
                          join user in context.UserModel on field.UserID equals user.UserId
                          select new
                          {
                              AssetGroup = parent.Name,
                              GroupID = parent.ParentAssetID,
                              Child = child.Name,
                              longitude = field.Longitude,
                              latitude = field.Latitude,
                              username = user.Name
                          });

            return Ok(assets);
        }


        [HttpGet]
        [Route("api/admin/GetAssetsForGroup")]
        public IHttpActionResult GetAssetsForGroup(int groupID)
        {
            var assets_ = (from asset in context.AssetModel
                          join field in context.FieldDataModel on asset.AssetID equals field.AssetID
                          join property in context.PropertyValue on asset.ChildID equals property.ChildID
                          join propertyT in context.Property on property.PropertyID equals propertyT.PropertyID
                          join parent in context.ParentAsset on asset.ParentAssetID equals parent.ParentAssetID
                          join child in context.AssetChild on asset.ChildID equals child.ChildID
                          where asset.ParentAssetID == groupID 
                          select new
                          {
                              AssetGroup = parent.Name,
                              Children = child.Name,

                          });
            var assets = context.AssetModel.Where(x => x.ParentAssetID == groupID).ToList();
            var children = context.AssetChild.Where(x => x.ParentAssetID == groupID).ToList();

            List<object> AssetsList = new List<object>();
            foreach (var asset in assets)
            {
                
                var assetObj = new ExpandoObject() as IDictionary<string, Object>;
                foreach (var child in children)
                {
                    var properties = context.Property.Where(x => x.ChildID == child.ChildID);
                    foreach (var p in properties)
                    {
                        
                        var c = context.PropertyValue.Where(x => x.AssetID == asset.AssetID & x.PropertyID == p.PropertyID).FirstOrDefault();
                        if (c != null){
                            
                            assetObj.Add(p.Name.Replace(" ", String.Empty), c.Value);
                        }
                        else
                        {
                            assetObj.Add(p.Name.Replace(" ", String.Empty), "");
                        }
                        
                    }
                }
                AssetsList.Add(assetObj);
            }

           
            
            return Ok(AssetsList);
        }
    }
}
