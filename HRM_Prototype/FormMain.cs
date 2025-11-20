using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace HRM_Prototype
{
    public partial class FormMain : Form
    {
        // ui contrl
        private Panel pnlHeader, pnlBody, pnlSidebar, pnlContent, pnlDropdown;
        private PictureBox picAvatar;

        public FormMain()
        {
            InitializeComponent();
            SetupForm();
            SetupLayout();
            SetupDropdown(); // Menu đăng xuất

            ShowPage(new UC_Home());
            HighlightMenu("Trang chủ");

            // click ngoài đóng dropdown
            this.Click += HideDropdown;
            pnlContent.Click += HideDropdown;
        }

        private void SetupForm()
        {
            this.Text = "Hệ thống Quản lý Nhân sự";
            this.Size = new Size(1280, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void SetupLayout()
        {
            InitHeader();
            InitBodyAndSidebar();

            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlBody);
            pnlBody.BringToFront();
        }

        private void InitHeader()
        {
            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = Color.White };

            // line
            var pnlLine = new Panel { Dock = DockStyle.Bottom, Height = 2, BackColor = Color.WhiteSmoke };
            pnlLine.Controls.Add(new Panel { Dock = DockStyle.Top, Height = 1, BackColor = Color.LightGray });

            // header
            var pnlContent = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            // logo
            var picLogo = new PictureBox { Size = new Size(250, 80), Location = new Point(20, 5), SizeMode = PictureBoxSizeMode.Zoom };
            try { picLogo.Image = Image.FromFile("logo.png"); } catch { picLogo.BackColor = Color.FromArgb(190, 30, 45); }

            // user 
            var pnlUser = new Panel { Width = 250, Dock = DockStyle.Right, Padding = new Padding(0, 20, 20, 0) };

            picAvatar = new PictureBox { Size = new Size(50, 50), SizeMode = PictureBoxSizeMode.Zoom, Cursor = Cursors.Hand, Dock = DockStyle.Right };
            try { picAvatar.Image = Image.FromFile("avatar.png"); }
            catch { picAvatar.Paint += (s, e) => { e.Graphics.FillEllipse(Brushes.LightGray, 0, 0, 48, 48); e.Graphics.DrawString("A", new Font("Arial", 15, FontStyle.Bold), Brushes.White, 15, 10); }; }

            picAvatar.Click += (s, e) => { pnlDropdown.Visible = !pnlDropdown.Visible; pnlDropdown.BringToFront(); };

            var lblName = new Label { Text = "ADMIN", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.Gray, TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };

            pnlUser.Controls.Add(lblName);
            pnlUser.Controls.Add(picAvatar);
            pnlContent.Controls.Add(picLogo);
            pnlContent.Controls.Add(pnlUser);
            pnlHeader.Controls.Add(pnlContent);
            pnlHeader.Controls.Add(pnlLine); pnlLine.SendToBack();
        }

        private void InitBodyAndSidebar()
        {
            pnlBody = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            // Sidebar
            pnlSidebar = new Panel { Dock = DockStyle.Left, Width = 250, BackColor = Color.White };
            pnlSidebar.Controls.Add(new Panel { Dock = DockStyle.Right, Width = 1, BackColor = Color.LightGray }); // Kẻ dọc

            var btnHome = CreateMenuBtn("🏠   Trang chủ", 30);
            var btnReport = CreateMenuBtn("📊   Báo cáo công ty", 80);

            btnHome.Click += (s, e) => { SetActive(btnHome); ShowPage(new UC_Home()); };
            btnReport.Click += (s, e) => { SetActive(btnReport); ShowPage(new UC_Report()); };

            pnlSidebar.Controls.Add(btnHome);
            pnlSidebar.Controls.Add(btnReport);

            // Content
            pnlContent = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(244, 246, 249), AutoScroll = true };

            // Body
            pnlBody.Controls.Add(pnlContent);
            pnlBody.Controls.Add(pnlSidebar);
            pnlSidebar.BringToFront();
            pnlContent.BringToFront();
        }

        private void SetupDropdown()
        {
            pnlDropdown = new Panel { Size = new Size(150, 45), BackColor = Color.White, Padding = new Padding(1), Visible = false };
            pnlDropdown.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, pnlDropdown.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
            pnlDropdown.Location = new Point(this.ClientSize.Width - 170, 85);
            pnlDropdown.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            var btnLogout = new Button { Text = "🚪 Đăng xuất", Dock = DockStyle.Fill, FlatStyle = FlatStyle.Flat, BackColor = Color.White, ForeColor = Color.Red, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) => {
                pnlDropdown.Visible = false;
                if (MessageBox.Show("Đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) { this.Hide(); new FormLogin().Show(); }
            };

            pnlDropdown.Controls.Add(btnLogout);
            this.Controls.Add(pnlDropdown);
            pnlDropdown.BringToFront();
        }

        private Button CreateMenuBtn(string text, int top)
        {
            return new Button { Text = text, Location = new Point(0, top), Size = new Size(249, 45), FlatStyle = FlatStyle.Flat, BackColor = Color.White, ForeColor = Color.FromArgb(64, 64, 64), Font = new Font("Segoe UI", 10), TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(25, 0, 0, 0), Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 } };
        }

        private void SetActive(Button btn)
        {
            foreach (Control c in pnlSidebar.Controls) if (c is Button) { c.BackColor = Color.White; c.ForeColor = Color.FromArgb(64, 64, 64); }
            btn.BackColor = Color.FromArgb(235, 242, 252); btn.ForeColor = Color.FromArgb(0, 86, 179);
        }

        private void ShowPage(UserControl page) { pnlContent.Controls.Clear(); page.Dock = DockStyle.Fill; pnlContent.Controls.Add(page); }

        private void HighlightMenu(string text)
        {
            foreach (Control c in pnlSidebar.Controls) if (c is Button btn && btn.Text.Contains(text)) { SetActive(btn); break; }
        }

        private void HideDropdown(object sender, EventArgs e) => pnlDropdown.Visible = false;
    }
}