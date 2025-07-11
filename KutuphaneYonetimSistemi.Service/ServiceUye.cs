using KutuphaneYonetimSistemi.DataAccess.DTOs;
using KutuphaneYonetimSistemi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Service
{
    public class ServiceUye
    {
        public List<DTOUye> GetirTumUyeler(DTOUye dTO)
        {
            EUye eUye = new EUye();
            List<DTOUye> listUye = eUye.GetirTumUyeler(dTO);
            return listUye;
        }

        public DTOUye GetirByIdUye(int uyeId)
        {
            EUye eUye = new EUye();
            DTOUye dTO = new DTOUye { UyeID = uyeId };
            return eUye.GetirByIdUye(dTO);
        }

        public int EkleUye(DTOUye dTO)
        {
            EUye eUye = new EUye();
            return eUye.EkleUye(dTO);
        }

        public bool GuncelleUye(DTOUye dTO)
        {
            EUye eUye = new EUye();
            return eUye.GuncelleUye(dTO);
        }

        public bool SilUye(int uyeId)
        {
            EUye eUye=new EUye();
            DTOUye dTO=new DTOUye { UyeID=uyeId };
            return eUye.SilUye(dTO);
        }
    }
}
