using Microsoft.Owin;

[assembly: OwinStartup(typeof(CarruselManizales.Service.Startup))]

namespace CarruselManizales.Service
{
    using Microsoft.Owin.Security.OAuth;
    using Owin;
    using System;
    using System.Web;
    using System.Web.Http;
    using CarruselManizales.Service.OAuth2;
    public class Startup : System.Web.HttpApplication
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigurarOauth(app);
            WebApiConfig.Register(config);
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //app.UseWebApi(config);
        }

        private void ConfigurarOauth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AutorizacionCredencialesToken()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
