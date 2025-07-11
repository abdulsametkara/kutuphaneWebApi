using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.DTOs
{
    public class DTOPersonel : DTOBase
    {
        public int PersonelID { get; set; }
        public string TCKimlikNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string KullaniciAdi { get; set; }
        public string Yetki { get; set; }


    }
}
