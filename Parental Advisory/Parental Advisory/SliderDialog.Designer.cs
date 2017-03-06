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
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.LargeChange = 50;
            this.trackBar.Location = new System.Drawing.Point(12, 38);
            this.trackBar.Maximum = 255;
            this.trackBar.Minimum = -255;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(231, 45);
            this.trackBar.SmallChange = 25;
            this.trackBar.TabIndex = 0;
            this.trackBar.TickFrequency = 25;
            this.trackBar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.trackBar_KeyPress);
            this.trackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseUp);
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
            // SliderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(255, 80);
            this.ControlBox = false;
            this.Controls.Add(this.label);
            this.Controls.Add(this.trackBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SliderDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SliderDialog";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label label;
    }
}