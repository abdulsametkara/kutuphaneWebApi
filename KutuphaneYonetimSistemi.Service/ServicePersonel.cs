using KutuphaneYonetimSistemi.DataAccess.DTOs;
using KutuphaneYonetimSistemi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Service
{
    public class ServicePersonel
    {
        public List<DTOPersonel> GetirTumPersoneller(DTOPersonel dTO)
        {
            EPersonel ePersonel = new EPersonel();
            List<DTOPersonel> list = ePersonel.GetirTumPersoneller(dTO);
            return list;
        }

        public int EklePersonel(DTOPersonel dTO)
        {
            EPersonel ePersonel = new EPersonel();
            return ePersonel.EklePersonel(dTO);
        }

        public bool GuncellePersonel(DTOPersonel dTO)
        {
            EPersonel ePersonel = new EPersonel();
            return ePersonel.GuncellePersonel(dTO);
        }

        public bool SilPersonel(int personelId)
        {
            EPersonel ePersonel = new EPersonel();
            DTOPersonel dTO = new DTOPersonel { PersonelID = personelId };
            return ePersonel.SilPersonel(dTO);
        }

        public DTOPersonel GetirByIdPersonel(int personelId)
        {
            EPersonel ePersonel = new EPersonel();
            DTOPersonel dTO = new DTOPersonel { PersonelID = personelId };
            return ePersonel.GetirByIdPersonel(dTO);
        }

        public List<DTOPersonel> AraPersonel(string arama)
        {
            EPersonel ePersonel = new EPersonel();
            List<DTOPersonel> list = ePersonel.AraPersonel(arama);
            return list;
        }

    }
}
