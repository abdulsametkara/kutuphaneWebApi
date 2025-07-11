using KutuphaneYonetimSistemi.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.Interfaces
{
    namespace KutuphaneYonetimSistemi.DataAccess.Interfaces
    {
        public interface IEKitap
        {
            List<DTOKitap> GetirKitapListesi(DTOKitap dTO); 
            DTOKitap GetirByIdKitap(DTOKitap dTO);     
            int EkleKitap(DTOKitap dTO);       
            bool GuncelleKitap(DTOKitap dTO);          
            bool SilKitap(DTOKitap dTO); 
            List<DTOKitap> AraKitap(DTOKitap dTO);
        }
    }
}
