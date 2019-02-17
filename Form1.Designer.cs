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
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.defaultButton = new System.Windows.Forms.Button();
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
            this.generateButton.Location = new System.Drawing.Point(501, 320);
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
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(501, 291);
            this.loadButton.Name = "loadButton";
            this.loadButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.loadButton.Size = new System.Drawing.Size(143, 23);
            this.loadButton.TabIndex = 9;
            this.loadButton.Text = "Load Layout";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(501, 349);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(290, 23);
            this.saveButton.TabIndex = 14;
            this.saveButton.Text = "Export Image";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.AutoScroll = true;
            this.propertiesPanel.BackColor = System.Drawing.SystemColors.Control;
            this.propertiesPanel.Location = new System.Drawing.Point(498, 38);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(293, 247);
            this.propertiesPanel.TabIndex = 15;
            // 
            // defaultButton
            // 
            this.defaultButton.Location = new System.Drawing.Point(648, 291);
            this.defaultButton.Name = "defaultButton";
            this.defaultButton.Size = new System.Drawing.Size(143, 23);
            this.defaultButton.TabIndex = 16;
            this.defaultButton.Text = "Set as default";
            this.defaultButton.UseVisualStyleBackColor = true;
            this.defaultButton.Click += new System.EventHandler(this.defaultButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 390);
            this.Controls.Add(this.defaultButton);
            this.Controls.Add(this.propertiesPanel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.idBox);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.preview);
            this.MaximizeBox = false;
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
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel propertiesPanel;
        private System.Windows.Forms.Button defaultButton;
    }
}

