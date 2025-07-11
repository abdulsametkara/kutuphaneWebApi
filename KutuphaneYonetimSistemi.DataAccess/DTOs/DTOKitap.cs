using KutuphaneYonetimSistemi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.DTOs
{
    public class DTOKitap : DTOBase
    {
        public int KitapID { get; set; }
        public string KitapAdi { get; set; }
        public string YazarAdi { get; set; }
        public string YayinEvi { get; set; }
        public string ISBN { get; set; }
        public int SayfaSayisi { get; set; }
        public int BasimYili { get; set; }
        public int KategoriID { get; set; }
        public string KategoriAdi { get; set; }

        public int StokAdedi { get; set; }
        public string RafNo { get; set; }
        public bool Durum { get; set; }

    }
}
