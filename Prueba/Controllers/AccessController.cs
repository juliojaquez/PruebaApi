using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Prueba.Models;

namespace Prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly CoreContext _context;
        IConfiguration _configuration;

        public AccessController(IConfiguration configuration, CoreContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [AllowAnonymous,HttpPost, Route("GetToken")]
        public ActionResult<string> GetToken([FromBody] User User)
        {

            if (!_context.User.Any(x => x.Email == User.Email && x.Password == EncryptPass(User.Password)))
            {
                return NotFound();
            }

            //Get User.
            User userLogged = _context.User.Where(x => x.Email == User.Email && x.Password == EncryptPass(User.Password)).FirstOrDefault();

            //Se comienza con la creación del token.
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();

            // Leemos el secret_key desde nuestro appseting
            var secretKey = _configuration.GetValue<string>("JWT:SecretKey");
            var key = Encoding.UTF8.GetBytes(secretKey);

            // Creamos los claims (pertenencias, características) del usuario
            List<Claim> ClaimsList = new List<Claim>();
            ClaimsList.Add(new Claim(ClaimTypes.NameIdentifier, userLogged.Email));
            ClaimsList.Add(new Claim(ClaimTypes.Name, userLogged.Name));
            ClaimsList.Add(new Claim(ClaimTypes.Surname, userLogged.FirstSurname + " " + userLogged.SecondSurname));
            ClaimsList.Add(new Claim(ClaimTypes.Email, userLogged.Email));


            var claims = ClaimsList.ToArray();

            //Se serializa a json el objecto.
            claimsIdentity = new ClaimsIdentity(claims, "jwt");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                // Nuestro token va a durar un día
                Expires = DateTime.UtcNow.AddDays(1),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = new JwtSecurityToken(

                expires: DateTime.Now.AddHours(1),
                signingCredentials: tokenDescriptor.SigningCredentials,
                claims: claims
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenEnsi = tokenHandler.WriteToken(token);

            return tokenEnsi;
        }

        public string EncryptPass(string inputString)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(inputString);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}