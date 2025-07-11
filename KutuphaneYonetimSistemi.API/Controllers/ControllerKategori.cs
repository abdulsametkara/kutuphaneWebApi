using Microsoft.AspNetCore.Mvc;
using KutuphaneYonetimSistemi.DataAccess;
using KutuphaneYonetimSistemi.Service;
using KutuphaneYonetimSistemi.DataAccess.DTOs;


namespace KutuphaneYonetimSistemi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerKategori : ControllerBase
    {
        private readonly ServiceKategori _serviceKategori;
        public ControllerKategori(ServiceKategori serviceKategori)
        {
            _serviceKategori = serviceKategori;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var kategoriler = _serviceKategori.GetirKategoriListesi(new DTOKategori());
                return Ok(kategoriler);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, stack = ex.StackTrace });

            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var kategori = _serviceKategori.GetirByIdKategori(id);
                if (kategori == null)
                {
                    return BadRequest();
                }
                return Ok(kategori);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] DTOKategori dTO)
        {
            try
            {
                dTO.KategoriID = null;
                int yeniKategori = _serviceKategori.EkleKategori(dTO);

                if (yeniKategori > 0)
                {
                    return CreatedAtAction(
                        nameof(GetById),
                        new { id = yeniKategori },
                        new
                        {
                            KategoriID = yeniKategori,
                            KategoriAdi = dTO.KategoriAdi,
                            Aciklama = dTO.Aciklama,
                            KitapSayisi = dTO.KitapSayisi
                        }
                    );
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(DTOKategori dTO, int id)
        {
            try
            {
                dTO.KategoriID = id;
                if (dTO.KategoriID != id)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                bool sonuc = _serviceKategori.GuncelleKategori(dTO);
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool sonuc = _serviceKategori.SilKategori(id);
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
                return BadRequest();
            }
        }

        [HttpGet("{id}/kitaplar")]
        public IActionResult GetirKategoriyeGoreKitaplar(int id)
        {
            try
            {
                var kitaplar = _serviceKategori.GetirKategoriyeGoreKitaplar(id);
                return Ok(kitaplar);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, stack = ex.StackTrace });
            }
        }

        [HttpGet("ara/{aramaMetni}")]
        public IActionResult Search(string aramaMetni)
        {
            try
            {
                var dTO = new DTOKategori { KategoriAdi = aramaMetni };
                var sonuclar = _serviceKategori.AraKategori(dTO);
                return Ok(sonuclar);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
