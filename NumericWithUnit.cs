using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Windows.Forms.Design;
using System.Globalization;
using System.Reflection;

namespace NumericUnit
{
    [Description("A text box that accepts numeric input followed by a character string.  Together these are interpreted as a number and a unit (one of the units of the AllowedUnits collection)")]
    public partial class NumericWithUnit : TextBox
    {
        //internal class AllowedUnitsCollectionEditor : CollectionEditor
        //{

        //    public AllowedUnitsCollectionEditor(Type type) : base(type) { }

        //    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        //    {
        //        object result = base.EditValue(context, provider, value);

        //        // assign the temporary collection from the UI to the property
        //        ((NumericWithUnit)context.Instance).AllowedUnits = (ObservableCollection<Unit>)result;

        //        return result;
        //    }
        //}

        internal class UnitConverter : TypeConverter
        {
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(InstanceDescriptor) && value is Unit)
                {
                    ConstructorInfo constructor = typeof(Unit).GetConstructor(new[] { typeof(string), typeof(double) });

                    var filter = value as Unit;
                    var descriptor = new InstanceDescriptor(constructor, new object[] { filter.UnitString, filter.UnitValue }, true);

                    return descriptor;
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        [Serializable]
        /// <summary>
        /// A description of a Unit
        /// </summary>
        public class Unit
        {
            public string UnitString { get; set; }
            public double UnitValue { get; set; }
            /// <summary>
            /// Creates a Unit which represents the value 1 (no unit).
            /// </summary>
            public Unit() { UnitString = ""; UnitValue = 1; }
            /// <summary>
            /// Creates a Unit which represents a value.
            /// </summary>
            /// <param name="unitString">The unit string ("milliseconds", "kilograms"...).</param>
            /// <param name="unitValue">The value of the unit in base units (if working in SI units, "milliseconds" would have the value 0.001).</param>
            public Unit(string unitString, double unitValue)
            {
                UnitString = unitString;
                UnitValue = unitValue;
            }
        }

        [Description("A collection of allowed units.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(UnitConverter))]
        /// <summary>
        /// A collection of allowed units.
        /// </summary>
        public ObservableCollection<Unit> AllowedUnits { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(CollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(UnitConverter))]
        public List<Unit> TestUnits { get; set; }

        private bool forbiddenKey = false;
        private bool enterKey = false;
        private bool internalSet = false;

        private Regex formatRegex;
        private string unitsRegexString = "()$";
        private string numberRegexString = @"^\s*[-+]?[0-9]+\.?[0-9]*([eE][-+]?[0-9]+)?\s*";
        //private string numberRegexString = @"[+-]?[0-9]+\.?[0-9]*\s*";

        [Description("The Maximum (unitless) value allowed to be entered.")]
        [Category("Data")]
        /// <summary>
        /// The Maximum (unitless) value allowed to be entered.
        /// </summary>
        public double Maximum { get; set; }

        [Description("The Minimum (unitless) value allowed to be entered.")]
        [Category("Data")]
        /// <summary>
        /// The Minimum (unitless) value allowed to be entered.
        /// </summary>
        public double Minimum { get; set; }

        private double value;

        [Description("The current (unitless) value.")]
        [Category("Appearance")]
        /// <summary>
        /// The current (unitless) value.
        /// </summary>
        public double Value
        {
            get { return value; }
            set
            {
                // try to set it, throw exceptions if error
                // these are caught but allow the debugger to catch them if set up in Visual Studio
                try
                {
                    // check range
                    if (value > Maximum) throw new ArgumentOutOfRangeException("Value", "Tried to set 'Value' to above 'Maximum'.");
                    if (value < Minimum) throw new ArgumentOutOfRangeException("Value", "Tried to set 'Value' to below 'Minimum'.");
                    
                    this.value = value;

                    // set the text as well
                    // set flag not to change colour
                    internalSet = true;
                    Text = makeString(this.value);
                    internalSet = false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    // do nothing with it
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The text shown.")]
        [Category("Appearance")]
        /// <summary>
        /// Gets or sets the current text in the NumericWithUnit.
        /// </summary>
        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
            }
        }

        [Description("The number of decimal round the value up to.")]
        [Category("Data")]
        /// <summary>
        /// How many Decimal places to allow, relative to SI unit!  e.g. if DecimalPlaces = 3, then 1ms is allowed, but 0.1 ms isn't
        /// </summary>
        public int DecimalPlaces { get; set; }

        [Description("The format string used to generate the UI Text.  Must be a valid format string for the arguments {0} being the double value, and {1} being a the unit string.")]
        [Category("Data")]
        /// <summary>
        /// The format string used to generate the UI Text.  Must be a valid format string for the arguments {0} being the double value, and {1} being a the unit string.
        /// </summary>
        public string DisplayFormat
        {
            get { return _displayFormat; }
            set
            {
                // test the display format to make sure it is correct
                // just try to format something simple
                try
                {
                    if (value != null) String.Format(value, 0, "V");
                    
                    // we got this far, so format should be ok!
                    _displayFormat = value;
                }
                catch (FormatException ex)
                {
                    // don't do anything with it
                }
            }
        }
        private string _displayFormat;

        [Description("The color that the background is set to when a correct value is entered, before pressing enter.")]
        [Category("Appearance")]
        /// <summary>
        /// The color that the background is set to when a correct value is entered, before pressing enter.
        /// </summary>
        public Color CorrectColor { get; set; }

        [Description("The color that the background is set to when an incorrect value is entered.")]
        [Category("Appearance")]
        /// <summary>
        /// The color that the background is set to when an incorrect value is entered.
        /// </summary>
        public Color IncorrectColor { get; set; }

        [Description("The default background color of the control.")]
        [Category("Appearance")]
        /// <summary>
        /// The default background color of the control.
        /// </summary>
        public Color DefaultColor { get; set; }

        [Description("Event raised when the internal value of the control was successfully changed.")]
        [Category("Action")]
        /// <summary>
        /// Value of the control was successfully changed.
        /// </summary>
        public event EventHandler<EventArgs> ValueChanged;
        
        [Description("Event raised when the Enter key was pressed on the control which reuslted in a successfull update of the value.")]
        [Category("Action")]
        /// <summary>
        /// Enter was pressed on the control which resulted in a successful update of the value.
        /// </summary>
        public event EventHandler<EventArgs> EnterPressed;



        public NumericWithUnit()
        {
            InitializeComponent();

            // unitsAllowedUnits
            AllowedUnits = new ObservableCollection<Unit>();
            TestUnits = new List<Unit>();

            // default units (blank one)
            AllowedUnits.Add(new Unit());
            Minimum = 0;
            Maximum = 1;
            DecimalPlaces = 13;
            DisplayFormat = null;
            
            // defaults
            CorrectColor = Color.LightGreen;
            IncorrectColor = Color.Red;
            DefaultColor = Color.White;

            // initial build of regex
            rebuildRegex();

            // listeners
            AllowedUnits.CollectionChanged += collectionChanged;
        }

        private void collectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // need to update?
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // reorder
                AllowedUnits = new ObservableCollection<Unit>(AllowedUnits.OrderByDescending(a => a.UnitValue));
                AllowedUnits.CollectionChanged += collectionChanged;

                // rebuild regexes
                rebuildRegex();
            }
        }


        private void rebuildRegex()
        {
            string regexString = numberRegexString;

            // add the formatsz
            unitsRegexString = "";
            if (AllowedUnits.Count > 0)
            {
                unitsRegexString = "(";
                for (int i = 0; i < AllowedUnits.Count; i++)
                {
                    unitsRegexString += AllowedUnits[i].UnitString + "|";
                }
                unitsRegexString = unitsRegexString.Remove(unitsRegexString.Length - 1);   // remove trailing "|"
                unitsRegexString += ")(?<=[\\s]*)$";
            }
            else
            {
                unitsRegexString = "(?<=[\\s]*)$";
            }

            formatRegex = new Regex(regexString + unitsRegexString);
        }


        /// <summary>
        /// Some taken from https://msdn.microsoft.com/en-us/library/system.windows.forms.keyeventargs.keycode(v=vs.110).aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
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
            bool allowed = numberEntered || letterEntered || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Space || e.KeyCode == Keys.Decimal || e.KeyCode == Keys.OemPeriod || e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Oemplus;

            // enter?
            enterKey = e.KeyCode == Keys.Enter;

            // not allowed
            if (!allowed && !enterKey)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            forbiddenKey = !allowed;

            // call base
            base.OnKeyDown(e);
        }


        /// <summary>
        /// Raises the Control.KeyUp event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            // reset
            forbiddenKey = false;
            enterKey = false;

            // call base
            base.OnKeyUp(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            // reset
            forbiddenKey = false;
            enterKey = false;

            // call base
            base.OnLeave(e);
        }

        /// <summary>
        /// Key pressed.  Check that format is correct.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {

            if (forbiddenKey)
            {
                e.Handled = true;
            }

            // enter?
            if (enterKey)
            {
                VerifyInput();
            }

            // base
            base.OnKeyPress(e);
        }


