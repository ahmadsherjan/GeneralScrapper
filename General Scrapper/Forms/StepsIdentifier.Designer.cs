namespace General_Scrapper.Forms
{
    partial class StepsIdentifier
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.comboValueTypes = new System.Windows.Forms.ComboBox();
            this.lblValueType = new System.Windows.Forms.Label();
            this.comboOperationType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtText = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboSelectorType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(392, 376);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(91, 43);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // comboValueTypes
            // 
            this.comboValueTypes.FormattingEnabled = true;
            this.comboValueTypes.Location = new System.Drawing.Point(125, 66);
            this.comboValueTypes.Name = "comboValueTypes";
            this.comboValueTypes.Size = new System.Drawing.Size(284, 21);
            this.comboValueTypes.TabIndex = 1;
            this.comboValueTypes.SelectedIndexChanged += new System.EventHandler(this.comboValueTypes_SelectedIndexChanged);
            // 
            // lblValueType
            // 
            this.lblValueType.AutoSize = true;
            this.lblValueType.Location = new System.Drawing.Point(12, 69);
            this.lblValueType.Name = "lblValueType";
            this.lblValueType.Size = new System.Drawing.Size(98, 13);
            this.lblValueType.TabIndex = 2;
            this.lblValueType.Text = "Operation Category";
            // 
            // comboOperationType
            // 
            this.comboOperationType.FormattingEnabled = true;
            this.comboOperationType.Location = new System.Drawing.Point(125, 119);
            this.comboOperationType.Name = "comboOperationType";
            this.comboOperationType.Size = new System.Drawing.Size(284, 21);
            this.comboOperationType.TabIndex = 3;
            this.comboOperationType.SelectedIndexChanged += new System.EventHandler(this.comboOperationType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Operation Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Operation Values";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 292);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Text";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(126, 269);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(284, 20);
            this.txtValue.TabIndex = 9;
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(126, 320);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(284, 20);
            this.txtText.TabIndex = 10;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(503, 376);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(91, 43);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(416, 320);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(54, 20);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Selector Type";
            // 
            // comboSelectorType
            // 
            this.comboSelectorType.FormattingEnabled = true;
            this.comboSelectorType.Location = new System.Drawing.Point(125, 169);
            this.comboSelectorType.Name = "comboSelectorType";
            this.comboSelectorType.Size = new System.Drawing.Size(284, 21);
            this.comboSelectorType.TabIndex = 13;
            this.comboSelectorType.SelectedIndexChanged += new System.EventHandler(this.comboSelectorType_SelectedIndexChanged);
            // 
            // StepsIdentifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 443);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboSelectorType);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboOperationType);
            this.Controls.Add(this.lblValueType);
            this.Controls.Add(this.comboValueTypes);
            this.Controls.Add(this.btnSubmit);
            this.Name = "StepsIdentifier";
            this.Text = "Steps Identifier";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox comboValueTypes;
        private System.Windows.Forms.Label lblValueType;
        private System.Windows.Forms.ComboBox comboOperationType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboSelectorType;
    }
}