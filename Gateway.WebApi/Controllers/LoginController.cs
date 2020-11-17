using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Gateway.WebApi.Modelo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Gateway.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        private List<T> Deserializar<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonStr);
        }
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();

            ModeloUsuario usu = GetUser(login.UserName, login.Password);

            //User user = AuthenticateUser(login);
            if (usu != null)
            {
                var tokenString = GenerateJWTToken(usu);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = usu,
                });
            }
            return response;
        }
        string GenerateJWTToken(ModeloUsuario userInfo)
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Correo),
                new Claim("fullName", userInfo.Nombre.ToString()),
                new Claim("role",userInfo.Perfil),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(
                    JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(DateTime.Now).ToUniversalTime().ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64
                )
            };

            var signinCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes("VaibhavBhapkar")),
                    SecurityAlgorithms.HmacSha256Signature
                );

            var token = new JwtSecurityToken(
                issuer: "https//localhost:44377/",
                audience: "https//localhost:44377/",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );
            var encodeJwt= new JwtSecurityTokenHandler().WriteToken(token);
            return encodeJwt;
        }

        private static ModeloUsuario GetUser(string user, string password)
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
                            string var = objReader.ReadToEnd();
                            //var item = Deserializer<ModeloUsuario>(var);
                            ModeloUsuario _usu = new ModeloUsuario();
                            _usu.Id = 1;
                            _usu.Correo = "aaa";
                            _usu.Nombre = "David";
                            _usu.Perfil = "Administrador";
                            return _usu;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                return null;
            }
        }
    }
}
