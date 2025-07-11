using KutuphaneYonetimSistemi.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.Interfaces
{
    public interface IEUye
    {
        List<DTOUye> GetirTumUyeler(DTOUye dTO);
        DTOUye GetirByIdUye(DTOUye dTO);
        int EkleUye(DTOUye dTO);
        bool GuncelleUye(DTOUye dTO);
        bool SilUye(DTOUye dTO);
    }
}
