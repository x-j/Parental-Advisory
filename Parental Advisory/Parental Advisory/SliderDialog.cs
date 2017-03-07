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

        internal void Reset() {
            upDown.Value = 0;
        }

        internal void MoveSliderTo(int v) {
            upDown.Value = v;
        }

        private void okButton_Click(object sender, EventArgs e) {
            Value = (int)upDown.Value;
            ValueObtained = true;
            Close();
        }

        private void upDown_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)27) {
                ValueObtained = false;
                Close();
            }
        }
    }
}
