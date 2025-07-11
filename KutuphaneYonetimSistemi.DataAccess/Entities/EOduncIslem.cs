using KutuphaneYonetimSistemi.DataAccess.Context;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace KutuphaneYonetimSistemi.DataAccess.Entities
{
    public class EOduncIslem
    {
        public virtual int OduncVer(DTOOduncIslem dTO)
        {
            int islemId = 0;
            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_OduncVer", baglanti);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UyeID", dTO.UyeID);
                command.Parameters.AddWithValue("@KitapID", dTO.KitapID);
                command.Parameters.AddWithValue("@PersonelID", dTO.PersonelID);

                baglanti.Open();
                var sonuc = command.ExecuteScalar();
                if (sonuc != null)
                {
                    islemId = Convert.ToInt32(sonuc);
                }
            }
            return islemId;
        }

        public virtual bool TeslimAl(DTOOduncIslem dTO)
        {
            try
            {
                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_TeslimAl", baglanti);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IslemID", dTO.IslemID);
                    baglanti.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
        }

        public virtual List<DTOOduncIslem> GetirGecikenKitaplar(DTOOduncIslem dTO)
        {
            List<DTOOduncIslem> gecikenler = new List<DTOOduncIslem>();
            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirGecikenKitaplar",baglanti);
                command.CommandType= CommandType.StoredProcedure;
                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DTOOduncIslem islem = new DTOOduncIslem();
                    islem.IslemID = Convert.ToInt32(reader["IslemID"]);
                    islem.UyeID = Convert.ToInt32(reader["UyeID"]);
                    islem.OduncAlmaTarihi = Convert.ToDateTime(reader["OduncAlmaTarihi"] == DBNull.Value ? null : (DateTime?)reader["OduncAlmaTarihi"]);
                    islem.TeslimEdilmesiGerekenTarih = Convert.ToDateTime(reader["TeslimEdilmesiGerekenTarih"] == DBNull.Value ? null : (DateTime?)reader["TeslimEdilmesiGerekenTarih"]);
                    islem.IslemDurumu = reader["IslemDurumu"].ToString();

                    gecikenler.Add(islem);
                }
            }
            return gecikenler;
        }

        // 4. TÜM ÖDÜNÇ İŞLEMLERİNİ GETİR
        public virtual List<DTOOduncIslem> GetirTumOduncIslemler(DTOOduncIslem dTO)
        {
            List<DTOOduncIslem> islemListesi = new List<DTOOduncIslem>();

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirTumOduncIslemler", baglanti);
                command.CommandType = CommandType.StoredProcedure;

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    islemListesi.Add(MapReaderToOduncIslem(reader));
                }
            }

            return islemListesi;
        }

        // 5. ID'YE GÖRE İŞLEM GETİR
        public virtual DTOOduncIslem GetirByIdOduncIslem(DTOOduncIslem dTO)
        {
            DTOOduncIslem islem = null;

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirByIdOduncIslem", baglanti);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IslemID", dTO.IslemID);

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    islem = MapReaderToOduncIslem(reader);
                }
            }

            return islem;
        }

        // 6. ÜYENİN AKTİF ÖDÜNÇ KİTAPLARI
        public virtual List<DTOOduncIslem> GetirUyeninOduncKitaplari(DTOOduncIslem dTO)
        {
            List<DTOOduncIslem> islemListesi = new List<DTOOduncIslem>();

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirUyeninOduncKitaplari", baglanti);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UyeID", dTO.UyeID);

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    islemListesi.Add(MapReaderToOduncIslem(reader));
                }
            }

            return islemListesi;
        }

        // 7. ÜYENİN ÖDÜNÇ GEÇMİŞİ
        public virtual List<DTOOduncIslem> GetirUyeninOduncGecmisi(DTOOduncIslem dTO)
        {
            List<DTOOduncIslem> islemListesi = new List<DTOOduncIslem>();

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirUyeninOduncGecmisi", baglanti);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UyeID", dTO.UyeID);

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    islemListesi.Add(MapReaderToOduncIslem(reader));
                }
            }

            return islemListesi;
        }
       
        private DTOOduncIslem MapReaderToOduncIslem(SqlDataReader reader)
        {
            return new DTOOduncIslem
            {
                IslemID = Convert.ToInt32(reader["IslemID"]),
                UyeID = Convert.ToInt32(reader["UyeID"]),
                KitapID = Convert.ToInt32(reader["KitapID"]),
                OduncAlmaTarihi = Convert.ToDateTime(reader["OduncAlmaTarihi"]),
                TeslimEdilmesiGerekenTarih = Convert.ToDateTime(reader["TeslimEdilmesiGerekenTarih"] == DBNull.Value ? null : (DateTime?)reader["TeslimEdilmesiGerekenTarih"]),
                GercekTeslimTarihi = Convert.ToDateTime(reader["GercekTeslimTarihi"] == DBNull.Value ? null : (DateTime?)reader["GercekTeslimTarihi"]),
                IslemDurumu = reader["IslemDurumu"].ToString(),
                PersonelID = Convert.ToInt32(reader["PersonelID"])
            };
        }
    }
}
