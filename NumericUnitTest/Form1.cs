using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumericUnitTest
{
    public partial class Form1 : Form
    {
        public string BoundText { get; set; }
        public event EventHandler BoundTextChanged;

        public double BoundValue { get; set; }
        public event EventHandler BoundValueChanged;

        public Form1()
        {
            InitializeComponent();
            BoundText = "BoundText";
            numericWithUnit1.Maximum = 0;
            numericWithUnit1.Maximum = 100;
            numericWithUnit1.Value = 0;
            numericWithUnit1.EnterPressed += NumericWithUnit1_EnterPressed;
            
            textBinding = new Binding("Text", this, "BoundText");
            valueBinding = new Binding("Value", this, "BoundValue");
        }

        private void NumericWithUnit1_EnterPressed(object sender, EventArgs e)
        {
            MessageBox.Show("Enter Pressed.  Unitless value is " + numericWithUnit1.Value.ToString("F" + numericWithUnit1.DecimalPlaces));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numericWithUnit1.AllowedUnits.Clear();
            numericWithUnit1.AllowedUnits.Add(new NumericUnit.NumericWithUnit.Unit());
            numericWithUnit1.AllowedUnits.Add(new NumericUnit.NumericWithUnit.Unit("s", 1));
            numericWithUnit1.AllowedUnits.Add(new NumericUnit.NumericWithUnit.Unit("ms", 1e-3));
        }



        private void numericWithUnit1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            numericWithUnit1.Value = 3;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Unitless value is " + numericWithUnit1.Value.ToString("F" + numericWithUnit1.DecimalPlaces));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            numericWithUnit1.Text = "3 s";
        }

        private void buttonSetBoundText_Click(object sender, EventArgs e)
        {
            BoundText = textBoundText.Text;
            if (BoundTextChanged != null) BoundTextChanged(buttonSetBoundText, EventArgs.Empty);
        }

        Binding textBinding = null;
        private void buttonBindText_Click(object sender, EventArgs e)
        {
            numericWithUnit1.DataBindings.Add(textBinding);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoundText.Text = BoundText;
        }

        Binding valueBinding = null;
        private void buttonBindValue_Click(object sender, EventArgs e)
        {
            numericWithUnit1.DataBindings.Add(valueBinding);
        }

        private void buttonSetBoundValue_Click(object sender, EventArgs e)
        {
            BoundValue = (double)numericBoundValue.Value;
            if (BoundValueChanged != null) BoundValueChanged(buttonSetBoundValue, EventArgs.Empty);
        }

        private void buttonGetBoundValue_Click(object sender, EventArgs e)
        {
            numericBoundValue.Value = (decimal)BoundValue;
        }
    }
}
