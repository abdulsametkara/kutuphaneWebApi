using KutuphaneYonetimSistemi.DataAccess.Context;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO.Pipelines;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.Entities
{
    public class EUye
    {

        // Tüm üyeleri getir

        public virtual List<DTOUye> GetirTumUyeler(DTOUye dTO)
        {
            List<DTOUye> uyeListesi = new List<DTOUye>();
            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirTumUyeler", baglanti);
                command.CommandType = CommandType.StoredProcedure;
                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DTOUye uye = new DTOUye();
                    uye.UyeID = Convert.ToInt32(reader["UyeID"]);
                    uye.TCKimlik = reader["TCKimlik"].ToString();
                    uye.Ad = reader["Ad"].ToString();
                    uye.Soyad = reader["Soyad"].ToString();
                    uye.Email = reader["Email"].ToString();
                    uye.KullaniciAdi = reader["KullaniciAdi"].ToString();
                    uye.DogumTarihi = Convert.ToDateTime(reader["DogumTarihi"] == DBNull.Value ? null : (DateTime?)reader["DogumTarihi"]);
                    uye.Cinsiyet = reader["Cinsiyet"].ToString();
                    uye.Telefon = reader["Telefon"].ToString();
                    uye.UyelikTarihi = Convert.ToDateTime(reader["UyelikTarihi"] == DBNull.Value ? null : (DateTime?)reader["UyelikTarihi"]);

                    uyeListesi.Add(uye);
                }
            }
            return uyeListesi;
        }


        // ID'ye göre üye getir

        public virtual DTOUye GetirByIdUye(DTOUye dTO)
        {
            DTOUye uye = null;

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirByIdUye", baglanti);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UyeID", dTO.UyeID);
                
                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    uye = new DTOUye();
                    uye.UyeID = Convert.ToInt32(reader["UyeID"]);
                    uye.TCKimlik = reader["TCKimlik"].ToString();
                    uye.Ad = reader["Ad"].ToString();
                    uye.Soyad = reader["Soyad"].ToString();
                    uye.Email = reader["Email"].ToString();
                    uye.KullaniciAdi = reader["KullaniciAdi"].ToString();
                    uye.DogumTarihi = Convert.ToDateTime(reader["DogumTarihi"] == DBNull.Value ? null : (DateTime?)reader["DogumTarihi"]);
                    uye.Cinsiyet = reader["Cinsiyet"].ToString();
                    uye.Telefon = reader["Telefon"].ToString();
                    uye.UyelikTarihi = Convert.ToDateTime(reader["UyelikTarihi"] == DBNull.Value ? null : (DateTime?)reader["UyelikTarihi"]);
                }
            }
            return uye;
        }

        //UYE EKLE
        public virtual int EkleUye(DTOUye dTO)
        {
            int yeniUyeId = 0;
            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_EkleUye", baglanti);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TCKimlik",dTO.TCKimlik);
                command.Parameters.AddWithValue("@Ad", dTO.Ad);
                command.Parameters.AddWithValue("@Soyad", dTO.Soyad);
                command.Parameters.AddWithValue("@Email", dTO.Email);
                command.Parameters.AddWithValue("@KullaniciAdi", dTO.KullaniciAdi);
                var dogumTarih =
                dTO.DogumTarihi == default ||
                dTO.DogumTarihi <= SqlDateTime.MinValue.Value ? (object)DBNull.Value : dTO.DogumTarihi;
                command.Parameters.AddWithValue("@DogumTarihi", dogumTarih);
                command.Parameters.AddWithValue("Cinsiyet", dTO.Cinsiyet);
                command.Parameters.AddWithValue("Telefon", dTO.Telefon);

                baglanti.Open();
                var sonuc = command.ExecuteScalar();
                if (sonuc != null)
                {
                    yeniUyeId = Convert.ToInt32(sonuc);
                }
            }
            return yeniUyeId;
        }

        //UYE GUNCELLE

        public virtual bool GuncelleUye(DTOUye dTO)
        {
            try
            {
                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_GuncelleUye", baglanti);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UyeID", dTO.UyeID);
                    command.Parameters.AddWithValue("@Ad", dTO.Ad);
                    command.Parameters.AddWithValue("@Soyad", dTO.Soyad);
                    command.Parameters.AddWithValue("@Email", dTO.Email);
                    command.Parameters.AddWithValue("@KullaniciAdi", dTO.KullaniciAdi);
                    command.Parameters.AddWithValue("Telefon", dTO.Telefon);

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

        //UYE SİL

        public virtual bool SilUye(DTOUye dTO)
        {
            try
            {
                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_SilUye", baglanti);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UyeID", dTO.UyeID);
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
    }
}
