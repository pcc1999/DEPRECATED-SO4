namespace WindowsFormsApplication1
{
    partial class VerConectados
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
            this.tablaUsuarios = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tablaUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // tablaUsuarios
            // 
            this.tablaUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaUsuarios.Location = new System.Drawing.Point(155, 63);
            this.tablaUsuarios.Name = "tablaUsuarios";
            this.tablaUsuarios.RowTemplate.Height = 28;
            this.tablaUsuarios.Size = new System.Drawing.Size(240, 400);
            this.tablaUsuarios.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 524);
            this.Controls.Add(this.tablaUsuarios);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablaUsuarios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tablaUsuarios;
    }
}