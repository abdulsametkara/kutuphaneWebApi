using KutuphaneYonetimSistemi.DataAccess.DTOs;
using KutuphaneYonetimSistemi.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static KutuphaneYonetimSistemi.DataAccess.DTOs.DTOAuthentication;
using Microsoft.Extensions.Configuration;


namespace KutuphaneYonetimSistemi.Service
{
    public class ServiceAuthentication
    {
        private readonly EAuthentication _eAuthentication;
        private readonly IConfiguration _configuration;

        public ServiceAuthentication(EAuthentication eAuthentication, IConfiguration configuration)
        {
            _eAuthentication = eAuthentication;
            _configuration = configuration;
        }

        public LoginResponse Login(Login loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.KullaniciAdi) || string.IsNullOrEmpty(loginDto.Password))
            {
                return new DTOAuthentication.LoginResponse
                {
                    BasariliMi = false,
                    Mesaj = "Username and password cannot be empty"
                };
            }

            var result = _eAuthentication.Login(loginDto);

            // Login başarılıysa JWT token oluştur
            if (result.BasariliMi)
            {
                var token = GenerateJwtToken(result);
                result.Token = token;
            }

            return result;
        }

        public CreateUserResponse CreateUser(CreateUser createUserDto)
        {
            if (string.IsNullOrEmpty(createUserDto.KullaniciAdi) || string.IsNullOrEmpty(createUserDto.Password))
            {
                return new CreateUserResponse
                {
                    BasariliMi = false,
                    Mesaj = "Username and password are required"
                };
            }

            if (string.IsNullOrEmpty(createUserDto.Ad) || string.IsNullOrEmpty(createUserDto.Soyad))
            {
                return new CreateUserResponse
                {
                    BasariliMi = false,
                    Mesaj = "First name and last name are required"
                };
            }

            if (createUserDto.KullaniciTipi != "Uye" && createUserDto.KullaniciTipi != "Personel")
            {
                return new CreateUserResponse
                {
                    BasariliMi = false,
                    Mesaj = "User type must be 'Uye' or 'Personel'"
                };
            }

            return _eAuthentication.CreateUser(createUserDto);
        }

        private string GenerateJwtToken(LoginResponse user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.KullaniciID.ToString()),
                new Claim(ClaimTypes.Name, user.Ad),
                new Claim(ClaimTypes.Role, user.KullaniciTipi)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}