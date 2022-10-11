namespace Game_Of_Life
{
    partial class ErrorModal
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
            this.okButton = new System.Windows.Forms.Button();
            this.ExistingTemplateLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(90, 51);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // ExistingTemplateLabel
            // 
            this.ExistingTemplateLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ExistingTemplateLabel.AutoSize = true;
            this.ExistingTemplateLabel.Location = new System.Drawing.Point(34, 9);
            this.ExistingTemplateLabel.Name = "ExistingTemplateLabel";
            this.ExistingTemplateLabel.Size = new System.Drawing.Size(212, 26);
            this.ExistingTemplateLabel.TabIndex = 1;
            this.ExistingTemplateLabel.Text = "There is already a template with that name, \r\nplease enter a different name";
            this.ExistingTemplateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ErrorModal
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.okButton;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(264, 86);
            this.ControlBox = false;
            this.Controls.Add(this.ExistingTemplateLabel);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Error";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label ExistingTemplateLabel;
    }
}