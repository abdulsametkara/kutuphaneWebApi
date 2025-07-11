using KutuphaneYonetimSistemi.DataAccess.DTOs;
using KutuphaneYonetimSistemi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Service
{
    public class ServiceOduncIslem
    {
        public int OduncVer(int kitapId, int uyeId, int personelId)
        {
            EOduncIslem eOduncIslem = new EOduncIslem();
            DTOOduncIslem dTO = new DTOOduncIslem { KitapID = kitapId, UyeID = uyeId, PersonelID = personelId };
            return eOduncIslem.OduncVer(dTO);
        }

        public bool TeslimAl(int islemId)
        {
            EOduncIslem eOduncIslem = new EOduncIslem();
            DTOOduncIslem dTO = new DTOOduncIslem { IslemID = islemId };
            return eOduncIslem.TeslimAl(dTO);
        }

        public List<DTOOduncIslem> GetirGecikenKitaplar(DTOOduncIslem dTO)
        {
            EOduncIslem eOduncIslem= new EOduncIslem();
            List<DTOOduncIslem> list = eOduncIslem.GetirGecikenKitaplar(dTO);
            return list;
        }

        public List<DTOOduncIslem> GetirTumOduncIslemler(DTOOduncIslem dTO)
        {
            EOduncIslem eOduncIslem = new EOduncIslem();
            List<DTOOduncIslem> list = eOduncIslem.GetirTumOduncIslemler(dTO);
            return list;
        }

        public DTOOduncIslem GetirByIdOduncIslem(int islemId)
        {
            EOduncIslem eOduncIslem = new EOduncIslem();
            DTOOduncIslem dTO = new DTOOduncIslem { IslemID  = islemId };
            return eOduncIslem.GetirByIdOduncIslem(dTO);
        }

        public List<DTOOduncIslem> GetirUyeninOduncKitaplari(DTOOduncIslem dTO)
        {
            EOduncIslem eOduncIslem= new EOduncIslem();
            List<DTOOduncIslem> list = eOduncIslem.GetirUyeninOduncKitaplari(dTO);
            return list;
        }

        public List<DTOOduncIslem> GetirUyeninOduncGecmisi(DTOOduncIslem dTO)
        {
            EOduncIslem eOduncIslem = new EOduncIslem();
            List<DTOOduncIslem> list = eOduncIslem.GetirUyeninOduncGecmisi(dTO);
            return list;
        }
    }
}
