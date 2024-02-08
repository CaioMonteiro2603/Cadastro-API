using Cadastro.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cadastro.Services
{
    public class AuthenticationService
    {
        public static string GetToken(UsuarioModel usuarioModel)
        {
            byte[] secret = Encoding.ASCII.GetBytes(Settings.SECRET_TOKEN);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim( ClaimTypes.Name , usuarioModel.NomeUsuario),
                    new Claim( ClaimTypes.Email, usuarioModel.EmailUsuario),
                    new Claim( ClaimTypes.Role, usuarioModel.Regra),
                    new Claim( "UsuarioId", usuarioModel.UsuarioId.ToString() )
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = "cadastro",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }
    }
}
