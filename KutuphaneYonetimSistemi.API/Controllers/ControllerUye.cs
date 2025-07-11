using Microsoft.AspNetCore.Mvc;
using KutuphaneYonetimSistemi.Service;
using KutuphaneYonetimSistemi.DataAccess.DTOs;

namespace KutuphaneYonetimSistemi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerUye : ControllerBase
    {
        private readonly ServiceUye _serviceUye;
        public ControllerUye(ServiceUye serviceUye)
        {
            _serviceUye = serviceUye;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var uyeler = _serviceUye.GetirTumUyeler(new DTOUye());
                return Ok(uyeler);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var uye = _serviceUye.GetirByIdUye(id);
                if (uye == null)
                {
                    return NotFound();
                }
                return Ok(uye);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create(DTOUye dTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                int yeniUye = _serviceUye.EkleUye(dTO);
                if (yeniUye > 0)
                {
                    return CreatedAtAction(
                        nameof(GetById),
                        new { id = yeniUye },
                        dTO
                        );
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, DTOUye dTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != dTO.UyeID)
                {
                    return BadRequest();
                }

                bool sonuc = _serviceUye.GuncelleUye(dTO);
                return Ok(sonuc);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool sonuc = _serviceUye.SilUye(id);
                return Ok(sonuc);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
