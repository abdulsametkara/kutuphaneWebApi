using KutuphaneYonetimSistemi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.DTOs
{
    public class DTOOduncIslem : DTOBase
    {
        public int IslemID { get; set; }
        public int UyeID { get; set; }
        public int KitapID { get; set; }
        public DateTime OduncAlmaTarihi { get; set; }
        public DateTime TeslimEdilmesiGerekenTarih { get; set; }
        public DateTime GercekTeslimTarihi { get; set; } //NULL ise henüz teslim edilmemiş
        public string IslemDurumu { get; set; } //'Odunc', 'TeslimEdildi', 'Gecikti'
        public int PersonelID { get; set; }

    }
}
