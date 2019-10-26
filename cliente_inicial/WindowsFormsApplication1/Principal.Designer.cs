namespace WindowsFormsApplication1
{
    partial class Principal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.usuario = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.enviarbtn = new System.Windows.Forms.Button();
            this.resgistrarbtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.consulta1 = new System.Windows.Forms.RadioButton();
            this.consulta2 = new System.Windows.Forms.RadioButton();
            this.consulta3 = new System.Windows.Forms.RadioButton();
            this.enviar = new System.Windows.Forms.Button();
            this.desconectar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.carta4 = new System.Windows.Forms.PictureBox();
            this.carta3 = new System.Windows.Forms.PictureBox();
            this.carta2 = new System.Windows.Forms.PictureBox();
            this.carta1 = new System.Windows.Forms.PictureBox();
            this.lanzarbtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.carta4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carta3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carta2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carta1)).BeginInit();
            this.SuspendLayout();
            // 
            // usuario
            // 
            this.usuario.Location = new System.Drawing.Point(875, 56);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(169, 26);
            this.usuario.TabIndex = 1;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(875, 122);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(169, 26);
            this.password.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(871, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(871, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Contraseña";
            // 
            // enviarbtn
            // 
            this.enviarbtn.Location = new System.Drawing.Point(901, 167);
            this.enviarbtn.Name = "enviarbtn";
            this.enviarbtn.Size = new System.Drawing.Size(119, 37);
            this.enviarbtn.TabIndex = 5;
            this.enviarbtn.Text = "Iniciar sesión";
            this.enviarbtn.UseVisualStyleBackColor = true;
            this.enviarbtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // resgistrarbtn
            // 
            this.resgistrarbtn.Location = new System.Drawing.Point(901, 229);
            this.resgistrarbtn.Name = "resgistrarbtn";
            this.resgistrarbtn.Size = new System.Drawing.Size(119, 37);
            this.resgistrarbtn.TabIndex = 6;
            this.resgistrarbtn.Text = "Registrarse";
            this.resgistrarbtn.UseVisualStyleBackColor = true;
            this.resgistrarbtn.Click += new System.EventHandler(this.registrarbtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(835, 334);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(119, 26);
            this.textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(835, 390);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(119, 26);
            this.textBox2.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(831, 308);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Consultas";
            // 
            // consulta1
            // 
            this.consulta1.AutoSize = true;
            this.consulta1.Location = new System.Drawing.Point(972, 333);
            this.consulta1.Name = "consulta1";
            this.consulta1.Size = new System.Drawing.Size(110, 24);
            this.consulta1.TabIndex = 10;
            this.consulta1.TabStop = true;
            this.consulta1.Text = "Consulta 1";
            this.consulta1.UseVisualStyleBackColor = true;
            // 
            // consulta2
            // 
            this.consulta2.AutoSize = true;
            this.consulta2.Location = new System.Drawing.Point(972, 362);
            this.consulta2.Name = "consulta2";
            this.consulta2.Size = new System.Drawing.Size(110, 24);
            this.consulta2.TabIndex = 11;
            this.consulta2.TabStop = true;
            this.consulta2.Text = "Consulta 2";
            this.consulta2.UseVisualStyleBackColor = true;
            // 
            // consulta3
            // 
            this.consulta3.AutoSize = true;
            this.consulta3.Location = new System.Drawing.Point(972, 392);
            this.consulta3.Name = "consulta3";
            this.consulta3.Size = new System.Drawing.Size(110, 24);
            this.consulta3.TabIndex = 12;
            this.consulta3.TabStop = true;
            this.consulta3.Text = "Consulta 3";
            this.consulta3.UseVisualStyleBackColor = true;
            // 
            // enviar
            // 
            this.enviar.Location = new System.Drawing.Point(835, 437);
            this.enviar.Name = "enviar";
            this.enviar.Size = new System.Drawing.Size(119, 37);
            this.enviar.TabIndex = 13;
            this.enviar.Text = "Enviar";
            this.enviar.UseVisualStyleBackColor = true;
            this.enviar.Click += new System.EventHandler(this.enviar_Click);
            // 
            // desconectar
            // 
            this.desconectar.Location = new System.Drawing.Point(963, 482);
            this.desconectar.Name = "desconectar";
            this.desconectar.Size = new System.Drawing.Size(119, 66);
            this.desconectar.TabIndex = 14;
            this.desconectar.Text = "Desconectar";
            this.desconectar.UseVisualStyleBackColor = true;
            this.desconectar.Click += new System.EventHandler(this.desconectar_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.WhatsApp_Image_2019_10_21_at_17_38_29;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.carta4);
            this.panel1.Controls.Add(this.carta3);
            this.panel1.Controls.Add(this.carta2);
            this.panel1.Controls.Add(this.carta1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(811, 536);
            this.panel1.TabIndex = 0;
            // 
            // carta4
            // 
            this.carta4.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources._72f58f9446cc33e8ed1bf8ef7316636c;
            this.carta4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.carta4.Location = new System.Drawing.Point(405, 379);
            this.carta4.Name = "carta4";
            this.carta4.Size = new System.Drawing.Size(90, 132);
            this.carta4.TabIndex = 3;
            this.carta4.TabStop = false;
            this.carta4.Click += new System.EventHandler(this.SeleccionarCarta);
            // 
            // carta3
            // 
            this.carta3.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.d7c751beea214fda50b763270220deb2;
            this.carta3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.carta3.Location = new System.Drawing.Point(296, 380);
            this.carta3.Name = "carta3";
            this.carta3.Size = new System.Drawing.Size(90, 132);
            this.carta3.TabIndex = 2;
            this.carta3.TabStop = false;
            this.carta3.Click += new System.EventHandler(this.SeleccionarCarta);
            // 
            // carta2
            // 
            this.carta2.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.WhatsApp_Image_2019_10_21_at_17_49_50;
            this.carta2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.carta2.Location = new System.Drawing.Point(405, 21);
            this.carta2.Name = "carta2";
            this.carta2.Size = new System.Drawing.Size(90, 132);
            this.carta2.TabIndex = 1;
            this.carta2.TabStop = false;
            // 
            // carta1
            // 
            this.carta1.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.WhatsApp_Image_2019_10_21_at_17_49_50;
            this.carta1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.carta1.Location = new System.Drawing.Point(296, 21);
            this.carta1.Name = "carta1";
            this.carta1.Size = new System.Drawing.Size(90, 132);
            this.carta1.TabIndex = 0;
            this.carta1.TabStop = false;
            // 
            // lanzarbtn
            // 
            this.lanzarbtn.Location = new System.Drawing.Point(279, 556);
            this.lanzarbtn.Name = "lanzarbtn";
            this.lanzarbtn.Size = new System.Drawing.Size(119, 37);
            this.lanzarbtn.TabIndex = 15;
            this.lanzarbtn.Text = "Lanzar carta";
            this.lanzarbtn.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(417, 556);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 37);
            this.button1.TabIndex = 16;
            this.button1.Text = "Siguiente turno";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1095, 605);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lanzarbtn);
            this.Controls.Add(this.desconectar);
            this.Controls.Add(this.enviar);
            this.Controls.Add(this.consulta3);
            this.Controls.Add(this.consulta2);
            this.Controls.Add(this.consulta1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.resgistrarbtn);
            this.Controls.Add(this.enviarbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.panel1);
            this.Name = "Principal";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.carta4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carta3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carta2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carta1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox usuario;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button enviarbtn;
        private System.Windows.Forms.Button resgistrarbtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton consulta1;
        private System.Windows.Forms.RadioButton consulta2;
        private System.Windows.Forms.RadioButton consulta3;
        private System.Windows.Forms.Button enviar;
        private System.Windows.Forms.Button desconectar;
        private System.Windows.Forms.PictureBox carta4;
        private System.Windows.Forms.PictureBox carta3;
        private System.Windows.Forms.PictureBox carta2;
        private System.Windows.Forms.PictureBox carta1;
        private System.Windows.Forms.Button lanzarbtn;
        private System.Windows.Forms.Button button1;
    }
}