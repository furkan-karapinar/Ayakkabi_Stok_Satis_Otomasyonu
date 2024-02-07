namespace Ayakkabi_Stok_Satis_Otomasyonu
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.cikis_btn = new System.Windows.Forms.Button();
            this.satis_btn = new System.Windows.Forms.Button();
            this.kasa_btn = new System.Windows.Forms.Button();
            this.stok_btn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cikis_btn);
            this.panel1.Controls.Add(this.satis_btn);
            this.panel1.Controls.Add(this.kasa_btn);
            this.panel1.Controls.Add(this.stok_btn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(192, 543);
            this.panel1.TabIndex = 0;
            // 
            // cikis_btn
            // 
            this.cikis_btn.BackColor = System.Drawing.SystemColors.Control;
            this.cikis_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.cikis_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cikis_btn.Location = new System.Drawing.Point(5, 164);
            this.cikis_btn.Margin = new System.Windows.Forms.Padding(10);
            this.cikis_btn.Name = "cikis_btn";
            this.cikis_btn.Size = new System.Drawing.Size(182, 53);
            this.cikis_btn.TabIndex = 1;
            this.cikis_btn.Text = "Çıkış";
            this.cikis_btn.UseVisualStyleBackColor = false;
            this.cikis_btn.Click += new System.EventHandler(this.cikis_btn_Click);
            // 
            // satis_btn
            // 
            this.satis_btn.BackColor = System.Drawing.SystemColors.Control;
            this.satis_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.satis_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.satis_btn.Location = new System.Drawing.Point(5, 111);
            this.satis_btn.Margin = new System.Windows.Forms.Padding(10);
            this.satis_btn.Name = "satis_btn";
            this.satis_btn.Size = new System.Drawing.Size(182, 53);
            this.satis_btn.TabIndex = 1;
            this.satis_btn.Text = "Satış Geçmişi";
            this.satis_btn.UseVisualStyleBackColor = false;
            this.satis_btn.Click += new System.EventHandler(this.satis_btn_Click);
            // 
            // kasa_btn
            // 
            this.kasa_btn.BackColor = System.Drawing.SystemColors.Control;
            this.kasa_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.kasa_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.kasa_btn.Location = new System.Drawing.Point(5, 58);
            this.kasa_btn.Margin = new System.Windows.Forms.Padding(10);
            this.kasa_btn.Name = "kasa_btn";
            this.kasa_btn.Size = new System.Drawing.Size(182, 53);
            this.kasa_btn.TabIndex = 1;
            this.kasa_btn.Text = "Kasa Yönetimi";
            this.kasa_btn.UseVisualStyleBackColor = false;
            this.kasa_btn.Click += new System.EventHandler(this.kasa_btn_Click);
            // 
            // stok_btn
            // 
            this.stok_btn.BackColor = System.Drawing.SystemColors.Control;
            this.stok_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.stok_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.stok_btn.Location = new System.Drawing.Point(5, 5);
            this.stok_btn.Margin = new System.Windows.Forms.Padding(10);
            this.stok_btn.Name = "stok_btn";
            this.stok_btn.Size = new System.Drawing.Size(182, 53);
            this.stok_btn.TabIndex = 1;
            this.stok_btn.Text = "Stok Yönetimi";
            this.stok_btn.UseVisualStyleBackColor = false;
            this.stok_btn.Click += new System.EventHandler(this.stok_btn_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(192, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(1070, 543);
            this.panel2.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1262, 543);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Ayakkabı Stok Satış Otomasyonu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cikis_btn;
        private System.Windows.Forms.Button stok_btn;
        private System.Windows.Forms.Button satis_btn;
        private System.Windows.Forms.Button kasa_btn;
        private System.Windows.Forms.Panel panel2;
    }
}

