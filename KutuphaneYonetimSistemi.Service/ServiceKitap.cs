using Azure;
using Azure.Core;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using KutuphaneYonetimSistemi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Service
{
    public class ServiceKitap
    {
        //entity e gitme responce ve request 

        //ui den gelir
        public List<DTOKitap> GetirKitapListesi(DTOKitap dTO)
        {
            EKitap eKitap = new EKitap();
            List<DTOKitap> listDTO = eKitap.GetirKitapListesi(dTO);
            return listDTO;
        }

        public DTOKitap GetirByIdKitap(int kitapId)
        {
            EKitap eKitap = new EKitap();
            DTOKitap dTO = new DTOKitap { KitapID = kitapId };
            return eKitap.GetirByIdKitap(dTO);
        }

        public int EkleKitap(DTOKitap dTO)
        {
            EKitap eKitap = new EKitap();
            return eKitap.EkleKitap(dTO);
        }

        public bool GuncelleKitap(DTOKitap dTO)
        {
            EKitap eKitap = new EKitap();
            return eKitap.GuncelleKitap(dTO);
        }

        public bool SilKitap(int kitapId)
        {
            EKitap eKitap = new EKitap();
            DTOKitap dTO = new DTOKitap { KitapID = kitapId };
            return eKitap.SilKitap(dTO);
        }

        public List<DTOKitap> AraKitap(string aramaMetni)
        {
            EKitap eKitap = new EKitap();
            DTOKitap dTO = new DTOKitap { KitapAdi = aramaMetni };
            return eKitap.AraKitap(dTO);
        }
    }
}
