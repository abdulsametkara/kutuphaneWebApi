using KutuphaneYonetimSistemi.DataAccess.Context;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace KutuphaneYonetimSistemi.DataAccess.Entities
{
    public class EPersonel
    {
        public virtual List<DTOPersonel> GetirTumPersoneller(DTOPersonel dTO)
        {
            List<DTOPersonel> personelListe = new List<DTOPersonel>();
            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirTumPersoneller", baglanti);
                command.CommandType = CommandType.StoredProcedure;
                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    personelListe.Add(MapReaderToPersonel(reader));
                }
                return personelListe;
            }
        }

        public virtual int EklePersonel(DTOPersonel dTO)
        {
            int yeniId = 0;

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_EklePersonel", baglanti);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("TCKimlikNo", dTO.TCKimlikNo);
                command.Parameters.AddWithValue("@Ad", dTO.Ad);
                command.Parameters.AddWithValue("@Soyad", dTO.Soyad);
                command.Parameters.AddWithValue("@Email", dTO.Email);
                command.Parameters.AddWithValue("@KullaniciAdi", dTO.KullaniciAdi);
                command.Parameters.AddWithValue("Yetki", dTO.Yetki);

                baglanti.Open();
                var sonuc = command.ExecuteScalar();
                if (sonuc != null)
                {
                    yeniId = Convert.ToInt32(sonuc);
                }

                return yeniId;
            }
        }

        public virtual bool GuncellePersonel(DTOPersonel dTO)
        {
            try
            {
                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_GuncellePersonel", baglanti);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonelID", dTO.PersonelID);
                    command.Parameters.AddWithValue("TCKimlikNo", dTO.TCKimlikNo);
                    command.Parameters.AddWithValue("@Ad", dTO.Ad);
                    command.Parameters.AddWithValue("@Soyad", dTO.Soyad);
                    command.Parameters.AddWithValue("@Email", dTO.Email);
                    command.Parameters.AddWithValue("@KullaniciAdi", dTO.KullaniciAdi);
                    command.Parameters.AddWithValue("Yetki", dTO.Yetki);

                    baglanti.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual bool SilPersonel(DTOPersonel dTO)
        {
            try
            {
                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_SilPersonel", baglanti);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonelID" ,dTO.PersonelID);

                    baglanti.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual DTOPersonel GetirByIdPersonel(DTOPersonel dTO)
        {
            DTOPersonel personel = null;
            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirByIdPersonel", baglanti);
                command.CommandType= CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonelID", dTO.PersonelID);

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    personel = MapReaderToPersonel(reader);
                }

                return personel;
            }
        }

        public virtual List<DTOPersonel> AraPersonel(string arama)
        {
            List<DTOPersonel> liste = new List<DTOPersonel>();

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand komut = new SqlCommand("sp_AraPersonel", baglanti);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.AddWithValue("@AramaMetni", arama);

                baglanti.Open();
                SqlDataReader reader = komut.ExecuteReader();

                while (reader.Read())
                    liste.Add(MapReaderToPersonel(reader));
            }

            return liste;
        }

        private DTOPersonel MapReaderToPersonel(SqlDataReader reader)
        {
            return new DTOPersonel
            {
                PersonelID = Convert.ToInt32(reader["PersonelID"]),
                Ad = reader["Ad"].ToString(),
                Soyad = reader["Soyad"].ToString(),
                KullaniciAdi = reader["KullaniciAdi"].ToString(),
                Yetki = reader["Yetki"].ToString()
            };
        }
    }
}
