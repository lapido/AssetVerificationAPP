using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using AssetVerificationApi.Context_;
using System.Web.Http.Cors;
using AssetVerificationApi.Models;

namespace AssetVerificationApi.Provider
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private bool HasChangedPassword;
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); //   
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            
            Context db = new Context();

            
            var n = db.UserModel.Where(u => u.Username == context.UserName && u.Password == context.Password).FirstOrDefault<UserModel>();
                if (!string.IsNullOrEmpty(n.Name))
                {
                    HasChangedPassword = n.HasChangedPassword == 1 ? true : false;
                    identity.AddClaim(new Claim("Age", "16"));
                    
                    var props = new AuthenticationProperties(new Dictionary<string, string>
                            {
                                {
                                    "userdisplayname", context.UserName
                                },
                                {
                                     "role", "admin"
                                }
                             });

                    var ticket = new AuthenticationTicket(identity, props);
                    context.Validated(ticket);
                }
                else
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    context.Rejected();
                }
            //}

            //else
            //{
            //    context.SetError("invalid_grant", "Provided username and password is incorrect");
            //    context.Rejected();
            //}
            return;


        }

        public override async Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            context.AdditionalResponseParameters.Add("hasChangedPassword", HasChangedPassword.ToString());

            return;
        }
        
    }
}