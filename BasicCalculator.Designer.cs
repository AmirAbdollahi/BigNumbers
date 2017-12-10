namespace BigNumbers
{
    partial class BasicCalculator
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
            this.label3 = new System.Windows.Forms.Label();
            this.btnEqual = new System.Windows.Forms.Button();
            this.cmbOperators = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBigNum2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBigNum1 = new System.Windows.Forms.TextBox();
            this.nudDecimalPlaces = new System.Windows.Forms.NumericUpDown();
            this.lblDecimalPlaces = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudDecimalPlaces)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Result:";
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(121, 155);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(75, 23);
            this.btnEqual.TabIndex = 13;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.Click += new System.EventHandler(this.btnEqual_Click);
            // 
            // cmbOperators
            // 
            this.cmbOperators.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperators.FormattingEnabled = true;
            this.cmbOperators.Items.AddRange(new object[] {
            "+",
            "-",
            "*",
            "/",
            "Mod"});
            this.cmbOperators.Location = new System.Drawing.Point(121, 68);
            this.cmbOperators.Name = "cmbOperators";
            this.cmbOperators.Size = new System.Drawing.Size(106, 24);
            this.cmbOperators.TabIndex = 12;
            this.cmbOperators.SelectedIndexChanged += new System.EventHandler(this.cmbOperators_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Big Number 2:";
            // 
            // txtBigNum2
            // 
            this.txtBigNum2.Location = new System.Drawing.Point(121, 113);
            this.txtBigNum2.Name = "txtBigNum2";
            this.txtBigNum2.Size = new System.Drawing.Size(1301, 22);
            this.txtBigNum2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Big Number 1:";
            // 
            // txtBigNum1
            // 
            this.txtBigNum1.Location = new System.Drawing.Point(121, 27);
            this.txtBigNum1.Name = "txtBigNum1";
            this.txtBigNum1.Size = new System.Drawing.Size(1301, 22);
            this.txtBigNum1.TabIndex = 8;
            // 
            // nudDecimalPlaces
            // 
            this.nudDecimalPlaces.Location = new System.Drawing.Point(481, 69);
            this.nudDecimalPlaces.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudDecimalPlaces.Name = "nudDecimalPlaces";
            this.nudDecimalPlaces.Size = new System.Drawing.Size(120, 22);
            this.nudDecimalPlaces.TabIndex = 16;
            this.nudDecimalPlaces.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudDecimalPlaces.Visible = false;
            // 
            // lblDecimalPlaces
            // 
            this.lblDecimalPlaces.AutoSize = true;
            this.lblDecimalPlaces.Location = new System.Drawing.Point(294, 71);
            this.lblDecimalPlaces.Name = "lblDecimalPlaces";
            this.lblDecimalPlaces.Size = new System.Drawing.Size(181, 17);
            this.lblDecimalPlaces.TabIndex = 17;
            this.lblDecimalPlaces.Text = "Number Of Decimal Places:";
            this.lblDecimalPlaces.Visible = false;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(121, 196);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(1301, 116);
            this.txtResult.TabIndex = 14;
            // 
            // BasicCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1434, 332);
            this.Controls.Add(this.lblDecimalPlaces);
            this.Controls.Add(this.nudDecimalPlaces);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnEqual);
            this.Controls.Add(this.cmbOperators);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBigNum2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBigNum1);
            this.Name = "BasicCalculator";
            this.Text = "BasicCalculator";
            ((System.ComponentModel.ISupportInitialize)(this.nudDecimalPlaces)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.ComboBox cmbOperators;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBigNum2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBigNum1;
        private System.Windows.Forms.NumericUpDown nudDecimalPlaces;
        private System.Windows.Forms.Label lblDecimalPlaces;
        private System.Windows.Forms.TextBox txtResult;
    }
}