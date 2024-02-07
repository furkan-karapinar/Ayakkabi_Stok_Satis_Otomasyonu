using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ayakkabi_Stok_Satis_Otomasyonu
{
    internal class Database_Control
    {
        // Veritabanı adı ve konumu
        string path = "database.db", cs = @"URI=file:" + Application.StartupPath + "\\database.db";

        // Gerekli tanımlamalar
        SQLiteConnection data_connection;
        SQLiteCommand command;
        SQLiteDataReader reader;



        public void Create_Database(String datatable_name, String data_options)
        {
            // Veritablosu yoksa oluşturulur. Varsa oluşturmaz. Hata durumunda kullanıcıya belirtilir.
            try
            {
                // Veritabanı var mı sorgulama
                if (!System.IO.File.Exists(path))
                {
                    // if sorgusunda '!' işareti mevcut olduğundan veritabanı yoktur. Bu yüzden veritabanı dosyası oluşturulur.
                    SQLiteConnection.CreateFile(path);
                }

                // İstenilen veritablosu yoksa oluşturulur.
                using (var sqlite = new SQLiteConnection(@"Data Source=" + path))
                {
                    sqlite.Open();
                    string sql = "CREATE TABLE IF NOT EXISTS " + datatable_name + " (" + data_options + ")";
                    SQLiteCommand cmd = new SQLiteCommand(sql, sqlite);
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }
            catch { MessageBox.Show("Veritabanı Oluşturma Hatası"); }

        }

        public void Delete_Data(String datatable_name, String database_item_name, String item_name)
        {
            // Burası (Genel) veritablosundan veri silme yeridir. Hata durumunda kullanıcıya belirtilir.
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "DELETE FROM " + datatable_name + " WHERE " + database_item_name + "=@name";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@name", item_name);
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
            catch { MessageBox.Show("Veri Silme Hatası"); }
        }

        public bool Insert_Data(String datatable_name, String item_names, Object item_values, Object picture)
        {
            // Burası veritablosuna birden fazla veri işlemek içindir.
            // Hata durumunda ayrıca belirtilir.
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "INSERT INTO " + datatable_name + "(" + item_names + ") VALUES(" + item_values + ", @imageData)";
                cmd.Parameters.AddWithValue("@imageData", picture);
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
            catch { return false; }

            return true;
        }


        public void Update_Data(String datatable_name, String where_column_name, String where_column_value, String item_name, Object item_value)
        {
            // Burası ayarların verilerini güncellemek içindir. Hata durumunda belirtilir.
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "UPDATE " + datatable_name + " SET " + item_name + "=@value WHERE " + where_column_name + "=@name";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@name", where_column_value);
                cmd.Parameters.AddWithValue("@value", item_value);
                cmd.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Veri Değiştirme Hatası"); }


        }

        public List<object> GetStockInfoByBarcode(string barcode)
        {
            List<object> stockInfo = new List<object>();

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(cs))
                {
                    con.Open();
                    string query = "SELECT barkod,urun_adi, cinsiyet, kategori, renk, beden, gelis_tarihi, alis_fiyati, satis_fiyati, stok_adet, detay, gorsel FROM stoklar WHERE barkod = @barcode";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@barcode", barcode);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                stockInfo.Add(reader["barkod"]);
                                stockInfo.Add(reader["urun_adi"]);
                                stockInfo.Add(reader["cinsiyet"]);
                                stockInfo.Add(reader["kategori"]);
                                stockInfo.Add(reader["renk"]);
                                stockInfo.Add(reader["beden"]);
                                stockInfo.Add(reader["gelis_tarihi"]);
                                stockInfo.Add(reader["alis_fiyati"]);
                                stockInfo.Add(reader["satis_fiyati"]);
                                stockInfo.Add(reader["stok_adet"]);
                                stockInfo.Add(reader["detay"]);
                                stockInfo.Add(reader["gorsel"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }

            return stockInfo;
        }

        public DataTable Get_All_Stock_Data()
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(cs))
                {
                    con.Open();
                    string query = "SELECT * FROM stoklar"; // stoklar tablosundaki tüm sütunları al

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        return dt; // DataGridView'e verileri ata
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
            return new DataTable();
        }

        public DataTable GetProductInfoByBarcode(string barcode,int miktar)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(cs))
                {
                    con.Open();
                    string query = "SELECT barkod, urun_adi,cinsiyet, beden, renk, satis_fiyati , stok_adet FROM stoklar WHERE barkod = @barcode";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@barcode", barcode);

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            
                            adapter.Fill(dt); // Verileri DataTable'a doldur
                            DataColumn newColumn = new DataColumn("miktar", typeof(int));
                            newColumn.DefaultValue = miktar;
                            dt.Columns.Add(newColumn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }

            return dt;
        }

        public void AddDataTableToDatabase(DataTable dt,string bugun)
    {
        try
        {
            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        // Örnek olarak sütun isimlerini ve değerleri ilgili şekilde düzenleyin
                        cmd.CommandText = "INSERT INTO gecmis_satislar (barkod,urun_adi,cinsiyet,beden,renk,satis_fiyati,miktar,satilma_tarihi) " +
                                          "VALUES (@barkod, @urunAdi, @cinsiyet, @beden,@renk,@satis_fiyati , @miktar ,'" + bugun + "')";

                            // Örnek olarak parametrelerin değerlerini ve tiplerini ilgili şekilde ayarlayın
                        cmd.Parameters.AddWithValue("@barkod", row["barkod"]);
                        cmd.Parameters.AddWithValue("@urunAdi", row["urun_adi"]);
                        cmd.Parameters.AddWithValue("@cinsiyet", row["cinsiyet"]);
                        cmd.Parameters.AddWithValue("@beden", row["beden"]);
                        cmd.Parameters.AddWithValue("@renk", row["renk"]);
                        cmd.Parameters.AddWithValue("@satis_fiyati", row["satis_fiyati"]);
                        cmd.Parameters.AddWithValue("@miktar", row["miktar"]);

                        // Diğer sütunlar için benzer şekilde devam edin...

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
        }
    }

        public void ShowSalesByDate(DataGridView dataGridView, String satilmaTarihi)
        {

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(cs))
                {
                    con.Open();
                    string query = "SELECT * FROM gecmis_satislar WHERE satilma_tarihi = @satilmaTarihi";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@satilmaTarihi", satilmaTarihi);

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dataGridView.DataSource = dt; // DataGridView'e verileri ata
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }

        public int GetStockAmountByBarcode(string barcode)
        {
            int stokAdet = 0; // Varsayılan olarak -1, bulunamadığı durumu belirtmek için

            try
            {
               
                using (SQLiteConnection con = new SQLiteConnection(cs))
                {
                    con.Open();
                    string query = "SELECT stok_adet FROM stoklar WHERE barkod = @barcode";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@barcode", barcode);
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            stokAdet = Convert.ToInt32(result); // stok_adet değerini al
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }

            return stokAdet;
        }

    }
}
