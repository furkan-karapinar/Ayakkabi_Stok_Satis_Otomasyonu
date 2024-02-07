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
    public partial class Form1 : Form
    {
        List<Button> buttonList;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            buttonList = new List<Button>() { stok_btn,kasa_btn,satis_btn,cikis_btn };

            change_panel(new Satis_UC());
            selected_button(kasa_btn);
        }

        private void change_panel(Control control)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(control);
        }

        private void selected_button(Button button)
        {
            foreach (Button btn in buttonList)
            {
                btn.BackColor = Color.FromKnownColor(KnownColor.Control);
                btn.ForeColor = Color.Black;
            }
            button.BackColor = Color.FromKnownColor(KnownColor.HotTrack);
            button.ForeColor = Color.White;
        }

        private void stok_btn_Click(object sender, EventArgs e)
        {
            change_panel(new Stok_Yönetimi_UC());
            selected_button((sender as Button));
        }

        private void kasa_btn_Click(object sender, EventArgs e)
        {
            change_panel(new Satis_UC());
            selected_button((sender as Button));
        }

        private void cikis_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void satis_btn_Click(object sender, EventArgs e)
        {
            change_panel(new Satis_Gecmisi_UC());
            selected_button((sender as Button));
        }
    }
}
