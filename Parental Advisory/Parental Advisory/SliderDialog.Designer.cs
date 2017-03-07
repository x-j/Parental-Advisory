namespace Parental_Advisory {
    partial class SliderDialog {
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
            this.upDown = new System.Windows.Forms.NumericUpDown();
            this.okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.upDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label.Location = new System.Drawing.Point(12, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(206, 15);
            this.label.TabIndex = 1;
            this.label.Text = "Please choose the value for the filter:";
            // 
            // upDown
            // 
            this.upDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.upDown.Location = new System.Drawing.Point(43, 38);
            this.upDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.upDown.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.upDown.Name = "upDown";
            this.upDown.Size = new System.Drawing.Size(59, 20);
            this.upDown.TabIndex = 2;
            this.upDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.upDown_KeyPress);
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
            this.okButton.Location = new System.Drawing.Point(143, 31);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(86, 30);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // SliderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(255, 80);
            this.ControlBox = false;
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.upDown);
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SliderDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SliderDialog";
            ((System.ComponentModel.ISupportInitialize)(this.upDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.NumericUpDown upDown;
        private System.Windows.Forms.Button okButton;
    }
}