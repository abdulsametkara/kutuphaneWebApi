using KutuphaneYonetimSistemi.Service;
using Microsoft.AspNetCore.Mvc;
using KutuphaneYonetimSistemi.DataAccess.DTOs;

namespace KutuphaneYonetimSistemi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerKitap : ControllerBase
    {
        private readonly ServiceKitap _serviceKitap;
        public ControllerKitap(ServiceKitap serviceKitap)
        {
            _serviceKitap = serviceKitap;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var kitaplar = _serviceKitap.GetirKitapListesi(new DTOKitap());
                return Ok(kitaplar);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var kitaplar = _serviceKitap.GetirByIdKitap(id);
                if (kitaplar == null)
                {
                    return NotFound();
                }
                return Ok(kitaplar);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create(DTOKitap dTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                dTO.Durum = true;
                dTO.CreatedTime = DateTime.Now;

                int yeniKitapId = _serviceKitap.EkleKitap(dTO);

                if (yeniKitapId > 0)
                {
                    dTO.KitapID = yeniKitapId;
                    return CreatedAtAction(
                        nameof(GetById),
                        new { id = yeniKitapId },
                        dTO
                        );
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, DTOKitap dTO)
        {
            try
            {
                if (id != dTO.KitapID)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                bool sonuc = _serviceKitap.GuncelleKitap(dTO);
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
                bool sonuc = _serviceKitap.SilKitap(id);
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

        [HttpGet("ara/{aramaMetni}")]

        public IActionResult Search(string aramaMetni)
        {
            try
            {
                var sonuc = _serviceKitap.AraKitap(aramaMetni);
                return Ok(sonuc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
