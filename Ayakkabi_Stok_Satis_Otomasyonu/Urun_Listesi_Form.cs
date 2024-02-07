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
    public partial class Urun_Listesi_Form : Form
    {
        public Urun_Listesi_Form()
        {
            InitializeComponent();
        }
        Database_Control dc = new Database_Control();

        private void Urun_Listesi_Form_Load(object sender, EventArgs e)
        {
           dataGridView1.DataSource = dc.Get_All_Stock_Data();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["gorsel"].Visible = false;
            dataGridView1.Columns["barkod"].HeaderText = "Barkod";
            dataGridView1.Columns["beden"].HeaderText = "Beden";
            dataGridView1.Columns["renk"].HeaderText = "Renk";
            dataGridView1.Columns["cinsiyet"].HeaderText = "Cinsiyet";
            dataGridView1.Columns["urun_adi"].HeaderText = "Ürün Adı";
            dataGridView1.Columns["satis_fiyati"].HeaderText = "Satış Fiyatı";
            dataGridView1.Columns["kategori"].HeaderText = "Kategori";
            dataGridView1.Columns["gelis_tarihi"].HeaderText = "Geliş Tarihi";
            dataGridView1.Columns["alis_fiyati"].HeaderText = "Alış Fiyatı";
            dataGridView1.Columns["stok_adet"].HeaderText = "Stok Adedi";
            dataGridView1.Columns["detay"].HeaderText = "Detay";
        }
    }
}
