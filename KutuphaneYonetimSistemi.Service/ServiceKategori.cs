using AutoMapper.Configuration.Conventions;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using KutuphaneYonetimSistemi.DataAccess.Entities.KutuphaneYonetimSistemi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Service
{
    public class ServiceKategori
    {
        public List<DTOKategori> GetirKategoriListesi(DTOKategori dTO)
        {
            EKategori eKategori = new EKategori();
            List<DTOKategori> list = eKategori.GetirKategoriListesi(dTO);
            return list;
        }

        public DTOKategori GetirByIdKategori(int kategoriId)
        {
            EKategori eKategori=new EKategori();
            int a = 0;
            int b = 3;
            var c = b / a;
            DTOKategori dTO = new DTOKategori { KategoriID = kategoriId };
            return eKategori.GetirByIdKategori(dTO);
        }
        public int EkleKategori(DTOKategori dTO)
        {
            EKategori eKategori = new EKategori();
            return eKategori.EkleKategori(dTO);
        }

        public bool GuncelleKategori(DTOKategori dTO)
        {
            EKategori eKategori = new EKategori();
            return eKategori.GuncelleKategori(dTO);
        }

        public bool SilKategori(int kategoriId)
        {
            EKategori eKategori = new EKategori();
            DTOKategori dTO = new DTOKategori { KategoriID = kategoriId };
            return eKategori.SilKategori(dTO);
        }

        public List<DTOKitap> GetirKategoriyeGoreKitaplar(int kategoriId)
        {
            EKategori eKategori = new EKategori();
            return eKategori.GetirKategoriyeGoreKitaplar(kategoriId);
        }


        public List<DTOKategori> AraKategori(DTOKategori dTO)
        {
            EKategori eKategori=new EKategori();
            List<DTOKategori> list = eKategori.AraKategori(dTO);
            return list;
        }

    }
}
