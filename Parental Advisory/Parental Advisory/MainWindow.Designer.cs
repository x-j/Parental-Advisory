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
            this.rightTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.contrastButton = new System.Windows.Forms.Button();
            this.brightnessButton = new System.Windows.Forms.Button();
            this.invertButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gaussianButton = new System.Windows.Forms.Button();
            this.embossButton = new System.Windows.Forms.Button();
            this.edgeDetectButton = new System.Windows.Forms.Button();
            this.sharpenButton = new System.Windows.Forms.Button();
            this.blurButton = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.orderedDitheringButton = new System.Windows.Forms.Button();
            this.medianButton = new System.Windows.Forms.Button();
            this.thresholdButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.loadImageButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.resetButton = new System.Windows.Forms.Button();
            this.graphPanel = new System.Windows.Forms.Panel();
            this.uquantizationButton = new System.Windows.Forms.Button();
            this.bottomPanel.SuspendLayout();
            this.rightTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.filterPanel.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            this.bottomPanel.Controls.Add(this.rightTabControl);
            this.bottomPanel.Controls.Add(this.button1);
            this.bottomPanel.Controls.Add(this.loadImageButton);
            this.bottomPanel.Location = new System.Drawing.Point(12, 299);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(584, 100);
            this.bottomPanel.TabIndex = 0;
            // 
            // rightTabControl
            // 
            this.rightTabControl.Controls.Add(this.tabPage1);
            this.rightTabControl.Controls.Add(this.tabPage2);
            this.rightTabControl.Controls.Add(this.tabPage3);
            this.rightTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rightTabControl.Location = new System.Drawing.Point(91, 0);
            this.rightTabControl.Name = "rightTabControl";
            this.rightTabControl.SelectedIndex = 0;
            this.rightTabControl.Size = new System.Drawing.Size(493, 100);
            this.rightTabControl.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.filterPanel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(485, 74);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Functions";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // filterPanel
            // 
            this.filterPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.filterPanel.Controls.Add(this.contrastButton);
            this.filterPanel.Controls.Add(this.brightnessButton);
            this.filterPanel.Controls.Add(this.invertButton);
            this.filterPanel.Location = new System.Drawing.Point(-4, -12);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(493, 99);
            this.filterPanel.TabIndex = 3;
            this.filterPanel.Visible = false;
            // 
            // contrastButton
            // 
            this.contrastButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.contrastButton.BackColor = System.Drawing.Color.Transparent;
            this.contrastButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.contrastButton.FlatAppearance.BorderSize = 0;
            this.contrastButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.contrastButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.contrastButton.ForeColor = System.Drawing.Color.Black;
            this.contrastButton.Location = new System.Drawing.Point(176, 15);
            this.contrastButton.Name = "contrastButton";
            this.contrastButton.Size = new System.Drawing.Size(87, 70);
            this.contrastButton.TabIndex = 5;
            this.contrastButton.Text = "Contrast";
            this.contrastButton.UseVisualStyleBackColor = false;
            this.contrastButton.Click += new System.EventHandler(this.contrastButton_Click);
            // 
            // brightnessButton
            // 
            this.brightnessButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.brightnessButton.BackColor = System.Drawing.Color.Transparent;
            this.brightnessButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.brightnessButton.FlatAppearance.BorderSize = 0;
            this.brightnessButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.brightnessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.brightnessButton.ForeColor = System.Drawing.Color.Black;
            this.brightnessButton.Location = new System.Drawing.Point(94, 15);
            this.brightnessButton.Name = "brightnessButton";
            this.brightnessButton.Size = new System.Drawing.Size(76, 70);
            this.brightnessButton.TabIndex = 4;
            this.brightnessButton.Text = "Brightness";
            this.brightnessButton.UseVisualStyleBackColor = false;
            this.brightnessButton.Click += new System.EventHandler(this.brightnessButton_Click);
            // 
            // invertButton
            // 
            this.invertButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.invertButton.BackColor = System.Drawing.Color.Transparent;
            this.invertButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.invertButton.FlatAppearance.BorderSize = 0;
            this.invertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.invertButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.invertButton.ForeColor = System.Drawing.Color.Black;
            this.invertButton.Location = new System.Drawing.Point(10, 15);
            this.invertButton.Name = "invertButton";
            this.invertButton.Size = new System.Drawing.Size(78, 71);
            this.invertButton.TabIndex = 3;
            this.invertButton.Text = "Color invert";
            this.invertButton.UseVisualStyleBackColor = false;
            this.invertButton.Click += new System.EventHandler(this.invertButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gaussianButton);
            this.tabPage2.Controls.Add(this.embossButton);
            this.tabPage2.Controls.Add(this.edgeDetectButton);
            this.tabPage2.Controls.Add(this.sharpenButton);
            this.tabPage2.Controls.Add(this.blurButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(485, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Convolutions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gaussianButton
            // 
            this.gaussianButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gaussianButton.BackColor = System.Drawing.Color.Transparent;
            this.gaussianButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.gaussianButton.FlatAppearance.BorderSize = 0;
            this.gaussianButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gaussianButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gaussianButton.ForeColor = System.Drawing.Color.Black;
            this.gaussianButton.Location = new System.Drawing.Point(347, 0);
            this.gaussianButton.Name = "gaussianButton";
            this.gaussianButton.Size = new System.Drawing.Size(83, 74);
            this.gaussianButton.TabIndex = 16;
            this.gaussianButton.Text = "Gaussian smoothing";
            this.gaussianButton.UseVisualStyleBackColor = false;
            this.gaussianButton.Click += new System.EventHandler(this.gaussianButton_Click);
            // 
            // embossButton
            // 
            this.embossButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.embossButton.BackColor = System.Drawing.Color.Transparent;
            this.embossButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.embossButton.FlatAppearance.BorderSize = 0;
            this.embossButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.embossButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.embossButton.ForeColor = System.Drawing.Color.Black;
            this.embossButton.Location = new System.Drawing.Point(267, 0);
            this.embossButton.Name = "embossButton";
            this.embossButton.Size = new System.Drawing.Size(74, 71);
            this.embossButton.TabIndex = 15;
            this.embossButton.Text = "Emboss";
            this.embossButton.UseVisualStyleBackColor = false;
            this.embossButton.Click += new System.EventHandler(this.embossButton_Click);
            // 
            // edgeDetectButton
            // 
            this.edgeDetectButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.edgeDetectButton.BackColor = System.Drawing.Color.Transparent;
            this.edgeDetectButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.edgeDetectButton.FlatAppearance.BorderSize = 0;
            this.edgeDetectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.edgeDetectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.edgeDetectButton.ForeColor = System.Drawing.Color.Black;
            this.edgeDetectButton.Location = new System.Drawing.Point(174, 0);
            this.edgeDetectButton.Name = "edgeDetectButton";
            this.edgeDetectButton.Size = new System.Drawing.Size(87, 73);
            this.edgeDetectButton.TabIndex = 14;
            this.edgeDetectButton.Text = "Edge detect";
            this.edgeDetectButton.UseVisualStyleBackColor = false;
            this.edgeDetectButton.Click += new System.EventHandler(this.edgeDetectButton_Click);
            // 
            // sharpenButton
            // 
            this.sharpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sharpenButton.BackColor = System.Drawing.Color.Transparent;
            this.sharpenButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.sharpenButton.FlatAppearance.BorderSize = 0;
            this.sharpenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sharpenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sharpenButton.ForeColor = System.Drawing.Color.Black;
            this.sharpenButton.Location = new System.Drawing.Point(92, 0);
            this.sharpenButton.Name = "sharpenButton";
            this.sharpenButton.Size = new System.Drawing.Size(76, 73);
            this.sharpenButton.TabIndex = 13;
            this.sharpenButton.Text = "Sharpen";
            this.sharpenButton.UseVisualStyleBackColor = false;
            this.sharpenButton.Click += new System.EventHandler(this.sharpenButton_Click);
            // 
            // blurButton
            // 
            this.blurButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.blurButton.BackColor = System.Drawing.Color.Transparent;
            this.blurButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.blurButton.FlatAppearance.BorderSize = 0;
            this.blurButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.blurButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.blurButton.ForeColor = System.Drawing.Color.Black;
            this.blurButton.Location = new System.Drawing.Point(3, 0);
            this.blurButton.Name = "blurButton";
            this.blurButton.Size = new System.Drawing.Size(83, 73);
            this.blurButton.TabIndex = 12;
            this.blurButton.Text = "Blur";
            this.blurButton.UseVisualStyleBackColor = false;
            this.blurButton.Click += new System.EventHandler(this.blurButton_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.uquantizationButton);
            this.tabPage3.Controls.Add(this.orderedDitheringButton);
            this.tabPage3.Controls.Add(this.medianButton);
            this.tabPage3.Controls.Add(this.thresholdButton);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(485, 74);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Other";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // orderedDitheringButton
            // 
            this.orderedDitheringButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.orderedDitheringButton.BackColor = System.Drawing.Color.Transparent;
            this.orderedDitheringButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.orderedDitheringButton.FlatAppearance.BorderSize = 0;
            this.orderedDitheringButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.orderedDitheringButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.orderedDitheringButton.ForeColor = System.Drawing.Color.Black;
            this.orderedDitheringButton.Location = new System.Drawing.Point(176, 2);
            this.orderedDitheringButton.Name = "orderedDitheringButton";
            this.orderedDitheringButton.Size = new System.Drawing.Size(72, 69);
            this.orderedDitheringButton.TabIndex = 15;
            this.orderedDitheringButton.Text = "Ordered dithering";
            this.orderedDitheringButton.UseVisualStyleBackColor = false;
            this.orderedDitheringButton.Click += new System.EventHandler(this.orderedDitheringButton_Click);
            // 
            // medianButton
            // 
            this.medianButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.medianButton.BackColor = System.Drawing.Color.Transparent;
            this.medianButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.medianButton.FlatAppearance.BorderSize = 0;
            this.medianButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.medianButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.medianButton.ForeColor = System.Drawing.Color.Black;
            this.medianButton.Location = new System.Drawing.Point(96, 0);
            this.medianButton.Name = "medianButton";
            this.medianButton.Size = new System.Drawing.Size(74, 74);
            this.medianButton.TabIndex = 14;
            this.medianButton.Text = "Median";
            this.medianButton.UseVisualStyleBackColor = false;
            this.medianButton.Click += new System.EventHandler(this.medianButton_Click);
            // 
            // thresholdButton
            // 
            this.thresholdButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.thresholdButton.BackColor = System.Drawing.Color.Transparent;
            this.thresholdButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.thresholdButton.FlatAppearance.BorderSize = 0;
            this.thresholdButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.thresholdButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.thresholdButton.ForeColor = System.Drawing.Color.Black;
            this.thresholdButton.Location = new System.Drawing.Point(3, 0);
            this.thresholdButton.Name = "thresholdButton";
            this.thresholdButton.Size = new System.Drawing.Size(87, 75);
            this.thresholdButton.TabIndex = 13;
            this.thresholdButton.Text = "Thresholding";
            this.thresholdButton.UseVisualStyleBackColor = false;
            this.thresholdButton.Click += new System.EventHandler(this.thresholdButton_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
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
            this.loadImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loadImageButton.ForeColor = System.Drawing.Color.Black;
            this.loadImageButton.Location = new System.Drawing.Point(3, 4);
            this.loadImageButton.Name = "loadImageButton";
            this.loadImageButton.Size = new System.Drawing.Size(82, 91);
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
            this.pictureBox.BackColor = System.Drawing.Color.White;
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
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
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
            this.graphPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.graphPanel_MouseClick);
            this.graphPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.graphPanel_MouseDown);
            this.graphPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graphPanel_MouseMove);
            this.graphPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.graphPanel_MouseUp);
            // 
            // uquantizationButton
            // 
            this.uquantizationButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uquantizationButton.BackColor = System.Drawing.Color.Transparent;
            this.uquantizationButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.uquantizationButton.FlatAppearance.BorderSize = 0;
            this.uquantizationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uquantizationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.uquantizationButton.ForeColor = System.Drawing.Color.Black;
            this.uquantizationButton.Location = new System.Drawing.Point(254, 3);
            this.uquantizationButton.Name = "uquantizationButton";
            this.uquantizationButton.Size = new System.Drawing.Size(84, 68);
            this.uquantizationButton.TabIndex = 15;
            this.uquantizationButton.Text = "Uniform quantization";
            this.uquantizationButton.UseVisualStyleBackColor = false;
            this.uquantizationButton.Click += new System.EventHandler(this.uquantizationButton_Click);
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
            this.rightTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.filterPanel.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl rightTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Button contrastButton;
        private System.Windows.Forms.Button brightnessButton;
        private System.Windows.Forms.Button invertButton;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button gaussianButton;
        private System.Windows.Forms.Button embossButton;
        private System.Windows.Forms.Button edgeDetectButton;
        private System.Windows.Forms.Button sharpenButton;
        private System.Windows.Forms.Button blurButton;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button thresholdButton;
        private System.Windows.Forms.Button medianButton;
        private System.Windows.Forms.Button orderedDitheringButton;
        private System.Windows.Forms.Button uquantizationButton;
    }
}

