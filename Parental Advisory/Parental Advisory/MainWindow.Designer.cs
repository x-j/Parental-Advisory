namespace Parental_Advisory {
    partial class MainWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.invertButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.loadImageButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.resetButton = new System.Windows.Forms.Button();
            this.graphPanel = new System.Windows.Forms.Panel();
            this.brightnessButton = new System.Windows.Forms.Button();
            this.bottomPanel.SuspendLayout();
            this.filterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.topPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "JPG files|*.jpg";
            this.openFileDialog.InitialDirectory = "C:\\\\Users\\xj\\Desktop";
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomPanel.BackColor = System.Drawing.SystemColors.Window;
            this.bottomPanel.Controls.Add(this.filterPanel);
            this.bottomPanel.Controls.Add(this.button1);
            this.bottomPanel.Controls.Add(this.loadImageButton);
            this.bottomPanel.Location = new System.Drawing.Point(12, 299);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(584, 100);
            this.bottomPanel.TabIndex = 0;
            // 
            // filterPanel
            // 
            this.filterPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.filterPanel.Controls.Add(this.brightnessButton);
            this.filterPanel.Controls.Add(this.invertButton);
            this.filterPanel.Location = new System.Drawing.Point(67, 0);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(517, 99);
            this.filterPanel.TabIndex = 2;
            this.filterPanel.Visible = false;
            // 
            // invertButton
            // 
            this.invertButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.invertButton.BackColor = System.Drawing.Color.Transparent;
            this.invertButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.invertButton.FlatAppearance.BorderSize = 0;
            this.invertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.invertButton.Font = new System.Drawing.Font("Open Sans", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.invertButton.ForeColor = System.Drawing.Color.Black;
            this.invertButton.Location = new System.Drawing.Point(3, 4);
            this.invertButton.Name = "invertButton";
            this.invertButton.Size = new System.Drawing.Size(57, 91);
            this.invertButton.TabIndex = 3;
            this.invertButton.Text = "Color invert";
            this.invertButton.UseVisualStyleBackColor = false;
            this.invertButton.Click += new System.EventHandler(this.invertButton_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Open Sans", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(329, -54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(229, 45);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load image";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // loadImageButton
            // 
            this.loadImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.loadImageButton.BackColor = System.Drawing.Color.Transparent;
            this.loadImageButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.loadImageButton.FlatAppearance.BorderSize = 0;
            this.loadImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadImageButton.Font = new System.Drawing.Font("Open Sans", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loadImageButton.ForeColor = System.Drawing.Color.Black;
            this.loadImageButton.Location = new System.Drawing.Point(3, 4);
            this.loadImageButton.Name = "loadImageButton";
            this.loadImageButton.Size = new System.Drawing.Size(58, 91);
            this.loadImageButton.TabIndex = 0;
            this.loadImageButton.Text = "Load image";
            this.loadImageButton.UseVisualStyleBackColor = false;
            this.loadImageButton.Click += new System.EventHandler(this.loadImageButton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(343, 281);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // topPanel
            // 
            this.topPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.topPanel.Controls.Add(this.rightPanel);
            this.topPanel.Controls.Add(this.pictureBox);
            this.topPanel.Location = new System.Drawing.Point(12, 12);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(584, 281);
            this.topPanel.TabIndex = 1;
            // 
            // rightPanel
            // 
            this.rightPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rightPanel.Controls.Add(this.resetButton);
            this.rightPanel.Controls.Add(this.graphPanel);
            this.rightPanel.Location = new System.Drawing.Point(349, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(235, 281);
            this.rightPanel.TabIndex = 1;
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.resetButton.BackColor = System.Drawing.Color.Transparent;
            this.resetButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.resetButton.FlatAppearance.BorderSize = 0;
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetButton.Font = new System.Drawing.Font("Open Sans", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.resetButton.ForeColor = System.Drawing.Color.Black;
            this.resetButton.Location = new System.Drawing.Point(3, 238);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(229, 40);
            this.resetButton.TabIndex = 1;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = false;
            this.resetButton.Visible = false;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // graphPanel
            // 
            this.graphPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.graphPanel.Location = new System.Drawing.Point(0, 0);
            this.graphPanel.Name = "graphPanel";
            this.graphPanel.Size = new System.Drawing.Size(235, 232);
            this.graphPanel.TabIndex = 0;
            this.graphPanel.Visible = false;
            this.graphPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.graphPanel_Paint);
            // 
            // brightnessButton
            // 
            this.brightnessButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.brightnessButton.BackColor = System.Drawing.Color.Transparent;
            this.brightnessButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.brightnessButton.FlatAppearance.BorderSize = 0;
            this.brightnessButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.brightnessButton.Font = new System.Drawing.Font("Open Sans", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.brightnessButton.ForeColor = System.Drawing.Color.Black;
            this.brightnessButton.Location = new System.Drawing.Point(66, 4);
            this.brightnessButton.Name = "brightnessButton";
            this.brightnessButton.Size = new System.Drawing.Size(77, 91);
            this.brightnessButton.TabIndex = 4;
            this.brightnessButton.Text = "Brightness";
            this.brightnessButton.UseVisualStyleBackColor = false;
            this.brightnessButton.Click += new System.EventHandler(this.brightnessButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(608, 411);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.bottomPanel);
            this.MinimumSize = new System.Drawing.Size(417, 434);
            this.Name = "MainWindow";
            this.Text = "WARNING: Graphic content";
            this.bottomPanel.ResumeLayout(false);
            this.filterPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button loadImageButton;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Panel graphPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Button invertButton;
        private System.Windows.Forms.Button brightnessButton;
    }
}

