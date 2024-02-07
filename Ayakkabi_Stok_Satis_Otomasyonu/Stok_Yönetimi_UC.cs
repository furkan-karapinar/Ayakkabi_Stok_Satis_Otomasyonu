using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ayakkabi_Stok_Satis_Otomasyonu
{
    public partial class Stok_Yönetimi_UC : UserControl
    {
        Database_Control dc = new Database_Control();
        String dc_items = "barkod,urun_adi,cinsiyet,kategori,renk,beden,gelis_tarihi,alis_fiyati,satis_fiyati,stok_adet,detay,gorsel";
        
        public Stok_Yönetimi_UC()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) // Ürün Kayıt Kodları
        {
            if (empty_control())
            {
                MessageBox.Show("Lütfen Boş Alanları Doldurunuz" ,"Bildiri",MessageBoxButtons.OK,MessageBoxIcon.Error);
            } 
            else
            {
                Object values = "'" + textBox1.Text + "' , '" + textBox5.Text + "' , '" + comboBox1.Text + "' , '" +  comboBox2.Text + "' , '" + comboBox3.Text + "' , '" + comboBox4.Text + "' , '" + dateTimePicker1.Text + "' , '" + textBox2.Text + "' , '" + textBox3.Text + "' , '" + textBox4.Text + "' , '" + richTextBox1.Text + "'";
               bool result = dc.Insert_Data("stoklar", dc_items, values, picture_to_byte());
                if (result)
                {
                    MessageBox.Show("Ürün Kaydedildi","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
                } else { MessageBox.Show("Ürün Zaten Mevcut", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error); }
               
            }
        }

        private object picture_to_byte() // Pictureboxtaki Image'ı Byte Dizisine Çevirir (Veritabanında BLOB Olarak Saklamak İçin)
        {
            try
            {
                byte[] imageBytes;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                  pictureBox1.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png); // Resmi PNG formatında kaydetmek için
                  imageBytes = memoryStream.ToArray();
                }
                return imageBytes;
         
            } catch (Exception ex) {return "NULL"; }
            
            
        }

        public Image ConvertObjectToImage(object data) // Veritabanındaki Görseli Geri Image'a Çevirir
        {
            Image image = null;

            try
            {
                if (data is byte[])
                {
                    byte[] byteArray = (byte[])data;
                    using (MemoryStream memoryStream = new MemoryStream(byteArray))
                    {
                        image = Image.FromStream(memoryStream);
                    }
                }
                else
                {
                    Console.WriteLine("Hata: Veri byte[] türünde değil.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }

            return image;
        }
        private bool empty_control() // Girdilerin Boş Olup Olmadığını Kontrol Eder
        {
            bool result = false;
            String error_text = "Bu alan boş bırakılamaz";
            List<Control> not_null_controls = new List<Control>() { textBox1,textBox5,comboBox1,comboBox2,comboBox3,comboBox4,textBox2,textBox3,textBox4 };
            errorProvider1.Clear();

            foreach (Control control in not_null_controls)
            {
                if (control.Text == String.Empty)
                {
                    errorProvider1.SetError(control, error_text);
                    result = true;
                }
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Ürün Görselini Seçin";
            openFileDialog1.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               if (openFileDialog1.FileName != null)
                {
                    pictureBox1.ImageLocation = openFileDialog1.FileName;
                }
                
            }
        } // Görsel Seçme

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           List<int> erkek_ayakkabi_no = new List<int>() { 40,41,42,43,44,45 };
            List<int> kadin_ayakkabi_no = new List<int>() { 35,36,37,38,39,40 };
            List<int> selected_list = new List<int>();

            comboBox4.Items.Clear();
            comboBox4.ResetText();

            if (comboBox1.SelectedIndex == 0)
            {
                selected_list = erkek_ayakkabi_no;
            }
            else { selected_list = kadin_ayakkabi_no; }

            foreach (int i in selected_list)
            {
                comboBox4.Items.Add(i);
            }
            
        } // Erkek Seçilirse 40-45 Arası , Kadın Seçilirse 35-40 Arası

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        } // Bazı TextBoxlar İçin Sadece Sayı Girilmesini Sağlayan Kodları İçerir

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) // Fiyatlar İçin Sayıların Yanına Ek Olarak "." Desteği Ekli
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

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != '_')
            {
                e.Handled = true; // Bu karakteri işleme alma
            }
        } // Sql'e İşlerken Sıkıntı Oluşturma İhtimaline Karşı Özel Karakter Engelleyen Kodları İçerir

        private void clear_ui() // Girdileri Temizler
        {
            List<TextBox> textBoxes = new List<TextBox>() { textBox2, textBox3, textBox4 ,textBox5};
            List<ComboBox> comboBoxes = new List<ComboBox>() { comboBox1, comboBox2, comboBox3, comboBox4 };
            foreach (TextBox textBox in textBoxes) {textBox.Clear();}
            foreach (ComboBox comboBox in comboBoxes) {comboBox.SelectedIndex = 0;}
            dateTimePicker1.ResetText();
            richTextBox1.ResetText();
            pictureBox1.ImageLocation = null;
            pictureBox1.Image = null;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e) // Barkod girilir girilmez bilgileri ekrana yazdırır buradaki kodlar
        {
            pictureBox1.ImageLocation = null;
            List<Control> controls = new List<Control>() { textBox1,textBox5, comboBox1, comboBox2, comboBox3, comboBox4,dateTimePicker1, textBox2, textBox3, textBox4,richTextBox1 };

            clear_ui();
            List<object> list = dc.GetStockInfoByBarcode((sender as TextBox).Text);
            try
            {
                int i=0;
                foreach (Control control in controls)
                {
                    control.Text = Convert.ToString(list[i]);
                    i++;
                }
                pictureBox1.Image = ConvertObjectToImage(list[11]);
            } catch (OverflowException oex) {  } catch (Exception ex) { }
         

        }

        private void button4_Click(object sender, EventArgs e) // Ürün Düzenleme Kodları
        {
            

          DialogResult result =  MessageBox.Show("Düzenlemek İstediğinizden Emin Misiniz?", "Bilgilendirme", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
           if (result == DialogResult.Yes)
            {
                try
            {
            if (empty_control())
            {
                MessageBox.Show("Lütfen Boş Alanları Doldurunuz", "Bildiri", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<string> update_items = new List<string>() { "urun_adi","cinsiyet", "kategori", "renk","beden", "gelis_tarihi", "alis_fiyati", "satis_fiyati", "stok_adet", "detay"};
                List<Control> controls = new List<Control>() { textBox5,comboBox1, comboBox2, comboBox3, comboBox4, dateTimePicker1, textBox2, textBox3, textBox4, richTextBox1 };
                int i=0;
                foreach (Control control in controls)
                {
                    dc.Update_Data("stoklar", "barkod", textBox1.Text, update_items[i], control.Text);
                    i++;
                }
                    dc.Update_Data("stoklar", "barkod", textBox1.Text, "gorsel", picture_to_byte());

                    MessageBox.Show("Ürün Düzenlendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            }  catch (OverflowException oex) { MessageBox.Show("Hata Oluştu"); } catch (Exception ex) { MessageBox.Show("Hata Oluştu"); }
            }
        }

        private void button3_Click(object sender, EventArgs e) // Ürün Silme Kodları
        {
            DialogResult result = MessageBox.Show("Silmek İstediğinizden Emin Misiniz?", "Bilgilendirme", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (textBox1.Text != String.Empty || textBox1.Text != " ")
                    {
                        dc.Delete_Data("stoklar", "barkod", textBox1.Text);
                        MessageBox.Show("Ürün Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (OverflowException oex) { MessageBox.Show("Hata Oluştu"); }
                catch (Exception ex) { MessageBox.Show("Hata Oluştu"); }
            }
        }

        private void button5_Click(object sender, EventArgs e) // Ürün Listesi Açma Kodları
        {
            Urun_Listesi_Form u_frm = new Urun_Listesi_Form();
            u_frm.ShowDialog();
        }
    }
}
