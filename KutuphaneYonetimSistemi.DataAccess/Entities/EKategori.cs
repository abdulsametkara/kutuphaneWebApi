using KutuphaneYonetimSistemi.DataAccess.Context;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneYonetimSistemi.DataAccess.Entities
{
    namespace KutuphaneYonetimSistemi.DataAccess.Entities
    {
        public class EKategori
        {
            // Tüm kategorileri getir
            public virtual List<DTOKategori> GetirKategoriListesi(DTOKategori dTO)
            {
                List<DTOKategori> kategoriListesi = new List<DTOKategori>();

                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_GetirTumKategoriler", baglanti);
                    command.CommandType = CommandType.StoredProcedure;

                    baglanti.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DTOKategori kategori = new DTOKategori();
                        kategori.KategoriID = Convert.ToInt32(reader["KategoriID"]);
                        kategori.KategoriAdi = reader["KategoriAdi"].ToString();
                        kategori.Aciklama = reader["Aciklama"] == DBNull.Value ? null : reader["Aciklama"]?.ToString();
                        kategori.KitapSayisi = reader["KitapSayisi"] == DBNull.Value ? 0 : Convert.ToInt32(reader["KitapSayisi"]);


                        kategoriListesi.Add(kategori);
                    }
                }
                return kategoriListesi;
            }

            public virtual DTOKategori GetirByIdKategori(DTOKategori dTO)
            {
                DTOKategori kategori = null;
                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_GetirByIdKategori", baglanti);
                    command.CommandType= CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@KategoriID", dTO.KategoriID);

                    baglanti.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        kategori = new DTOKategori();
                        kategori.KategoriID = Convert.ToInt32(reader["KategoriID"]);
                        kategori.KategoriAdi = reader["KategoriAdi"]?.ToString();
                        kategori.Aciklama = reader["Aciklama"] == DBNull.Value ? null : reader["Aciklama"]?.ToString();
                        kategori.KitapSayisi = reader["KitapSayisi"] == DBNull.Value ? 0 : Convert.ToInt32(reader["KitapSayisi"]);

                    }
                }
                return kategori;
            }

            public virtual int EkleKategori(DTOKategori dTO)
            {
                int yeniKategori = 0;

                try
                {
                    using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                    {
                        Console.WriteLine($"Connection String: {baglanti.ConnectionString}");

                        SqlCommand command = new SqlCommand("sp_EkleKategori", baglanti);
                        command.CommandType = CommandType.StoredProcedure;

                        // Parametreleri ekleyin
                        command.Parameters.AddWithValue("@KategoriAdi", dTO.KategoriAdi ?? string.Empty);
                        command.Parameters.AddWithValue("@Aciklama", string.IsNullOrEmpty(dTO.Aciklama) ? (object)DBNull.Value : dTO.Aciklama);
                        command.Parameters.AddWithValue("@KitapSayisi", dTO.KitapSayisi);

                        Console.WriteLine($"Executing SP with params: KategoriAdi='{dTO.KategoriAdi}', Aciklama='{dTO.Aciklama}', KitapSayisi={dTO.KitapSayisi}");

                        baglanti.Open();

                        // ExecuteScalar ile sonucu alın
                        var sonuc = command.ExecuteScalar();

                        Console.WriteLine($"SP Result: {sonuc} (Type: {sonuc?.GetType().Name})");

                        if (sonuc != null && sonuc != DBNull.Value)
                        {
                            // Farklı dönüş tiplerini handle et
                            if (sonuc is int)
                            {
                                yeniKategori = (int)sonuc;
                            }
                            else if (sonuc is decimal)
                            {
                                yeniKategori = Convert.ToInt32(sonuc);
                            }
                            else if (sonuc is long)
                            {
                                yeniKategori = Convert.ToInt32(sonuc);
                            }
                            else
                            {
                                yeniKategori = Convert.ToInt32(sonuc);
                            }

                            Console.WriteLine($"Converted ID: {yeniKategori}");
                        }
                        else
                        {
                            Console.WriteLine("SP returned null or DBNull");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"EkleKategori Exception: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                    throw;
                }

                return yeniKategori;
            }

            public virtual bool GuncelleKategori(DTOKategori dTO)
            {
                try
                {
                    using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                    {
                        SqlCommand command = new SqlCommand("sp_GuncelleKategori", baglanti);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@KategoriID", dTO.KategoriID);
                        command.Parameters.AddWithValue("@KategoriAdi", dTO.KategoriAdi);
                        command.Parameters.AddWithValue("@Aciklama", string.IsNullOrEmpty(dTO.Aciklama) ? (object)DBNull.Value : dTO.Aciklama);
                        command.Parameters.AddWithValue("@KitapSayisi", dTO.KitapSayisi);

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

            public virtual bool SilKategori(DTOKategori dTO)
            {
                try
                {
                    using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                    {
                        SqlCommand command = new SqlCommand("sp_SilKategori", baglanti);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@KategoriID", dTO.KategoriID);

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

            // 7. KATEGORİYE GÖRE KİTAPLARI GETİR
            public virtual List<DTOKitap> GetirKategoriyeGoreKitaplar(int kategoriId)
            {
                var kitapListesi = new List<DTOKitap>();

                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                using (SqlCommand command = new SqlCommand("sp_GetirKategoriyeGoreKitaplar", baglanti))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@KategoriID", kategoriId);

                    baglanti.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kitapListesi.Add(new DTOKitap
                            {
                                KitapID = Convert.ToInt32(reader["KitapID"]),
                                KitapAdi = reader["KitapAdi"]?.ToString(),
                                YazarAdi = reader["YazarAdi"]?.ToString(),
                                YayinEvi = reader["YayinEvi"]?.ToString(),
                                StokAdedi = Convert.ToInt32(reader["StokAdedi"]),
                                RafNo = reader["RafNo"]?.ToString(),
                                KategoriAdi = reader["KategoriAdi"]?.ToString(),
                                KategoriID = Convert.ToInt32(reader["KategoriID"])
                            });
                        }
                    }
                }

                return kitapListesi;
            }


            // 8. KATEGORİ ARA
            public virtual List<DTOKategori> AraKategori(DTOKategori dTO)
            {
                List<DTOKategori> kategoriListesi = new List<DTOKategori>();

                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_AraKategori", baglanti);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AramaMetni", dTO.KategoriAdi);

                    baglanti.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DTOKategori kategori = new DTOKategori();
                        kategori.KategoriID = Convert.ToInt32(reader["KategoriID"]);
                        kategori.KategoriAdi = reader["KategoriAdi"].ToString();
                        kategori.Aciklama = reader["Aciklama"].ToString();
                        kategoriListesi.Add(kategori);

                    }
                }
                return kategoriListesi;
            }

        }
    }
}
