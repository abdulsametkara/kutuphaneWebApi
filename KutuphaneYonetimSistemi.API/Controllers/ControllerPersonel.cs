using Microsoft.AspNetCore.Mvc;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using KutuphaneYonetimSistemi.Service;
namespace KutuphaneYonetimSistemi.API.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ControllerPersonel : ControllerBase
    {
        private readonly ServicePersonel _servicePersonel;
        public ControllerPersonel(ServicePersonel servicePersonel)
        {
            _servicePersonel = servicePersonel;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var personeller = _servicePersonel.GetirTumPersoneller(new DTOPersonel());
                return Ok(personeller);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var personeller = _servicePersonel.GetirByIdPersonel(id);
                if (personeller == null)
                {
                    return NotFound();
                }
                return Ok(personeller);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create(DTOPersonel dTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                int yeniPersonel = _servicePersonel.EklePersonel(dTO);
                if (yeniPersonel > 0)
                {
                    return CreatedAtAction(
                        nameof(GetById),
                        new { id = yeniPersonel },
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
        public IActionResult Update(int id, DTOPersonel dTO)
        {
            try
            {
                if (id != dTO.PersonelID)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                bool sonuc = _servicePersonel.GuncellePersonel(dTO);
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
                bool sonuc = _servicePersonel.SilPersonel(id);
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
                return BadRequest($"{ex.Message}");
            }
        }


        [HttpGet("ara/{aramaMetni}")]

        public IActionResult Search(string aramaMetni)
        {
            try
            {
                var sonuc = _servicePersonel.AraPersonel(aramaMetni);
                return Ok(sonuc);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
