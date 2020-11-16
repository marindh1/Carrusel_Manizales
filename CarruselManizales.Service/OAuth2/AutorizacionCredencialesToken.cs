using CarruselManizales.Service.Models;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace CarruselManizales.Service.OAuth2
{
    public class AutorizacionCredencialesToken : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        private List<T> Deserializar<T>(string jsonStr) {
            return JsonConvert.DeserializeObject<List<T>>(jsonStr);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var usu = Deserializar<ModeloUsuario>(GetUser(context.UserName, context.Password));
            
            if (usu.Count == 0)
            {
                context.SetError("Sin Acceso", "Usuario o PW incorrecto");
            }
            else
            {
                ClaimsIdentity identidad = new ClaimsIdentity(context.Options.AuthenticationType);
                identidad.AddClaim(new Claim(ClaimTypes.Name, context.Password));

                identidad.AddClaim(new Claim(ClaimTypes.Role, usu[0].Perfil));
                context.Validated(identidad);
            }
        }

        public static string GetUser(string user, string password)
        {
            var url = $"https://localhost:44325/api/Usuarios/login/{user}/{password}";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            return objReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                return ex.ToString();
            }
        }
    }
}