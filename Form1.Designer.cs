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
            this.fontButton = new System.Windows.Forms.Button();
            this.starBox = new System.Windows.Forms.TextBox();
            this.starLabel = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.rankingBox = new System.Windows.Forms.TextBox();
            this.rankingLabel = new System.Windows.Forms.Label();
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
            // fontButton
            // 
            this.fontButton.Location = new System.Drawing.Point(501, 320);
            this.fontButton.Name = "fontButton";
            this.fontButton.Size = new System.Drawing.Size(287, 23);
            this.fontButton.TabIndex = 6;
            this.fontButton.Text = "Select Font";
            this.fontButton.UseVisualStyleBackColor = true;
            this.fontButton.Click += new System.EventHandler(this.fontButton_Click);
            // 
            // starBox
            // 
            this.starBox.Location = new System.Drawing.Point(578, 66);
            this.starBox.Name = "starBox";
            this.starBox.Size = new System.Drawing.Size(210, 20);
            this.starBox.TabIndex = 8;
            // 
            // starLabel
            // 
            this.starLabel.AutoSize = true;
            this.starLabel.Location = new System.Drawing.Point(512, 69);
            this.starLabel.Name = "starLabel";
            this.starLabel.Size = new System.Drawing.Size(60, 13);
            this.starLabel.TabIndex = 7;
            this.starLabel.Text = "Star Rating";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(501, 291);
            this.loadButton.Name = "loadButton";
            this.loadButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.loadButton.Size = new System.Drawing.Size(287, 23);
            this.loadButton.TabIndex = 9;
            this.loadButton.Text = "Load Layout";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // rankingBox
            // 
            this.rankingBox.Location = new System.Drawing.Point(578, 92);
            this.rankingBox.Name = "rankingBox";
            this.rankingBox.Size = new System.Drawing.Size(210, 20);
            this.rankingBox.TabIndex = 11;
            // 
            // rankingLabel
            // 
            this.rankingLabel.AutoSize = true;
            this.rankingLabel.Location = new System.Drawing.Point(525, 95);
            this.rankingLabel.Name = "rankingLabel";
            this.rankingLabel.Size = new System.Drawing.Size(47, 13);
            this.rankingLabel.TabIndex = 10;
            this.rankingLabel.Text = "Ranking";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 390);
            this.Controls.Add(this.rankingBox);
            this.Controls.Add(this.rankingLabel);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.starBox);
            this.Controls.Add(this.starLabel);
            this.Controls.Add(this.fontButton);
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
        private System.Windows.Forms.Button fontButton;
        private System.Windows.Forms.TextBox starBox;
        private System.Windows.Forms.Label starLabel;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.TextBox rankingBox;
        private System.Windows.Forms.Label rankingLabel;
    }
}

