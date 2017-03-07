using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parental_Advisory {
    public partial class SliderDialog : Form {
        public SliderDialog() {
            InitializeComponent();
            ValueObtained = false;
        }

        public int Value {
            get; set;
        }
        public bool ValueObtained {
            get;
            private set;
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e) {
            this.Value = trackBar.Value;
            ValueObtained = true;
            this.Close();
        }

        private void trackBar_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == (char)27) {
                ValueObtained = false;
                this.Close();
            }
        }

        internal void Reset() {
            trackBar.Value = 0;
        }

        internal void MoveSliderTo(int v) {
            trackBar.Value = v;
        }
    }
}
