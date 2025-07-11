using KutuphaneYonetimSistemi.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.Interfaces
{
    public interface IEOduncIslem
    {
        int OduncVer(DTOOduncIslem dTO);
        bool TeslimAl(DTOOduncIslem dTO);
        List<DTOOduncIslem> GetirGecikenKitaplar(DTOOduncIslem dTO);
        List<DTOOduncIslem> GetirTumOduncIslemler(DTOOduncIslem dTO);
        DTOOduncIslem GetirOduncIslemById(DTOOduncIslem dTO);
        List<DTOOduncIslem> GetirUyeninOduncKitaplari(DTOOduncIslem dTO);
        List<DTOOduncIslem> GetirUyeninOduncGecmisi(DTOOduncIslem dTO);

    }
}
