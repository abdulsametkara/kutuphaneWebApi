using KutuphaneYonetimSistemi.DataAccess.DTOs;
using KutuphaneYonetimSistemi.DataAccess.Entities;
using KutuphaneYonetimSistemi.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Service
{
    public class ServiceCezaBilgi
    {
        public int EkleCeza(DTOCezaBilgi dTO)
        {
            ECezaBilgi eCezaBilgi = new ECezaBilgi();

            if (dTO.GecikmeSuresi <= 0)
            {
                throw new Exception("Gecikme süresi 0'dan büyük olmalıdır!");
            }
            else
            {
                if (dTO.CezaTutari == 0)
                {
                    dTO.CezaTutari = dTO.GecikmeSuresi * 10;
                }
            }

            if (string.IsNullOrEmpty(dTO.Aciklama))
            {
                dTO.Aciklama = $"{dTO.GecikmeSuresi} gün gecikme cezası";
            }

            dTO.OdemeDurumu = false;
            dTO.OdemeTarihi = null;

            return eCezaBilgi.EkleCeza(dTO);
        }

        public bool GuncelleCeza(DTOCezaBilgi dTO)
        {
            ECezaBilgi eCezaBilgi = new ECezaBilgi();
            return eCezaBilgi.GuncelleCeza(dTO);
        }
        public bool SilCeza(int cezaId)
        {
            ECezaBilgi eCezaBilgi= new ECezaBilgi();
            DTOCezaBilgi dTO = new DTOCezaBilgi { CezaID = cezaId };
            return eCezaBilgi.SilCeza(dTO);
        }

        public DTOCezaBilgi GetirByIDCeza(int id)
        {
            ECezaBilgi eCezaBilgi = new ECezaBilgi();
            DTOCezaBilgi dTO = new DTOCezaBilgi { CezaID = id };
            return eCezaBilgi.GetirByIDCeza(dTO);
        }

        public List<DTOCezaBilgi> GetirTumCezalar(DTOCezaBilgi dTO)
        {
            ECezaBilgi eCezaBilgi = new ECezaBilgi();
            List<DTOCezaBilgi> list = eCezaBilgi.GetirTumCezalar(dTO);
            return list;
        }

        public List<DTOCezaBilgi> GetirByUyeIDCeza(int uyeId)
        {
            ECezaBilgi eCezaBilgi = new ECezaBilgi();
            DTOCezaBilgi dTO = new DTOCezaBilgi{ UyeID = uyeId };
            return eCezaBilgi.GetirByUyeIDCeza(dTO);    
        }

        public List<DTOCezaBilgi> GetirOdenmemisCezalar(DTOCezaBilgi dTO)
        {
            ECezaBilgi eCezaBilgi = new ECezaBilgi();
            List<DTOCezaBilgi> list = eCezaBilgi.GetirOdenmemisCezalar(dTO);
            return list;
        }

        public bool OdemeYap(int cezaId)
        {
            var ceza = GetirByIDCeza(cezaId); // doğru parametre türü: int

            if (ceza == null)
                throw new Exception("Ceza bulunamadı");

            if (ceza.OdemeDurumu)
                throw new Exception("Bu ceza ödenmiş");

            DTOCezaBilgi dTO = new DTOCezaBilgi
            {
                CezaID = cezaId,
                OdemeDurumu = true,
                OdemeTarihi = DateTime.Now
            };

            return new ECezaBilgi().OdemeYap(dTO);
        }

        public List<DTOCezaBilgi> GetirGecikenCeza(DTOCezaBilgi dTO)
        {
            ECezaBilgi eCezaBilgi = new ECezaBilgi();
            List<DTOCezaBilgi> list = eCezaBilgi.GetirGecikenCeza(dTO);
            return list;
        }
            
        public decimal HesaplaToplamBorc(int uyeId)
        {
            ECezaBilgi eCezaBilgi = new ECezaBilgi();
            DTOCezaBilgi dTO = new DTOCezaBilgi { UyeID= uyeId };
            return eCezaBilgi.HesaplaToplamBorc(dTO);
        }
    }
}
