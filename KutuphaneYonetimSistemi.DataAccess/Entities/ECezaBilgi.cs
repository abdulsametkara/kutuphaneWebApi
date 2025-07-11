using KutuphaneYonetimSistemi.DataAccess.Context;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace KutuphaneYonetimSistemi.DataAccess.Entities
{
    public class ECezaBilgi : EBase
    {
        // 1. CEZA EKLE
        public virtual int  EkleCeza(DTOCezaBilgi dTO)
        {
            int yeniCezaId = 0;

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_EkleCeza", baglanti);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@UyeID", dTO.UyeID);
                command.Parameters.AddWithValue("@IslemID", dTO.IslemID);
                command.Parameters.AddWithValue("@GecikmeSuresi", dTO.GecikmeSuresi);
                command.Parameters.AddWithValue("@CezaTutari", dTO.CezaTutari);
                command.Parameters.AddWithValue("@OdemeDurumu", dTO.OdemeDurumu);
                var odemeTarih =
                dTO.OdemeTarihi == default ||
                dTO.OdemeTarihi <= SqlDateTime.MinValue.Value ? (object)DBNull.Value : dTO.OdemeTarihi;
                command.Parameters.AddWithValue("@OdemeTarihi", odemeTarih);

                command.Parameters.AddWithValue("@Aciklama",
                    string.IsNullOrEmpty(dTO.Aciklama) ? DBNull.Value : (object)dTO.Aciklama);

                baglanti.Open();
                var sonuc = command.ExecuteScalar();
                if (sonuc != null)
                {
                    yeniCezaId = Convert.ToInt32(sonuc);
                }
            }

            return yeniCezaId;
        }

        // 2. CEZA GÜNCELLE
        public virtual bool GuncelleCeza(DTOCezaBilgi dTO)
        {
            try
            {
                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_GuncelleCeza", baglanti);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CezaID", dTO.CezaID);
                    command.Parameters.AddWithValue("@GecikmeSuresi", dTO.GecikmeSuresi);
                    command.Parameters.AddWithValue("@CezaTutari", dTO.CezaTutari);
                    command.Parameters.AddWithValue("@OdemeDurumu", dTO.OdemeDurumu);
                    command.Parameters.AddWithValue("@OdemeTarihi", dTO.OdemeTarihi ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Aciklama", string.IsNullOrEmpty(dTO.Aciklama) ? (object)DBNull.Value : dTO.Aciklama);
                    Console.WriteLine($"Güncellenecek ID: {dTO.CezaID}, Tutar: {dTO.CezaTutari}");

                    baglanti.Open();
                    int result = command.ExecuteNonQuery();

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // 3. CEZA SİL
        public virtual bool SilCeza(DTOCezaBilgi dTO)
        {
            try
            {
                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_SilCeza", baglanti);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CezaID", dTO.CezaID);

                    baglanti.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // 4. ID'YE GÖRE CEZA GETİR
        public virtual DTOCezaBilgi GetirByIDCeza(DTOCezaBilgi dTO)
        {
            DTOCezaBilgi ceza = null;

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirByIDCeza", baglanti);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CezaID", dTO.CezaID);

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ceza = MapReaderToCeza(reader);
                }
            }

            return ceza;
        }

        public virtual List<DTOCezaBilgi> GetirTumCezalar(DTOCezaBilgi dTO)
        {
            List<DTOCezaBilgi> cezaListesi = new List<DTOCezaBilgi>();

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirTumCezalar", baglanti);
                command.CommandType = CommandType.StoredProcedure;

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cezaListesi.Add(MapReaderToCeza(reader));
                }
            }

            return cezaListesi;
        }

        public virtual List<DTOCezaBilgi> GetirByUyeIDCeza(DTOCezaBilgi dTO)
        {
            List<DTOCezaBilgi> cezaListesi = new List<DTOCezaBilgi>();

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirByUyeIDCeza", baglanti);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UyeID", dTO.UyeID);

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cezaListesi.Add(MapReaderToCeza(reader));
                }
            }

            return cezaListesi;
        }


        public virtual List<DTOCezaBilgi> GetirOdenmemisCezalar(DTOCezaBilgi dTO)
        {
            List<DTOCezaBilgi> cezaListesi = new List<DTOCezaBilgi>();

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirOdenmemisCezalar", baglanti);
                command.CommandType = CommandType.StoredProcedure;

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cezaListesi.Add(MapReaderToCeza(reader));
                }
            }

            return cezaListesi;
        }

        // 8. ÖDEME YAP
        public virtual bool OdemeYap(DTOCezaBilgi dTO)
        {
            try
            {
                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_OdemeYap", baglanti);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CezaID", dTO.CezaID);
                    command.Parameters.AddWithValue("@OdemeTarihi", dTO.OdemeTarihi);

                    baglanti.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // 9. GECİKEN CEZALARI GETİR
        public virtual List<DTOCezaBilgi> GetirGecikenCeza(DTOCezaBilgi dTO)
        {
            List<DTOCezaBilgi> cezaListesi = new List<DTOCezaBilgi>();

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                SqlCommand command = new SqlCommand("sp_GetirGecikenCeza", baglanti);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MinimumGecikme",
                    dTO.GecikmeSuresi > 0 ? dTO.GecikmeSuresi : 5); // Default 5 gün

                baglanti.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cezaListesi.Add(MapReaderToCeza(reader));
                }
            }

            return cezaListesi;
        }

        // 10. TOPLAM BORÇ HESAPLA
        public virtual decimal HesaplaToplamBorc(DTOCezaBilgi dTO)
        {
            decimal toplamBorc = 0;

            using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
            {
                    string query = @"SELECT SUM(CezaTutari) as ToplamBorc 
                                   FROM CezaBilgi 
                                   WHERE UyeID = @UyeID AND OdemeDurumu = 0";

                SqlCommand command = new SqlCommand(query, baglanti);
                command.Parameters.AddWithValue("@UyeID", dTO.UyeID);

                baglanti.Open();
                var result = command.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    toplamBorc = Convert.ToDecimal(result);
                }
            }

            return toplamBorc;
        }

        private DTOCezaBilgi MapReaderToCeza(SqlDataReader reader)
        {
            return new DTOCezaBilgi
            {
                CezaID = Convert.ToInt32(reader["CezaID"]),
                UyeID = Convert.ToInt32(reader["UyeID"]),
                IslemID = Convert.ToInt32(reader["IslemID"]),
                GecikmeSuresi = Convert.ToInt32(reader["GecikmeSuresi"]),
                CezaTutari = Convert.ToDouble(reader["CezaTutari"]),
                OdemeDurumu = Convert.ToBoolean(reader["OdemeDurumu"]),
                OdemeTarihi = Convert.ToDateTime(reader["OdemeTarihi"] == DBNull.Value ? null : (DateTime?)reader["OdemeTarihi"]),
                Aciklama = reader["Aciklama"] == DBNull.Value ? null : reader["Aciklama"]?.ToString()
            };
        }
    }
}