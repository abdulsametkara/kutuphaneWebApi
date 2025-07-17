using KutuphaneYonetimSistemi.DataAccess.Context;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using KutuphaneYonetimSistemi.DataAccess.DTOs;
using static KutuphaneYonetimSistemi.DataAccess.DTOs.DTOAuthentication;

namespace KutuphaneYonetimSistemi.DataAccess.Entities
{
    public class EAuthentication
    {
        private string HashPassword(string password)
        {
            string secretKey = "nPU.4I|ll@r${4g";
            using (var sha256 = SHA256.Create())
            {
                var combinedPassword = password + secretKey;
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedPassword));
                var hashedPassword = Convert.ToBase64String(bytes);
                return hashedPassword;
            }
        }

        public virtual LoginResponse Login(Login loginDto)
        {
            var response = new LoginResponse();
            string hashedPassword = HashPassword(loginDto.Password);

            try
            {
                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command = new SqlCommand("sp_KullaniciGirisi", baglanti);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@KullaniciAdi", loginDto.KullaniciAdi);
                    command.Parameters.AddWithValue("@Password", hashedPassword);

                    baglanti.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        response.BasariliMi = true;
                        response.Mesaj = "Login successful";
                        response.KullaniciID = Convert.ToInt32(reader["ID"]);
                        response.Ad = reader["Ad"].ToString();
                        response.Soyad = reader["Soyad"].ToString();
                        response.KullaniciTipi = reader["Tip"].ToString();

                        if (response.KullaniciTipi == "Personel")
                        {
                            response.Yetki = reader["Yetki"].ToString();
                        }
                    }
                    else
                    {
                        response.BasariliMi = false;
                        response.Mesaj = "Invalid username or password";
                    }
                }
            }
            catch (Exception ex)
            {
                response.BasariliMi = false;
                response.Mesaj = "Login error: " + ex.Message;
            }

            return response;
        }

        // CreateUser metodu
        public virtual CreateUserResponse CreateUser(CreateUser createUserDto)
        {
            var response = new CreateUserResponse();

            try
            {
                string hashedPassword = HashPassword(createUserDto.Password);

                using (SqlConnection baglanti = VeriTabaniBaglantisi.BaglantiOlustur())
                {
                    SqlCommand command;

                    if (createUserDto.KullaniciTipi == "Uye")
                    {
                        command = new SqlCommand("sp_CreateUye", baglanti);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TCKimlik", createUserDto.TCKimlikNo);
                        command.Parameters.AddWithValue("@Ad", createUserDto.Ad);
                        command.Parameters.AddWithValue("@Soyad", createUserDto.Soyad);
                        command.Parameters.AddWithValue("@Email", createUserDto.Email);
                        command.Parameters.AddWithValue("@KullaniciAdi", createUserDto.KullaniciAdi);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("@DogumTarihi", createUserDto.DogumTarihi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Telefon", createUserDto.Telefon ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Cinsiyet", createUserDto.Cinsiyet ?? (object)DBNull.Value);
                    }
                    else if (createUserDto.KullaniciTipi == "Personel")
                    {
                        command = new SqlCommand("sp_CreatePersonel", baglanti);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TCKimlikNo", createUserDto.TCKimlikNo);
                        command.Parameters.AddWithValue("@Ad", createUserDto.Ad);
                        command.Parameters.AddWithValue("@Soyad", createUserDto.Soyad);
                        command.Parameters.AddWithValue("@Email", createUserDto.Email);
                        command.Parameters.AddWithValue("@KullaniciAdi", createUserDto.KullaniciAdi);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("@Yetki", createUserDto.Yetki ?? "User");
                    }
                    else
                    {
                        response.BasariliMi = false;
                        response.Mesaj = "Invalid user type. Must be 'Uye' or 'Personel'";
                        return response;
                    }

                    baglanti.Open();
                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                        response.BasariliMi = true;
                        response.Mesaj = "User created successfully";
                        response.KullaniciID = Convert.ToInt32(result);
                        response.KullaniciTipi = createUserDto.KullaniciTipi;
                    }
                    else
                    {
                        response.BasariliMi = false;
                        response.Mesaj = "Failed to create user";
                    }
                }
            }
            catch (Exception ex)
            {
                response.BasariliMi = false;
                response.Mesaj = "Create user error: " + ex.Message;
            }

            return response;
        }
    }
}