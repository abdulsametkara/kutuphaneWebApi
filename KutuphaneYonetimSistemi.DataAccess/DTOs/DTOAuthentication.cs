using System;

namespace KutuphaneYonetimSistemi.DataAccess.DTOs
{
    public class DTOAuthentication
    {
        public class Login
        {
            public string KullaniciAdi { get; set; }
            public string Password { get; set; }
        }

        public class LoginResponse
        {
            public bool BasariliMi { get; set; }
            public string Mesaj { get; set; }
            public int KullaniciID { get; set; }
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public string KullaniciTipi { get; set; }
            public string Yetki { get; set; }
            public string Token { get; set; } 
        }

        public class CreateUser
        {
            public string TCKimlikNo { get; set; }
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public string Email { get; set; }
            public string KullaniciAdi { get; set; }
            public string Password { get; set; }
            public string KullaniciTipi { get; set; } 

            public DateTime? DogumTarihi { get; set; }
            public string Telefon { get; set; }
            public string Cinsiyet { get; set; }

            public string Yetki { get; set; }
        }

        public class CreateUserResponse
        {
            public bool BasariliMi { get; set; }
            public string Mesaj { get; set; }
            public int KullaniciID { get; set; }
            public string KullaniciTipi { get; set; }
        }
    }
}