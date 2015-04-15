using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace NumericUnit
{
    public partial class NumericWithUnit: UserControl
    {
        /// <summary>
        /// A description of a Unit
        /// </summary>
        public class Unit
        {
            public string UnitString {get; private set;}
            public double UnitValue{get; private set;}
            public Unit() { UnitString = ""; UnitValue = 1; }
            public Unit(string unitString, double unitValue)
            {
                UnitString = unitString;
                UnitValue = unitValue;
            }
        }

        public ObservableCollection<Unit> AllowedUnits { get; private set; }

        private bool forbiddenKey = false;
        private bool enterKey = false;
        private bool internalSet = false;

        private Regex formatRegex;
        private string unitsRegexString = "()$";
        private string numberRegexString = "(?=[\\s]*)[0-9]+(.)?[0-9]*(?<=[\\s]*)";

        // max and min values
        public double Maximum { get; set; }
        public double Minimum { get; set; }
        private double value;
        public double Value
        {
            get { return value; }
            set
            {
                this.value = value;

                // set the text as well
                // set flag not to change colour
                internalSet = true;
                Text = makeString(this.value);
            }
        }
        public override string Text { get { return textBox.Text; } set { textBox.Text = value; } }

        // value changed event
        public event EventHandler<EventArgs> ValueChanged;
        
        public NumericWithUnit()
        {
            
            InitializeComponent();

            // unitsAllowedUnits
            AllowedUnits = new ObservableCollection<Unit>();

            // default units
            //AllowedUnits.Add(new Tuple<string, double>("s", 1));
            //AllowedUnits.Add(new Tuple<string, double>("ms", 1e-3));
            //AllowedUnits.Add(new Tuple<string, double>("mus", 1e-6));
            //AllowedUnits.Add(new Tuple<string, double>("ns", 1e-9));
            //AllowedUnits.Add(new Tuple<string, double>("fs", 1e-12));
            //Minimum = 0;
            //Maximum = 5e-2;

            //rebuildRegex();

            // listeners
            AllowedUnits.CollectionChanged += (o, e) => rebuildRegex();
        }

        private void rebuildRegex()
        {
            string regexString = numberRegexString;

            // add the formats
            unitsRegexString = "";
            if (AllowedUnits.Count > 0)
            {
                unitsRegexString = "(";
                for (int i = 0; i < AllowedUnits.Count; i++)
                {
                    unitsRegexString += AllowedUnits[i].UnitString + "|";
                }
                unitsRegexString = unitsRegexString.Remove(unitsRegexString.Length - 1);
                unitsRegexString += ")$";
            }

            formatRegex = new Regex(regexString + unitsRegexString);
        }


        /// <summary>
        /// Some taken from https://msdn.microsoft.com/en-us/library/system.windows.forms.keyeventargs.keycode(v=vs.110).aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            // reset
            forbiddenKey = false;
            enterKey = false;

            // what was entered?
            bool numberEntered = false;
            bool letterEntered = false;

            // from https://msdn.microsoft.com/en-us/library/system.windows.forms.keyeventargs.keycode(v=vs.110).aspx

            // number key?
            numberEntered = ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)) && (Control.ModifierKeys != Keys.Shift);

            // letter?
            letterEntered = (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z);

            // or other allowed keys?
            bool allowed = numberEntered || letterEntered || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Space || e.KeyCode == Keys.Decimal || e.KeyCode == Keys.OemPeriod;

            // enter?
            enterKey = e.KeyCode == Keys.Enter;

            // not allowed
            if (!allowed && !enterKey)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            forbiddenKey = !allowed;
        }


        /// <summary>
        /// Key pressed.  Check that format is correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (forbiddenKey)
            {
                e.Handled = true;
            }

            // enter?
            if (enterKey)
            {
                // try to convert
                double number;
                if (extractValue(out number))
                {
                    // succes, set value and clear BG colour
                    value = number;
                    textBox.BackColor = Color.White;
                    if (ValueChanged != null) ValueChanged(this, new EventArgs());
                }
            }
        }


        /// <summary>
        /// Text internally changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            bool allowed = false;
            double number;

            // if an internal set, don't do anything
            if (!internalSet)
            {
                MatchCollection matches = formatRegex.Matches(textBox.Text);

                allowed = extractValue(out number);

                textBox.BackColor = allowed ? Color.LightGreen : Color.Red;
            }
            internalSet = false;
        }


        /// <summary>
        /// Take the current value of the text box and try to extract it.
        /// </summary>
        /// <param name="?">True if the number written is within the range of the box and correctly formatted.</param>
        /// <returns></returns>
        private bool extractValue(out double number)
        {
            MatchCollection matches = formatRegex.Matches(textBox.Text);
            number = -1;
            bool allowed = false;

            // matched?
            if (matches.Count == 1)
            {
                // try to extract the number and the unit
                string numberString = new Regex(numberRegexString).Match(textBox.Text).Value.Trim();
                string unit = new Regex(unitsRegexString).Match(textBox.Text).Value.Trim();

                // try to parse
                if (Double.TryParse(numberString, out number))
                {
                    double unitValue = AllowedUnits.Where(uw => uw.UnitString == unit).ToList()[0].UnitValue;

                    // get real value
                    number = number * unitValue;

                    // within limits?
                    if (number >= Minimum && number <= Maximum)
                    {
                        allowed = true;
                    }
                }
            }

            return allowed;
        }

        /// <summary>
        /// Take a value, and make a test string using the appropriate unit
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string makeString(double value)
        {
            Unit unit;
            if (AllowedUnits.Count > 0)
            {
                // to find the best unit, got throug all units until we find one that is lower than the value
                // if none is found, use the smallest
                unit = AllowedUnits[AllowedUnits.Count - 1];
                for (int i = 0; i < AllowedUnits.Count; i++)
                {
                    if (AllowedUnits[i].UnitValue <= value)
                    {
                        // pic this unit
                        unit = AllowedUnits[i];
                        break;
                    }
                }
            }
            else
            {
                unit = new Unit("", 1);
            }

            // now make the thing
            string numberText = "" + value / unit.UnitValue + " " + unit.UnitString;
            return numberText;
        }

    }
}
