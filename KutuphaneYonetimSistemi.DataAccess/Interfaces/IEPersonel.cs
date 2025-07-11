using KutuphaneYonetimSistemi.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.Interfaces
{
    public interface IEPersonel
    {
        List<DTOPersonel> GetirTumPersoneller(DTOPersonel dTO);
        int EklePersonel(DTOPersonel dTO);
        bool GuncellePersonel(DTOPersonel dTO);
        bool SilPersonel(DTOPersonel dTO);
        DTOPersonel GetirByIdPersonel(DTOPersonel dTO);
        List<DTOPersonel> AraPersonel(string arama);

    }
}
