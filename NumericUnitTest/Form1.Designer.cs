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
            this.numericWithUnit1 = new NumericUnit.NumericWithUnit();
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
            // numericWithUnit1
            // 
            this.numericWithUnit1.BackColor = System.Drawing.Color.LightGreen;
            this.numericWithUnit1.CorrectColor = System.Drawing.Color.LightGreen;
            this.numericWithUnit1.DecimalPlaces = 13;
            this.numericWithUnit1.DefaultColor = System.Drawing.Color.White;
            this.numericWithUnit1.DisplayFormat = null;
            this.numericWithUnit1.IncorrectColor = System.Drawing.Color.Red;
            this.numericWithUnit1.Location = new System.Drawing.Point(12, 12);
            this.numericWithUnit1.Maximum = 1D;
            this.numericWithUnit1.Minimum = 0D;
            this.numericWithUnit1.Name = "numericWithUnit1";
            this.numericWithUnit1.Size = new System.Drawing.Size(209, 20);
            this.numericWithUnit1.TabIndex = 0;
            this.numericWithUnit1.Value = 0D;
            this.numericWithUnit1.TextChanged += new System.EventHandler(this.numericWithUnit1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 196);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericWithUnit1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericUnit.NumericWithUnit numericWithUnit1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

