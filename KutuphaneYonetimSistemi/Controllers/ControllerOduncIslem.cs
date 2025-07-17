using Microsoft.AspNetCore.Mvc;
using KutuphaneYonetimSistemi.Service;
using KutuphaneYonetimSistemi.DataAccess.DTOs;

namespace KutuphaneYonetimSistemi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerOduncIslem : ControllerBase
    {
        private readonly ServiceOduncIslem _serviceOduncIslem;
        public ControllerOduncIslem(ServiceOduncIslem serviceOduncIslem)
        {
            _serviceOduncIslem = serviceOduncIslem;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var oduncIslemleri = _serviceOduncIslem.GetirTumOduncIslemler(new DTOOduncIslem());
                return Ok(oduncIslemleri);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, stack = ex.StackTrace });
            }
        }

        [HttpPost("odunc-ver")]
        public IActionResult OduncVer([FromBody] DTOOduncIslem dTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (dTO.KitapID <= 0)
                {
                    return BadRequest("Geçerli bir Kitap ID'si gereklidir.");
                }
                if (dTO.UyeID <= 0)
                {
                    return BadRequest("Geçerli bir Üye ID'si gereklidir.");
                }
                if (dTO.PersonelID <= 0)
                {
                    return BadRequest("Geçerli bir Personel ID'si gereklidir.");
                }

                int yeniIslemId = _serviceOduncIslem.OduncVer(dTO.KitapID, dTO.UyeID, dTO.PersonelID);
                if (yeniIslemId > 0)
                {
                    return CreatedAtAction(
                        nameof(GetirByIdOduncIslem),
                        new { id = yeniIslemId },
                        new { IslemID = yeniIslemId, dTO.KitapID, dTO.UyeID, dTO.PersonelID }
                    );
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, stack = ex.StackTrace });
            }
        }

        [HttpPut("teslim-al/{islemId}")]
        public IActionResult TeslimAl(int islemId)
        {
            try
            {
                if (islemId <= 0)
                {
                    return BadRequest();
                }

                bool sonuc = _serviceOduncIslem.TeslimAl(islemId);
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
                return BadRequest(new { message = ex.Message, stack = ex.StackTrace });
            }
        }

        [HttpGet("geciken-kitaplar")]
        public IActionResult GetirGecikenKitaplar()
        {
            try
            {
                var gecikenler = _serviceOduncIslem.GetirGecikenKitaplar(new DTOOduncIslem());
                return Ok(gecikenler);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, stack = ex.StackTrace });
            }
        }

        [HttpGet("tum-odunc-islemler")]
        public IActionResult GetirTumOduncIslemler()
        {
            try
            {
                var tumOduncIslemler = _serviceOduncIslem.GetirTumOduncIslemler(new DTOOduncIslem());
                return Ok(tumOduncIslemler);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, stack = ex.StackTrace });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetirByIdOduncIslem(int id)
        {
            try
            {
                var islem = _serviceOduncIslem.GetirByIdOduncIslem(id);
                if (islem == null)
                {
                    return BadRequest();
                }
                return Ok(islem);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, stack = ex.StackTrace });
            }
        }

        [HttpGet("uye/{uyeId}/aktif-kitaplar")]
        public IActionResult GetirUyeninOduncKitaplari(int uyeId)
        {
            try
            {
                var dTO = new DTOOduncIslem { UyeID = uyeId };
                var kitaplar = _serviceOduncIslem.GetirUyeninOduncKitaplari(dTO);
                return Ok(kitaplar);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, stack = ex.StackTrace });
            }
        }

        [HttpGet("uye/{uyeId}/gecmis")]
        public IActionResult GetirUyeninOduncGecmisi(int uyeId)
        {
            try
            {
                var dTO = new DTOOduncIslem { UyeID = uyeId };
                var gecmis = _serviceOduncIslem.GetirUyeninOduncGecmisi(dTO);
                return Ok(gecmis);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, stack = ex.StackTrace });
            }
        }
    }
}
