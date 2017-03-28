namespace Parental_Advisory {
    partial class QuantizationControl {
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
            this.label = new System.Windows.Forms.Label();
            this.RupDown = new System.Windows.Forms.NumericUpDown();
            this.okButton = new System.Windows.Forms.Button();
            this.GupDown = new System.Windows.Forms.NumericUpDown();
            this.BupDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RupDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GupDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BupDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label.Location = new System.Drawing.Point(12, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(238, 15);
            this.label.TabIndex = 1;
            this.label.Text = "Choose how many colors per color pallete:";
            // 
            // RupDown
            // 
            this.RupDown.Location = new System.Drawing.Point(43, 38);
            this.RupDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.RupDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RupDown.Name = "RupDown";
            this.RupDown.Size = new System.Drawing.Size(59, 20);
            this.RupDown.TabIndex = 2;
            this.RupDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.okButton.FlatAppearance.BorderSize = 0;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.okButton.ForeColor = System.Drawing.Color.Black;
            this.okButton.Location = new System.Drawing.Point(43, 116);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(175, 40);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // GupDown
            // 
            this.GupDown.Location = new System.Drawing.Point(43, 64);
            this.GupDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.GupDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GupDown.Name = "GupDown";
            this.GupDown.Size = new System.Drawing.Size(59, 20);
            this.GupDown.TabIndex = 2;
            this.GupDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // BupDown
            // 
            this.BupDown.Location = new System.Drawing.Point(43, 90);
            this.BupDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.BupDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BupDown.Name = "BupDown";
            this.BupDown.Size = new System.Drawing.Size(59, 20);
            this.BupDown.TabIndex = 2;
            this.BupDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(108, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "RED";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(108, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "GREEN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(108, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "BLUE";
            // 
            // QuantizationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(255, 168);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.BupDown);
            this.Controls.Add(this.GupDown);
            this.Controls.Add(this.RupDown);
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QuantizationControl";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SliderDialog";
            ((System.ComponentModel.ISupportInitialize)(this.RupDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GupDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BupDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        public System.Windows.Forms.NumericUpDown RupDown;
        public System.Windows.Forms.Label label;
        public System.Windows.Forms.NumericUpDown GupDown;
        public System.Windows.Forms.NumericUpDown BupDown;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
    }
}