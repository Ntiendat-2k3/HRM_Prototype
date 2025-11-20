using System;
using System.Drawing;
using System.Windows.Forms;

namespace HRM_Prototype
{
    public partial class UC_Report : UserControl
    {
        public UC_Report()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.BackColor = Color.FromArgb(244, 246, 249);

            Label lblMessage = new Label()
            {
                Text = "Đây là trang Báo Cáo Công Ty",

                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(64, 64, 64),

                AutoSize = false, 
                Dock = DockStyle.Fill, 
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.Controls.Add(lblMessage);
        }
    }
}