        /// <summary>
        /// Try to verify the input
        /// </summary>
        /// <returns></returns>
        public bool VerifyInput()
        {
            // try to convert
            double number;
            bool verified = extractValue(out number);
            if (verified)
            {
                // succes, set value and clear BG colour
                value = number;
                BackColor = DefaultColor;
                if (ValueChanged != null) ValueChanged(this, new EventArgs());

                // make text as user may have entered just a number
                allowTextUpdateEvent = false;
                Text = makeString(value);
                allowTextUpdateEvent = true;

                // was enter pressed to get here?
                if (enterKey)
                {
                    // fire event
                    if (EnterPressed != null) EnterPressed(this, new EventArgs());
                }
            }
            else
            {
                BackColor = IncorrectColor;
            }
            return verified;
        }

        private bool allowTextUpdateEvent = true;

        /// <summary>
        /// Text internally changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {

            if (!allowTextUpdateEvent) return;
            bool allowed = false;
            double number;
            
            allowed = extractValue(out number);

            BackColor = !internalSet ? (allowed ? CorrectColor : IncorrectColor) : DefaultColor;
            internalSet = false;

            // base
            base.OnTextChanged(e);
        }


        /// <summary>
        /// Take the current value of the text box and try to extract it.
        /// </summary>
        /// <param name="?">True if the number written is within the range of the box and correctly formatted.</param>
        /// <returns></returns>
        private bool extractValue(out double number)
        {
            MatchCollection matches = formatRegex.Matches(Text.Trim());
            number = -1;
            bool allowed = false;

            // matched?
            if (matches.Count == 1)
            {
                // try to extract the number and the unit
                string numberString = new Regex(numberRegexString).Match(Text.Trim()).Value.Trim();
                string unit = new Regex(unitsRegexString).Match(Text.Trim()).Value.Trim();

                // try to parse
                if (Double.TryParse(numberString, out number))
                {
                    double unitValue = 1;
                    if (AllowedUnits.Count > 0)
                    {
                        unitValue = AllowedUnits.Where(uw => uw.UnitString == unit).ToList()[0].UnitValue;
                    }

                    // get real value
                    number = number * unitValue;

                    // round to decimal places
                    number = Math.Round(number, DecimalPlaces);

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
            string numberText;
            if (AllowedUnits.Count > 0)
            {
                // to find the best unit, got throug all units until we find one that is lower than the value
                // if none is found, use the smallest
                unit = AllowedUnits[AllowedUnits.Count - 1];
                for (int i = 0; i < AllowedUnits.Count; i++)
                {
                    // skip if the base unit (1)
                    if (AllowedUnits[i].UnitString == "") continue;

                    if (Math.Abs(AllowedUnits[i].UnitValue) <= Math.Abs(value))
                    {
                        // pic this unit
                        unit = AllowedUnits[i];
                        break;
                    }
                }

                // make text
                if (DisplayFormat == null)
                {
                    numberText = "" + value / unit.UnitValue + (unit.UnitString == "" ? "" : " " + unit.UnitString);
                }
                else
                {
                    numberText = String.Format(DisplayFormat, value / unit.UnitValue, unit.UnitString);
                }
            }
            else
            {
                // else assume no unit
                numberText = "" + value;
            }

            return numberText;
        }
    }
}
