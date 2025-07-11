using KutuphaneYonetimSistemi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.DTOs
{
    public class DTOUye
    {
        public int UyeID { get; set; }
        public string TCKimlik { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string KullaniciAdi { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Telefon { get; set; }
        public string Cinsiyet { get; set; }
        public DateTime UyelikTarihi { get; set; }
    }
}
