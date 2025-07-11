using KutuphaneYonetimSistemi.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.Interfaces
{
    public interface IECezaBilgi
    {
        int EkleCeza(DTOCezaBilgi dTO);
        bool GuncelleCeza(DTOCezaBilgi dTO);
        bool SilCeza(DTOCezaBilgi dTO);
        DTOCezaBilgi GetirByIDCeza(DTOCezaBilgi dTO);
        List<DTOCezaBilgi> GetirTumCezalar(DTOCezaBilgi dTO);
        List<DTOCezaBilgi> GetirByUyeIDCeza(DTOCezaBilgi dTO);
        List<DTOCezaBilgi> GetirOdenmemisCezalar(DTOCezaBilgi dTO);
        bool OdemeYap(DTOCezaBilgi dTO);
        List<DTOCezaBilgi> GetirGecikenCeza(DTOCezaBilgi dTO);
        decimal HesaplaToplamBorc(DTOCezaBilgi dTO);

    }
}
