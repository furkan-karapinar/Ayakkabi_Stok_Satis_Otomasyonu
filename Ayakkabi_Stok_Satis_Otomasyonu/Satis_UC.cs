using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ayakkabi_Stok_Satis_Otomasyonu
{
    public partial class Satis_UC : UserControl
    {
        public Satis_UC()
        {
            InitializeComponent();
        }

        Database_Control dc = new Database_Control();
        DataTable dt = new DataTable();

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece sayılar, geri silme (backspace) ve nokta (.) izin verilir
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true; // Bu karakteri işleme alma
            }

            // Sadece bir nokta olabilir ve birden fazla noktaya izin verme
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true; // Birden fazla noktaya izin verme
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty || textBox1.Text == "0") { textBox1.Text = "1"; }
            dt.Merge(dc.GetProductInfoByBarcode(textBox2.Text,Convert.ToInt32(textBox1.Text)));
            dataGridView1.DataSource = dt;
           textBox4.Text =Convert.ToString(CalculateTotalSales(dataGridView1));
            if (dt.Rows.Count > 0) {button5.Enabled = true; }

            dataGridView1.Columns["barkod"].HeaderText = "Barkod";
            dataGridView1.Columns["beden"].HeaderText = "Beden";
            dataGridView1.Columns["renk"].HeaderText = "Renk";
            dataGridView1.Columns["cinsiyet"].HeaderText = "Cinsiyet";
            dataGridView1.Columns["urun_adi"].HeaderText = "Ürün Adı";
            dataGridView1.Columns["satis_fiyati"].HeaderText = "Satış Fiyatı";
            dataGridView1.Columns["stok_adet"].HeaderText = "Stok Adedi";
            dataGridView1.Columns["miktar"].HeaderText = "Miktar";

        }

        public decimal CalculateTotalSales(DataGridView dataGridView)
        {
            decimal totalSales = 0;

            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    decimal satisFiyati = Convert.ToDecimal(row.Cells["satis_fiyati"].Value);
                    int miktar = Convert.ToInt32(row.Cells["miktar"].Value);

                    totalSales += satisFiyati * miktar;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }

            return totalSales;
        }

        public bool Stoktan_dus(DataGridView dataGridView)
        {
            decimal totalSales = 0;

            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    int barkod = Convert.ToInt32(row.Cells["barkod"].Value);
                    string urun_adi = Convert.ToString(row.Cells["urun_adi"].Value);
                    decimal satisFiyati = Convert.ToDecimal(row.Cells["satis_fiyati"].Value);
                    int miktar = Convert.ToInt32(row.Cells["miktar"].Value);

                    int mevcut_stok = dc.GetStockAmountByBarcode(barkod.ToString());
                    int result_1 = mevcut_stok - miktar;
                    if (result_1 <= 0) 
                    {
                        return false;
                    } else if (result_1 > 0)
                    {
                        dc.Update_Data("stoklar", "barkod", barkod.ToString(), "stok_adet", result_1.ToString());
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }

           return false;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                    dataGridView1.DataSource = dt;
                } else { button5.Enabled = false; }
                if (dt == null || dt.Rows.Count <= 0) {button5.Enabled = false; }
            } catch (NullReferenceException nrex) { } catch (Exception ex) { }
          textBox4.Text = CalculateTotalSales(dataGridView1).ToString();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            dataGridView1.DataSource = null;
            dt.Clear();
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            button5.Enabled = false;
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox3.Text == String.Empty) { textBox3.Text = "0"; }
            decimal result = Convert.ToDecimal(textBox3.Text) - Convert.ToDecimal(textBox4.Text);
            textBox5.Text = result.ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == String.Empty) { textBox3.Text = "0"; }
            decimal result = Convert.ToDecimal(textBox3.Text) - Convert.ToDecimal(textBox4.Text);
            textBox5.Text = result.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(textBox3.Text) >= Convert.ToDecimal(textBox4.Text))
            {
                if (Stoktan_dus(dataGridView1) == false) { MessageBox.Show("Ürünlerin Stok Durumunu Kontrol Edin","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Error); }
                else
                {
DateTime bugun = DateTime.Now;
            string bugununTarihi = bugun.ToString("dd MMMM yyyy");
            dc.AddDataTableToDatabase(dt,bugununTarihi);
                
            textBox2.Clear();
            dataGridView1.DataSource = null;
            dt.Clear();
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            button5.Enabled = false;
                    MessageBox.Show("Satış Başarılı" , "Bilgilendirme" , MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            
            } else { MessageBox.Show("Müşteri Ödenecek Ücretin Tamamını Ödemelidir", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error); }


        }


    }
}
