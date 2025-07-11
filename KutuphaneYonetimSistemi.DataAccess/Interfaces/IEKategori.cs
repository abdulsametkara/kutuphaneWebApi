using KutuphaneYonetimSistemi.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.Interfaces
{
    public interface IEKategori
    {
        List<DTOKategori> GetirKategoriListesi(DTOKategori dTO);
        DTOKategori GetirByIdKategori(DTOKategori dTO);
        int EkleKategori(DTOKategori dTO);
        bool GuncelleKategori(DTOKategori dTO);
        bool SilKategori(DTOKategori dTO);
        List<DTOKitap> GetirKategoriyeGoreKitaplar(DTOKategori dTO);
        List<DTOKategori> AraKategori(DTOKategori dTO);
    }
}
