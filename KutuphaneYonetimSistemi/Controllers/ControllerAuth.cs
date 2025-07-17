using Microsoft.AspNetCore.Mvc;
using KutuphaneYonetimSistemi.Service;
using KutuphaneYonetimSistemi.DataAccess.DTOs;

namespace KutuphaneYonetimSistemi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ServiceAuthentication _authService;

        public AuthController(ServiceAuthentication authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] DTOAuthentication.Login loginDto)
        {
            try
            {
                var result = _authService.Login(loginDto);

                if (result.BasariliMi)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Mesaj,
                        token = result.Token,
                        data = new
                        {
                            userId = result.KullaniciID,
                            firstName = result.Ad,
                            lastName = result.Soyad,
                            userType = result.KullaniciTipi,
                            role = result.Yetki
                        }
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result.Mesaj
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Internal server error: " + ex.Message
                });
            }
        }

        [HttpPost("createUser")]
        public IActionResult CreateUser([FromBody] DTOAuthentication.CreateUser createUserDto)
        {
            try
            {
                var result = _authService.CreateUser(createUserDto);

                if (result.BasariliMi)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Mesaj,
                        data = new
                        {
                            userId = result.KullaniciID,
                            userType = result.KullaniciTipi
                        }
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result.Mesaj
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Internal server error: " + ex.Message
                });
            }
        }
    }
}