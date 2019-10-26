namespace WindowsFormsApplication1
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.registrarbtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.usuario = new System.Windows.Forms.TextBox();
            this.enviarbtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.registrarbtn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.password);
            this.groupBox1.Controls.Add(this.usuario);
            this.groupBox1.Controls.Add(this.enviarbtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 282);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peticion";
            // 
            // registrarbtn
            // 
            this.registrarbtn.Location = new System.Drawing.Point(130, 186);
            this.registrarbtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.registrarbtn.Name = "registrarbtn";
            this.registrarbtn.Size = new System.Drawing.Size(75, 23);
            this.registrarbtn.TabIndex = 14;
            this.registrarbtn.Text = "Registrarse";
            this.registrarbtn.UseVisualStyleBackColor = true;
            this.registrarbtn.Click += new System.EventHandler(this.registrarbtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 99);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Password";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(117, 96);
            this.password.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(89, 20);
            this.password.TabIndex = 11;
            // 
            // usuario
            // 
            this.usuario.Location = new System.Drawing.Point(117, 61);
            this.usuario.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(89, 20);
            this.usuario.TabIndex = 10;
            // 
            // enviarbtn
            // 
            this.enviarbtn.Location = new System.Drawing.Point(130, 144);
            this.enviarbtn.Name = "enviarbtn";
            this.enviarbtn.Size = new System.Drawing.Size(75, 23);
            this.enviarbtn.TabIndex = 5;
            this.enviarbtn.Text = "Log in";
            this.enviarbtn.UseVisualStyleBackColor = true;
            this.enviarbtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 401);
            this.Controls.Add(this.groupBox1);
            this.Name = "Inicio";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox usuario;
        private System.Windows.Forms.Button enviarbtn;
        private System.Windows.Forms.Button registrarbtn;
    }
}

