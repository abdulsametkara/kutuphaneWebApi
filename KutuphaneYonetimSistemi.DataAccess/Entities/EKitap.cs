using KutuphaneYonetimSistemi.DataAccess.Context;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using KutuphaneYonetimSistemi.DataAccess.Interfaces;
using KutuphaneYonetimSistemi.DataAccess.Interfaces.KutuphaneYonetimSistemi.DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.Entities
{
    public class EKitap : IEKitap
    {
        // 1. TÜM KİTAPLARI GETİR
        public virtual List<DTOKitap> GetirKitapListesi(DTOKitap dTO)
        {
            List<DTOKitap> kitapListesi = new List<DTOKitap>();

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirTumKitaplar", baglanti);
                command.CommandType = CommandType.StoredProcedure;

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DTOKitap kitap = new DTOKitap();
                    kitapListesi.Add(MapReaderToKitap(reader));
                }
            }

            return kitapListesi;
        }

        // 2. ID'YE GÖRE KİTAP GETİR
        public virtual DTOKitap GetirByIdKitap(DTOKitap dTO)
        {
            DTOKitap kitap = null;

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {

                SqlCommand command = new SqlCommand("sp_GetirByIdKitap", baglanti);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@KitapID", dTO.KitapID);

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    kitap = new DTOKitap();
                    kitap.KitapID = Convert.ToInt32(reader["KitapID"]);
                    kitap.KitapAdi = reader["KitapAdi"].ToString();
                    kitap.YazarAdi = reader["YazarAdi"].ToString();
                    kitap.YayinEvi = reader["YayinEvi"].ToString();
                    kitap.ISBN = reader["ISBN"].ToString();
                    kitap.SayfaSayisi = Convert.ToInt32(reader["SayfaSayisi"]);
                    kitap.BasimYili = Convert.ToInt32(reader["BasimYili"]);
                    kitap.KategoriID = Convert.ToInt32(reader["KategoriID"]);
                    kitap.StokAdedi = Convert.ToInt32(reader["StokAdedi"]);
                    kitap.RafNo = reader["RafNo"].ToString();
                    kitap.Durum = Convert.ToBoolean(reader["Durum"]);
                    kitap.CreatedTime = Convert.ToDateTime(reader["EklenmeTarihi"]);
                }
            }

            return kitap;
        }

        // 3. KİTAP EKLE
        public virtual int EkleKitap(DTOKitap dTO)
        {
            int yeniKitapId = 0;

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_EkleKitap", baglanti);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@KitapAdi", dTO.KitapAdi);
                command.Parameters.AddWithValue("@YazarAdi", dTO.YazarAdi);
                command.Parameters.AddWithValue("@YayinEvi", dTO.YayinEvi);
                command.Parameters.AddWithValue("@ISBN", dTO.ISBN);
                command.Parameters.AddWithValue("@SayfaSayisi", dTO.SayfaSayisi);
                command.Parameters.AddWithValue("@BasimYili", dTO.BasimYili);
                command.Parameters.AddWithValue("@KategoriID", dTO.KategoriID);
                command.Parameters.AddWithValue("@StokAdedi", dTO.StokAdedi);
                command.Parameters.AddWithValue("@RafNo", dTO.RafNo);

                baglanti.Open();
                var sonuc = command.ExecuteScalar();
                if (sonuc != null)
                {
                    yeniKitapId = Convert.ToInt32(sonuc);
                }
            }

            return yeniKitapId;
        }

        // 4. KİTAP GÜNCELLE
        public virtual bool GuncelleKitap(DTOKitap dTO)
        {
            try
            {
                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_GuncelleKitap", baglanti);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@KitapID", dTO.KitapID);
                    command.Parameters.AddWithValue("@KitapAdi", dTO.KitapAdi);
                    command.Parameters.AddWithValue("@YazarAdi", dTO.YazarAdi);
                    command.Parameters.AddWithValue("@YayinEvi", dTO.YayinEvi);
                    command.Parameters.AddWithValue("@ISBN", dTO.ISBN);
                    command.Parameters.AddWithValue("@SayfaSayisi", dTO.SayfaSayisi);
                    command.Parameters.AddWithValue("@BasimYili", dTO.BasimYili);
                    command.Parameters.AddWithValue("@KategoriID", dTO.KategoriID);
                    command.Parameters.AddWithValue("@StokAdedi", dTO.StokAdedi);
                    command.Parameters.AddWithValue("@RafNo", dTO.RafNo);

                    baglanti.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: Güncelle işlemi başarısız oldu." + ex.Message);
                return false;
            }
        }

        // 5. KİTAP SİL
        public virtual bool SilKitap(DTOKitap dTO)
        {
            try
            {
                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_SilKitap", baglanti);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@KitapID", dTO.KitapID);

                    baglanti.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: Kitap silme başarısız oldu." + ex.Message);
                return false;
            }
        }

        // 6. KİTAP ARA
        public virtual List<DTOKitap> AraKitap(DTOKitap dTO)
        {
            List<DTOKitap> kitapListesi = new List<DTOKitap>();

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_AraKitap", baglanti);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AramaMetni", dTO.KitapAdi);

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DTOKitap kitap = new DTOKitap();
                    kitapListesi.Add(MapReaderToKitap(reader)); 
                }
            }

            return kitapListesi;
        }


        private DTOKitap MapReaderToKitap(SqlDataReader reader)
        {
            return new DTOKitap
            {
                KitapID = Convert.ToInt32(reader["KitapID"]),
                KitapAdi = reader["KitapAdi"].ToString(),
                YazarAdi = reader["YazarAdi"].ToString(),
                YayinEvi = reader["YayinEvi"].ToString(),
                ISBN = reader["ISBN"].ToString(),
                SayfaSayisi = Convert.ToInt32(reader["SayfaSayisi"]),
                BasimYili = Convert.ToInt32(reader["BasimYili"]),
                KategoriID = Convert.ToInt32(reader["KategoriID"]),
                KategoriAdi = reader["KategoriAdi"].ToString(),
                StokAdedi = Convert.ToInt32(reader["StokAdedi"]),
                RafNo = reader["RafNo"].ToString(),
                Durum = Convert.ToBoolean(reader["Durum"]),
                CreatedTime = Convert.ToDateTime(reader["EklenmeTarihi"]),

            };
        }
    }
}
