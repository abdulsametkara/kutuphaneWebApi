using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KutuphaneYonetimSistemi.DataAccess.Context
{
    public class VeriTabaniBaglantisi
    {
        public static SqlConnection BaglantiOlustur()
        {
            string connectionString = @"Server=DESKTOP-3IA74T6\SQLEXPRESS;Database=KutuphaneDB;Integrated Security=True;TrustServerCertificate=True;";
            return new SqlConnection(connectionString);
        }
    }
}