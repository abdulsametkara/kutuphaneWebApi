using KutuphaneYonetimSistemi.DataAccess.Context;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program başlatılıyor...");

            try
            {
                using (var conn = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    conn.Open();
                    Console.WriteLine("Veritabanına bağlanıldı.");
                }

                //TestKitapService();
                //TestUyeService();
                TestCezaService();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bağlantı hatası:");
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }


        static void TestKitapService()
        {
            try
            {
                ServiceKitap service = new ServiceKitap();

                // 1. Tüm kitapları getir
                var kitaplar = service.GetirKitapListesi(new DTOKitap());
                Console.WriteLine($"Toplam {kitaplar.Count} kitap bulundu.");

                // 2. Yeni kitap ekle
                DTOKitap yeniKitap = new DTOKitap
                {
                    KitapAdi = "Test Kitap",
                    YazarAdi = "Test Yazar",
                    YayinEvi = "Test Yayınevi",
                    ISBN = "1234567890",
                    SayfaSayisi = 300,
                    BasimYili = 2024,
                    KategoriID = 1,
                    StokAdedi = 5,
                    RafNo = "A-001",
                    Durum = true,
                    CreatedTime = DateTime.Now
                };

                int yeniId = service.EkleKitap(yeniKitap);
                Console.WriteLine($"Yeni kitap eklendi. ID: {yeniId}");

                // 3. Kitap getir
                var kitap = service.GetirByIdKitap(yeniId);
                Console.WriteLine($"Kitap bulundu: {kitap?.KitapAdi}");

                // 4. Kitap güncelle
                var guncelleKitap = new DTOKitap
                {

                    KitapAdi = "Test Kitap – Güncellenmiş",
                    YazarAdi = "Test Yazar",
                    YayinEvi = "Test Yayınevi",
                    ISBN = "1234567890",
                    SayfaSayisi = 350,
                    BasimYili = 2025,
                    KategoriID = 1,
                    StokAdedi = 7,
                    RafNo = "A-002",
                    KitapID = 11
                };

                bool guncellemeSonuc = service.GuncelleKitap(guncelleKitap);
                Console.WriteLine(guncellemeSonuc ? "✓ Kitap başarıyla güncellendi." : "✗ Kitap güncellenemedi.");


                int silinecekid = 12;
                bool silSonuc = service.SilKitap(silinecekid);
                Console.WriteLine(silSonuc ? "silindi" : "silinemedi");

                string arama = "Güncellenmiş";
                var aramaListesi = service.AraKitap(arama);
                Console.WriteLine($" \"{arama}\" için {aramaListesi.Count} sonuç bulundu.");
                foreach (var k in aramaListesi)
                    Console.WriteLine($"   • {k.KitapID} – {k.KitapAdi}");
            }

              
            catch (Exception ex)
            {
                Console.WriteLine($"HATA: {ex.Message}");
            }
        }

        static void TestUyeService()
        {
            try
            {
                ServiceUye service = new ServiceUye();

                var uyeler = service.GetirTumUyeler(new DTOUye());
                Console.WriteLine($"Toplam {uyeler.Count} üye bulundu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HATA: {ex.Message}");
            }
        }



        static void TestCezaService()
        {
            ServiceCezaBilgi service = new ServiceCezaBilgi();
            int UyeID;
            // 1. Ceza ekle
            DTOCezaBilgi cezaKaydi = new DTOCezaBilgi
            {
                UyeID = 2,
                IslemID = 1,
                GecikmeSuresi = 5,
                CezaTutari = 10.50,
                OdemeDurumu = false,
                OdemeTarihi = DateTime.Now.AddDays(10),
                Aciklama = "Test cezası"
            };

            int yeniCezaId = service.EkleCeza(cezaKaydi);
            Console.WriteLine(yeniCezaId > 0 ? $"Yeni ceza eklendi. ID: {yeniCezaId}" : "Ceza eklenemedi.");

            // 2. GetirByIDCeza
            var getirilenCeza = service.GetirByIDCeza(yeniCezaId);
            Console.WriteLine(getirilenCeza != null ? $"Ceza bulundu: {getirilenCeza.Aciklama}" : "Ceza bulunamadı.");


            // 3. GüncelleCeza
            getirilenCeza.GecikmeSuresi = 10;
            getirilenCeza.CezaTutari = 14.00;
            bool guncelleOK = service.GuncelleCeza(getirilenCeza);
            Console.WriteLine(guncelleOK ? "Güncelleme başarılı." : "Güncelleme başarısız.");
            Console.WriteLine($"Güncellenen ID: {getirilenCeza.CezaID}");

            // 4. Ödeme yap
            bool odemeOK = service.OdemeYap( yeniCezaId);
            Console.WriteLine(odemeOK ? "✓ Ödeme kaydedildi." : "✗ Ödeme yapılamadı.");


            // 5. Üyenin ödenmemiş cezalarını listele
            var odenmemis = service.GetirOdenmemisCezalar(new DTOCezaBilgi { UyeID = 2 });
            Console.WriteLine($"Üyenin ödenmemiş ceza adedi: {odenmemis.Count}");

            // 6. Toplam borç (ödenmemiş) hesapla
            decimal toplamBorc = service.HesaplaToplamBorc(UyeID = 2 );
            Console.WriteLine($"Üyenin toplam borcu: {toplamBorc:0.##} ₺");

            // 7. Geciken cezaları getir
            var geciken = service.GetirGecikenCeza(new DTOCezaBilgi { GecikmeSuresi = 5 });
            Console.WriteLine($"Geciken ceza adedi: {geciken.Count}");

            // 8. Üyenin tüm cezalarını getir
            
            var tumCezalar = service.GetirByUyeIDCeza(UyeID = 2);
            Console.WriteLine($"Üyenin tüm ceza adedi: {tumCezalar.Count}");

            // 9. Tüm cezaları getir
            var hepsi = service.GetirTumCezalar(new DTOCezaBilgi());
            Console.WriteLine($"Tüm ceza kaydı: {hepsi.Count}");

            // 10. Ceza sil
            bool silOK = service.SilCeza( yeniCezaId );
            Console.WriteLine(silOK ? "✓ Ceza silindi." : "✗ Ceza silinemedi.");
        }


    }

}
