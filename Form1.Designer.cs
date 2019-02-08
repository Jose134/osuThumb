namespace osuThumb
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.preview = new System.Windows.Forms.Panel();
            this.generateButton = new System.Windows.Forms.Button();
            this.idBox = new System.Windows.Forms.TextBox();
            this.idLabel = new System.Windows.Forms.Label();
            this.accLabel = new System.Windows.Forms.Label();
            this.accBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // preview
            // 
            this.preview.Location = new System.Drawing.Point(12, 12);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(480, 360);
            this.preview.TabIndex = 0;
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(498, 349);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(290, 23);
            this.generateButton.TabIndex = 1;
            this.generateButton.Text = "Generate!";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // idBox
            // 
            this.idBox.Location = new System.Drawing.Point(578, 12);
            this.idBox.Name = "idBox";
            this.idBox.Size = new System.Drawing.Size(210, 20);
            this.idBox.TabIndex = 2;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(498, 15);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(74, 13);
            this.idLabel.TabIndex = 3;
            this.idLabel.Text = "Beatmapset id";
            // 
            // accLabel
            // 
            this.accLabel.AutoSize = true;
            this.accLabel.Location = new System.Drawing.Point(520, 43);
            this.accLabel.Name = "accLabel";
            this.accLabel.Size = new System.Drawing.Size(52, 13);
            this.accLabel.TabIndex = 4;
            this.accLabel.Text = "Accuracy";
            // 
            // accBox
            // 
            this.accBox.Location = new System.Drawing.Point(578, 40);
            this.accBox.Name = "accBox";
            this.accBox.Size = new System.Drawing.Size(210, 20);
            this.accBox.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 390);
            this.Controls.Add(this.accBox);
            this.Controls.Add(this.accLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.idBox);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.preview);
            this.Name = "MainForm";
            this.Text = "osu!Thumbnail Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel preview;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label accLabel;
        private System.Windows.Forms.TextBox accBox;
    }
}

