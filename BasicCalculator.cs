using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BigNumbers
{
    public partial class BasicCalculator : Form
    {
        public BasicCalculator()
        {
            InitializeComponent();
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                BigNum bigNum1 = new BigNum(txtBigNum1.Text);
                BigNum bigNum2 = new BigNum(txtBigNum2.Text);
                BigNum result = new BigNum();
                BigNum remainder;

                switch (cmbOperators.SelectedIndex)
                {
                    case 0: // +
                        result = bigNum1.Add(bigNum2);
                        break;

                    case 1: // -
                        result = bigNum1.Subtract(bigNum2);
                        break;

                    case 2: // *
                        result = bigNum1.Multiplication(bigNum2);
                        break;

                    case 3: // /
                        result = bigNum1.Division(bigNum2, out remainder, (int)nudDecimalPlaces.Value);
                        break;

                    case 4:
                        bigNum1.Division(bigNum2, out remainder, 50);
                        result = new BigNum(remainder);
                        break;
                }

                txtResult.Text = result.ToString();
            }
            catch (Exception exception)
            {
                txtResult.Clear();
                MessageBox.Show(exception.Message);
            }
        }

        private void cmbOperators_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbOperators.SelectedIndex == 3)
            {
                lblDecimalPlaces.Show();
                nudDecimalPlaces.Show();
            }
            else
            {
                lblDecimalPlaces.Hide();
                nudDecimalPlaces.Hide();
            }
        }

        private void BasicCalculator_Load(object sender, EventArgs e)
        {
            cmbOperators.SelectedIndex = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBigNum1.Clear();
            txtBigNum2.Clear();
            cmbOperators.SelectedIndex = 0;
            txtResult.Clear();
        }
    }
}
