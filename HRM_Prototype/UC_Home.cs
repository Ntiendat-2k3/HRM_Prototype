using System;
using System.Drawing;
using System.Windows.Forms;

namespace HRM_Prototype
{
    public partial class UC_Home : UserControl
    {
        public UC_Home()
        {
            InitializeComponent();
            this.BackColor = Color.White;

            Label lbl = new Label()
            {
                Text = "Chào mừng đến với\nHệ Thống Quản Lý Nhân Sự Thăng Long",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 51, 153),
                AutoSize = false, // Dock Fill căn giữa
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill // Tự động căn giữa toàn màn hình
            };

            this.Controls.Add(lbl);
        }
    }
}