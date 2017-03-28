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
    public partial class QuantizationControl : Form {

        public QuantizationControl() {
            InitializeComponent();
            ValueObtained = false;
        }

        public bool ValueObtained { get; private set; }

        private void okButton_Click(object sender, EventArgs e) {
            ValueObtained = true;
            Close();
        }

        private void upDown_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == (char)27) {
                ValueObtained = false;
                Close();
            }
        }
    }
}
