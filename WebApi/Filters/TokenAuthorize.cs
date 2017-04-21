﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Ajax.Utilities;
using Services;

namespace WebApi.Filters
{
    public class TokenAuthorize : AuthorizationFilterAttribute
    {
        private const string Token = "Token";
        private const string ClientUserAgent = "Client-User-Agent";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var provider = 
                actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(ITokenAuthServices)) as ITokenAuthServices;

            try
            {
                if (actionContext.Request.Headers.Contains(Token))
                {
                    var tokenValue = actionContext.Request.Headers.GetValues(Token).First();

                    // TODO: ip... to be tested live
                    var ip =
                        IPAddress.Parse(((HttpContextBase)actionContext.Request.Properties["MS_HttpContext"]).Request.UserHostAddress)
                            .ToString();
                    
                    var userAgent = actionContext.Request.Headers.GetValues(ClientUserAgent)?.First(); // good sent with httpclient request since it returns null with Request.Headers.GetValues("User-Agent")

                    // Validate Token
                    if (provider != null && !ip.IsNullOrWhiteSpace() && !userAgent.IsNullOrWhiteSpace() && provider.IsTokenValid(tokenValue, ip, userAgent))
                    {
                        // let controller handle the resp, code and obj return
                    }
                    else
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Request" };

                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception e)
            {
                // log e
                // TODO: Should not use Try/catch for logic execution! What if this would throw e as wel... then it would dump a huge error revealing lot of info
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            base.OnAuthorization(actionContext);
        }
    }
}