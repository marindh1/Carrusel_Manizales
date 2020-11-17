using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Getawey.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Getawey.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private IConfiguration _config;

        private List<T> Deserializar<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonStr);
        }

        public LoginController(IConfiguration config) {
            _config = config;
        }

        [HttpGet]
        public IActionResult Login(UserModel login) {
            IActionResult response = Unauthorized();

            var user = AuthenticateUser(login);
            if (user != null) {
                var tokenStr = GenerarJSONWebToken(user);
                response = Ok(new
                {
                    token = tokenStr
                });
            };

            return response;
        }

        private ModeloUsuario AuthenticateUser(UserModel login) {
            ModeloUsuario user = null;

            var n_usu = GetUser(login.UserName, login.Password);
            var usu = Deserializar<ModeloUsuario>(n_usu);
            if (usu.Count > 0) 
            {
                user = new ModeloUsuario { 
                    Id = usu[0].Id, 
                    Identificacion = usu[0].Identificacion, 
                    Nombre = usu[0].Nombre,
                    Apellido = usu[0].Apellido,
                    Direccion = usu[0].Direccion,
                    Telefono = usu[0].Telefono,
                    Perfil = usu[0].Perfil,
                    Correo = usu[0].Correo,
                    Clave = usu[0].Clave
                };
            }
            return user;
        }

        private string GenerarJSONWebToken(ModeloUsuario userinfo) {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userinfo.Correo),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;
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


        [Authorize]
        [HttpPost("Post")]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;
            return "Welcome to:" + userName;
        }

        [Authorize]
        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Value1", "Value2", "Value3" };
        }
    }
}
