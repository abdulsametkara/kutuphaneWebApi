using Microsoft.AspNetCore.Mvc;
using KutuphaneYonetimSistemi.DataAccess;
using KutuphaneYonetimSistemi.Service;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace KutuphaneYonetimSistemi.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerCezaBilgi : ControllerBase
    {
        private readonly ServiceCezaBilgi _serviceCezaBilgi;

        public ControllerCezaBilgi(ServiceCezaBilgi serviceCezaBilgi)
        {
            _serviceCezaBilgi = serviceCezaBilgi;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var cezalar = _serviceCezaBilgi.GetirTumCezalar(new DTOCezaBilgi());
                return Ok(cezalar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var ceza = _serviceCezaBilgi.GetirByIDCeza(id);

                if (ceza == null)
                {
                    return BadRequest();
                }
                return Ok(ceza);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpGet("uye/{uyeId}")]
        public IActionResult GetUyeCezalari(int uyeId)
        {
            try
            {
                var cezalar = _serviceCezaBilgi.GetirByUyeIDCeza(uyeId);
                var toplamBorc = _serviceCezaBilgi.HesaplaToplamBorc(uyeId);
                return Ok(cezalar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(DTOCezaBilgi dTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                int yeniCezaId = _serviceCezaBilgi.EkleCeza(dTO);
                if (yeniCezaId > 0)
                {
                    return CreatedAtAction(
                        nameof(GetById),
                        new { id = yeniCezaId },
                        dTO
                        );
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(DTOCezaBilgi dTO, int id)
        {
            try
            {
                if (dTO.CezaID != id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                bool sonuc = _serviceCezaBilgi.GuncelleCeza(dTO);
                if (sonuc)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool sonuc = _serviceCezaBilgi.SilCeza(id);
                if (sonuc)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpPost("ode/{cezaId}")]
        public IActionResult OdemeYap(int cezaId)
        {
            try
            {
                bool sonuc = _serviceCezaBilgi.OdemeYap(cezaId);
                if (sonuc)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new { success = false, error = "Ceza bulunamadı veya zaten ödenmiş." });

                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }

        [HttpGet("odenmemis")]
        public IActionResult OdenmemisCezalar()
        {
            try
            {
                var cezalar = _serviceCezaBilgi.GetirOdenmemisCezalar(new DTOCezaBilgi());
                return Ok(cezalar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, error = ex.Message });
            }
        }
    }
}
