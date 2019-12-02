using System;
using System.Drawing;
//using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Labyrinth
{
	/// <summary>
	/// Summary description for DimensionsDialog.
	/// </summary>
	public class DimensionsDialog : System.Windows.Forms.Form
	{
        public System.Windows.Forms.Button OKButton;
		public System.Windows.Forms.Button CancelBtn;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DimensionsDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(15, 96);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(80, 24);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "Готово";
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(143, 97);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(80, 24);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "Отмена";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(39, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Число ячеек в строке:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(161, 40);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            35,
            0,
            0,
            0});
            // 
            // DimensionsDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(235, 133);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DimensionsDialog";
            this.Text = "Параметры лабиринта";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
	}
}
