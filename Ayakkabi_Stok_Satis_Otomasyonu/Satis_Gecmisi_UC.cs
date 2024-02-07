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
    public partial class Satis_Gecmisi_UC : UserControl
    {
        public Satis_Gecmisi_UC()
        {
            InitializeComponent();
        }

        Database_Control dc = new Database_Control();
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime secilenTarih = dateTimePicker1.Value;
            string formatliTarih = secilenTarih.ToString("dd MMMM yyyy"); // Formatlı tarih

            dc.ShowSalesByDate(dataGridView1,formatliTarih);
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["barkod"].HeaderText = "Barkod";
            dataGridView1.Columns["beden"].HeaderText = "Beden";
            dataGridView1.Columns["renk"].HeaderText = "Renk";
            dataGridView1.Columns["cinsiyet"].HeaderText = "Cinsiyet";
            dataGridView1.Columns["urun_adi"].HeaderText = "Ürün Adı";
            dataGridView1.Columns["satis_fiyati"].HeaderText = "Satış Fiyatı";
            dataGridView1.Columns["miktar"].HeaderText = "Miktar";
            dataGridView1.Columns["satilma_tarihi"].HeaderText = "Satıldığı Tarih";
        }
    }
}
