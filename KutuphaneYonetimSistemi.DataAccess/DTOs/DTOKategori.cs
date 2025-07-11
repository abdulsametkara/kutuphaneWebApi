using KutuphaneYonetimSistemi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.DTOs
{
    public class DTOKategori : DTOBase
    {
        public int? KategoriID { get; set; }
        public string KategoriAdi { get; set; }
        public string Aciklama { get; set; }
        public int? KitapSayisi { get; set; }

    }
}
