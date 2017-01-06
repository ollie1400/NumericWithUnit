namespace NumericUnitTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.numericWithUnit1 = new NumericUnit.NumericWithUnit();
            this.buttonSetBoundText = new System.Windows.Forms.Button();
            this.textBoundText = new System.Windows.Forms.TextBox();
            this.buttonBindText = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonGetBoundValue = new System.Windows.Forms.Button();
            this.buttonBindValue = new System.Windows.Forms.Button();
            this.buttonSetBoundValue = new System.Windows.Forms.Button();
            this.numericBoundValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericBoundValue)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Set Time Units";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Set value";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 108);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(132, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Ask Value";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(13, 137);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(132, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Set text";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // numericWithUnit1
            // 
            this.numericWithUnit1.BackColor = System.Drawing.Color.White;
            this.numericWithUnit1.CorrectColor = System.Drawing.Color.LightGreen;
            this.numericWithUnit1.DecimalPlaces = ((uint)(3u));
            this.numericWithUnit1.DefaultColor = System.Drawing.Color.White;
            this.numericWithUnit1.DisplayFormat = null;
            this.numericWithUnit1.IncorrectColor = System.Drawing.Color.Red;
            this.numericWithUnit1.Location = new System.Drawing.Point(30, 12);
            this.numericWithUnit1.Maximum = 1D;
            this.numericWithUnit1.Minimum = 0D;
            this.numericWithUnit1.Name = "numericWithUnit1";
            this.numericWithUnit1.Size = new System.Drawing.Size(100, 20);
            this.numericWithUnit1.TabIndex = 5;
            this.numericWithUnit1.Value = 0D;
            // 
            // buttonSetBoundText
            // 
            this.buttonSetBoundText.Location = new System.Drawing.Point(255, 50);
            this.buttonSetBoundText.Name = "buttonSetBoundText";
            this.buttonSetBoundText.Size = new System.Drawing.Size(107, 23);
            this.buttonSetBoundText.TabIndex = 6;
            this.buttonSetBoundText.Text = "Set Bound Text";
            this.buttonSetBoundText.UseVisualStyleBackColor = true;
            this.buttonSetBoundText.Click += new System.EventHandler(this.buttonSetBoundText_Click);
            // 
            // textBoundText
            // 
            this.textBoundText.Location = new System.Drawing.Point(368, 52);
            this.textBoundText.Name = "textBoundText";
            this.textBoundText.Size = new System.Drawing.Size(100, 20);
            this.textBoundText.TabIndex = 7;
            // 
            // buttonBindText
            // 
            this.buttonBindText.Location = new System.Drawing.Point(179, 50);
            this.buttonBindText.Name = "buttonBindText";
            this.buttonBindText.Size = new System.Drawing.Size(70, 23);
            this.buttonBindText.TabIndex = 8;
            this.buttonBindText.Text = "Bind Text";
            this.buttonBindText.UseVisualStyleBackColor = true;
            this.buttonBindText.Click += new System.EventHandler(this.buttonBindText_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(474, 49);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(105, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "Get Bound Text";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonGetBoundValue
            // 
            this.buttonGetBoundValue.Location = new System.Drawing.Point(474, 79);
            this.buttonGetBoundValue.Name = "buttonGetBoundValue";
            this.buttonGetBoundValue.Size = new System.Drawing.Size(105, 23);
            this.buttonGetBoundValue.TabIndex = 13;
            this.buttonGetBoundValue.Text = "Get Bound Value";
            this.buttonGetBoundValue.UseVisualStyleBackColor = true;
            this.buttonGetBoundValue.Click += new System.EventHandler(this.buttonGetBoundValue_Click);
            // 
            // buttonBindValue
            // 
            this.buttonBindValue.Location = new System.Drawing.Point(179, 80);
            this.buttonBindValue.Name = "buttonBindValue";
            this.buttonBindValue.Size = new System.Drawing.Size(70, 23);
            this.buttonBindValue.TabIndex = 12;
            this.buttonBindValue.Text = "Bind Value";
            this.buttonBindValue.UseVisualStyleBackColor = true;
            this.buttonBindValue.Click += new System.EventHandler(this.buttonBindValue_Click);
            // 
            // buttonSetBoundValue
            // 
            this.buttonSetBoundValue.Location = new System.Drawing.Point(255, 80);
            this.buttonSetBoundValue.Name = "buttonSetBoundValue";
            this.buttonSetBoundValue.Size = new System.Drawing.Size(107, 23);
            this.buttonSetBoundValue.TabIndex = 10;
            this.buttonSetBoundValue.Text = "Set Bound Value";
            this.buttonSetBoundValue.UseVisualStyleBackColor = true;
            this.buttonSetBoundValue.Click += new System.EventHandler(this.buttonSetBoundValue_Click);
            // 
            // numericBoundValue
            // 
            this.numericBoundValue.DecimalPlaces = 10;
            this.numericBoundValue.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericBoundValue.Location = new System.Drawing.Point(369, 81);
            this.numericBoundValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericBoundValue.Name = "numericBoundValue";
            this.numericBoundValue.Size = new System.Drawing.Size(99, 20);
            this.numericBoundValue.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 196);
            this.Controls.Add(this.numericBoundValue);
            this.Controls.Add(this.buttonGetBoundValue);
            this.Controls.Add(this.buttonBindValue);
            this.Controls.Add(this.buttonSetBoundValue);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.buttonBindText);
            this.Controls.Add(this.textBoundText);
            this.Controls.Add(this.buttonSetBoundText);
            this.Controls.Add(this.numericWithUnit1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericBoundValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private NumericUnit.NumericWithUnit numericWithUnit1;
        private System.Windows.Forms.Button buttonSetBoundText;
        private System.Windows.Forms.TextBox textBoundText;
        private System.Windows.Forms.Button buttonBindText;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonGetBoundValue;
        private System.Windows.Forms.Button buttonBindValue;
        private System.Windows.Forms.Button buttonSetBoundValue;
        private System.Windows.Forms.NumericUpDown numericBoundValue;
    }
}

