namespace WindowsFormsApplication1
{
    partial class Consulta
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.desconectar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.consulta1 = new System.Windows.Forms.RadioButton();
            this.consulta3 = new System.Windows.Forms.RadioButton();
            this.consulta2 = new System.Windows.Forms.RadioButton();
            this.enviar = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(149, 32);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 3;
            // 
            // desconectar
            // 
            this.desconectar.Location = new System.Drawing.Point(321, 84);
            this.desconectar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.desconectar.Name = "desconectar";
            this.desconectar.Size = new System.Drawing.Size(77, 27);
            this.desconectar.TabIndex = 4;
            this.desconectar.Text = "Desconectar";
            this.desconectar.UseVisualStyleBackColor = true;
            this.desconectar.Click += new System.EventHandler(this.desconectar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Texto consulta";
            // 
            // consulta1
            // 
            this.consulta1.AutoSize = true;
            this.consulta1.Location = new System.Drawing.Point(66, 70);
            this.consulta1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.consulta1.Name = "consulta1";
            this.consulta1.Size = new System.Drawing.Size(124, 17);
            this.consulta1.TabIndex = 7;
            this.consulta1.TabStop = true;
            this.consulta1.Text = "Click para consulta 1";
            this.consulta1.UseVisualStyleBackColor = true;
            // 
            // consulta3
            // 
            this.consulta3.AutoSize = true;
            this.consulta3.Location = new System.Drawing.Point(66, 109);
            this.consulta3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.consulta3.Name = "consulta3";
            this.consulta3.Size = new System.Drawing.Size(124, 17);
            this.consulta3.TabIndex = 8;
            this.consulta3.TabStop = true;
            this.consulta3.Text = "Click para consulta 3";
            this.consulta3.UseVisualStyleBackColor = true;
            // 
            // consulta2
            // 
            this.consulta2.AutoSize = true;
            this.consulta2.Location = new System.Drawing.Point(66, 90);
            this.consulta2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.consulta2.Name = "consulta2";
            this.consulta2.Size = new System.Drawing.Size(124, 17);
            this.consulta2.TabIndex = 9;
            this.consulta2.TabStop = true;
            this.consulta2.Text = "Click para consulta 2";
            this.consulta2.UseVisualStyleBackColor = true;
            // 
            // enviar
            // 
            this.enviar.Location = new System.Drawing.Point(216, 84);
            this.enviar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.enviar.Name = "enviar";
            this.enviar.Size = new System.Drawing.Size(77, 27);
            this.enviar.TabIndex = 10;
            this.enviar.Text = "Enviar";
            this.enviar.UseVisualStyleBackColor = true;
            this.enviar.Click += new System.EventHandler(this.enviar_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(278, 32);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(121, 20);
            this.textBox2.TabIndex = 11;
            // 
            // Consulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 230);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.enviar);
            this.Controls.Add(this.consulta2);
            this.Controls.Add(this.consulta3);
            this.Controls.Add(this.consulta1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.desconectar);
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Consulta";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Consulta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox consulta;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button desconectar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton consulta1;
        private System.Windows.Forms.RadioButton consulta3;
        private System.Windows.Forms.RadioButton consulta2;
        private System.Windows.Forms.Button enviar;
        private System.Windows.Forms.TextBox textBox2;
    }
}