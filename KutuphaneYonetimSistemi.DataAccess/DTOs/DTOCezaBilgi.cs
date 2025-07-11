using KutuphaneYonetimSistemi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.DTOs
{
    public class DTOCezaBilgi : DTOBase
    {
        public int CezaID { get; set; }
        public int UyeID { get; set; }
        public int IslemID { get; set; }
        public int GecikmeSuresi { get; set; }
        public double CezaTutari { get; set; }
        public bool OdemeDurumu { get; set; }
        public DateTime? OdemeTarihi { get; set; }
        public string Aciklama { get; set; }
    }
}
