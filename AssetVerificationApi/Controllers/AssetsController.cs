using System.Linq;
using System.Web.Http;
using AssetVerificationApi.Context_;
using AssetVerificationApi.Models;
using System.Data.Entity;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Dynamic;


namespace AssetVerificationApi.Controllers
{
    public class AssetsController : ApiController
    {
        private Context context = new Context();

        [HttpGet]
        [Route("api/Index")]
        public IHttpActionResult Index()
        {
            
            var Fake = new Fake
            {
                Name = "Asset A"
                
            };
            context.Fake.Add(Fake);
 
            context.SaveChanges();

            return Ok();
        }

        
        [HttpPost]
        [Route("api/forgotPassword")]
        public IHttpActionResult ForgotPassword(JObject AssetObj)
        {
            var username = AssetObj["userName"].ToString();
            var newPassword = AssetObj["newPassword"].ToString();
            var secretAnswer = AssetObj["secretAnswer"].ToString();
            var user = context.UserModel.Where(x => x.Username == username).FirstOrDefault<UserModel>();
            if (user != null)
            {
                if (user.HasChangedPassword == 0)
                {
                    ////Todo: check if secret answer exist


                    //user.HasChangedPassword = 1;
                    //user.Password = newPassword;
                    //user.SecretAnswer = secretAnswer;

                    return Unauthorized();
                }
                else if(user.HasChangedPassword == 1)
                {
                    //check if secret answer is the same with the one stored in the DB
                    if (user.SecretAnswer.Equals(secretAnswer))
                    {
                        user.Password = newPassword;

                        //return Ok(1);
                    }
                    else
                    {
                        return Ok(2);
                    }
                }
            }
            else
            {
                return NotFound();
            }

            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();

            return Ok(1);
        }

        [Authorize]
        [HttpPost]
        [Route("api/changePassword")]
        public IHttpActionResult ChangePassword(JObject AssetObj)
        {
            var username = AssetObj["userName"].ToString();
            var oldPassword = AssetObj["password"].ToString();
            var newPassword = AssetObj["newPassword"].ToString();
            var secretAnswer = AssetObj["secretAnswer"].ToString();

            //check if username exist in the db
            var user = context.UserModel.Where(x => x.Username == username && x.Password == oldPassword).FirstOrDefault<UserModel>();
            if (user != null)
            {
                //this is the first time of changing password has required by the system
                if(user.HasChangedPassword == 0)
                {
                    //Todo: check if secret answer exist


                    user.HasChangedPassword = 1;
                    user.Password = newPassword;
                    user.SecretAnswer = secretAnswer;

                    //return Ok(1);
                }

                ////changing password after the initial required change
                //else if(user.HasChangedPassword == 1)
                //{
                //    //check if secret answer is the same with the one stored in the DB
                //    if (user.SecretAnswer.Equals(secretAnswer))
                //    {
                //        user.Password = newPassword;

                //        //return Ok(1);
                //    }
                //    else
                //    {
                //        return Ok(2);
                //    }
                //}
            }
            else
            {
                return NotFound();
            }

            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();

            return Ok(1);
        }

        [Authorize]
        [Route("api/allAssets")]
        public IHttpActionResult GetAllAssets()
        {
           
            var assets = context.ParentAsset
                .Include(x => x.AssetChild.Select(y=> y.Properties.Select(z => z.Optons)))
                //.Include("AssetChild.Properties")
                .ToList();

            return Ok(assets);
        }

        [Authorize]
        [HttpPost]
        [Route("api/postAsset")]
        public IHttpActionResult PostVerify(IEnumerable<JObject> AssetObjs)
        {
            foreach(var AssetObj in AssetObjs)
            {
                var ParentAssetID = Int32.Parse(AssetObj["ParentAssetID"].ToString());
                var ChildID = Int32.Parse(AssetObj["ChildID"].ToString());
                var lat = Decimal.Parse(AssetObj["latitude"].ToString());
                var log = Decimal.Parse(AssetObj["longitude"].ToString());
                //var SiteID = Int32.Parse(AssetObj["SiteID"].ToString());
                var username = AssetObj["username"].ToString();
                //var uniqueIdentifier = AssetObj["UniqueIdentifier"].ToString();
                var UserID = context.UserModel.Where(x => x.Username == username).FirstOrDefault<UserModel>().UserId;

                //store in the asset model
                AssetModel assetModel = new AssetModel()
                {
                    //SiteID = SiteID,
                    //UniqueIdentifier = uniqueIdentifier,
                    ParentAssetID = ParentAssetID,
                    ChildID = ChildID
                };

                //storing the properties value into the dB
                var Properties = AssetObj["Properties"];
                foreach (var property in Properties)
                {
                    PropertyValue prop = new PropertyValue()
                    {
                        AssetID = assetModel.AssetID,
                        ParentAssetID = ParentAssetID,
                        ChildID = ChildID,
                        PropertyID = Int32.Parse(property["PropertyID"].ToString()),
                        Value = property["Value"]["Name"].ToString(),
                        //OptionID = Int32.Parse(property["Value"]["OptionID"].ToString()),
                    };
                    context.PropertyValue.Add(prop);
                }

                //Field Data
                FieldDataModel fieldData = new FieldDataModel()
                {
                    AssetID = assetModel.AssetID,
                    Latitude = lat,
                    Longitude = log,
                    ParentAssetID = ParentAssetID,
                    ChildID = ChildID,
                    UserID = UserID
                };


                context.AssetModel.Add(assetModel);
                context.FieldDataModel.Add(fieldData);



                context.SaveChanges();
            }
            
            return Ok();

        }
//AdminController endpoints
        [HttpGet]
        [Route("api/admin/getGroups")]
        public IHttpActionResult GetParentAssets()
        {
            var parents = context.ParentAsset.ToList();
            return Ok(parents);
        }



        [HttpGet]
        [Route("api/admin/allAssets")]
        public IHttpActionResult GetAllAssetsAdmin()
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
        [Route("api/admin/getAssetsForGroup")]
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
                        if (c != null)
                        {

                            assetObj.Add(p.Name, c.Value);
                        }
                        else
                        {
                            assetObj.Add(p.Name, "");
                        }

                    }
                }
                AssetsList.Add(assetObj);
            }



            return Ok(AssetsList);
        }

    }
}
