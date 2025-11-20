using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace HRM_Prototype
{
    public partial class FormLogin : Form
    {
        private TextBox txtUser;
        private TextBox txtPass;

        private readonly Color PRIMARY_COLOR = Color.FromArgb(190, 30, 45);
        private readonly Color TEXT_COLOR = Color.FromArgb(64, 64, 64);
        private readonly Font FONT_LABEL = new Font("Segoe UI", 10);
        private readonly Font FONT_INPUT = new Font("Segoe UI", 11);

        public FormLogin()
        {
            InitializeComponent();
            SetupForm();
            SetupControls();
        }

        private void SetupForm()
        {
            this.Text = "Đăng Nhập Hệ Thống";
            this.Size = new Size(400, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void SetupControls()
        {
            var picLogo = CreateLogo();
            var lblTitle = CreateTitle("ĐĂNG NHẬP");

            var lblUser = CreateLabel("Tài khoản:", 160);
            txtUser = CreateTextBox(185);

            var lblPass = CreateLabel("Mật khẩu:", 225);
            txtPass = CreateTextBox(250, isPassword: true);

            var btnLogin = CreateButton("Đăng Nhập", 310, PRIMARY_COLOR, Color.White, true);
            btnLogin.Click += BtnLogin_Click;

            var btnRegister = CreateButton("Chưa có tài khoản? Đăng ký ngay", 360, Color.White, PRIMARY_COLOR, false);
            btnRegister.Font = new Font("Segoe UI", 9, FontStyle.Underline);
            btnRegister.Click += (s, e) => new FormRegister().ShowDialog();

            this.Controls.AddRange(new Control[] { picLogo, lblTitle, lblUser, txtUser, lblPass, txtPass, btnLogin, btnRegister });
        }


        private PictureBox CreateLogo()
        {
            var pic = new PictureBox { Size = new Size(80, 80), SizeMode = PictureBoxSizeMode.Zoom };
            pic.Location = new Point((this.ClientSize.Width - 80) / 2, 20);
            try { pic.Image = Image.FromFile("logo.png"); } catch { pic.BackColor = PRIMARY_COLOR; }
            return pic;
        }

        private Label CreateTitle(string text)
        {
            var lbl = new Label { Text = text, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = PRIMARY_COLOR, AutoSize = true };
            lbl.Location = new Point((this.ClientSize.Width - lbl.PreferredWidth) / 2, 110);
            return lbl;
        }

        private Label CreateLabel(string text, int top)
        {
            return new Label { Text = text, Location = new Point(50, top), Font = FONT_LABEL, ForeColor = TEXT_COLOR, AutoSize = true };
        }

        private TextBox CreateTextBox(int top, bool isPassword = false)
        {
            return new TextBox { Location = new Point(50, top), Width = 280, Font = FONT_INPUT, BorderStyle = BorderStyle.FixedSingle, PasswordChar = isPassword ? '●' : '\0' };
        }

        private Button CreateButton(string text, int top, Color back, Color fore, bool isBold)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(50, top),
                Size = new Size(280, isBold ? 40 : 30),
                BackColor = back,
                ForeColor = fore,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            if (isBold) btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if ((txtUser.Text == "admin" && txtPass.Text == "123") || (txtUser.Text == "nv" && txtPass.Text == "123"))
            {
                this.Hide();
                new FormMain().Show();
            }
            else MessageBox.Show("Thông tin không chính xác!\nHint: admin / 123", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}