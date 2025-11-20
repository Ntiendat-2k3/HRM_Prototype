using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace HRM_Prototype
{
    public partial class FormRegister : Form
    {
        // const color
        private readonly Color PRIMARY_COLOR = Color.FromArgb(190, 30, 45);
        private readonly Color TEXT_COLOR = Color.FromArgb(64, 64, 64);
        private readonly Font FONT_TITLE = new Font("Segoe UI", 16, FontStyle.Bold);
        private readonly Font FONT_LABEL = new Font("Segoe UI", 10);
        private readonly Font FONT_INPUT = new Font("Segoe UI", 11);

        public FormRegister()
        {
            InitializeComponent();
            SetupForm();
            SetupControls();
        }

        private void SetupForm()
        {
            this.Text = "Đăng Ký Tài Khoản";
            this.Size = new Size(400, 520);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void SetupControls()
        {
            var picLogo = CreateLogo();

            var lblTitle = CreateLabel("TẠO TÀI KHOẢN MỚI", FONT_TITLE, PRIMARY_COLOR, 110);
            // căn giữa
            lblTitle.Location = new Point((this.ClientSize.Width - lblTitle.PreferredWidth) / 2, 110);

            int startY = 160;
            int spacing = 70;

            var lblName = CreateLabel("Họ Tên:", FONT_LABEL, TEXT_COLOR, startY);
            var txtName = CreateTextBox(startY + 25);

            var lblEmail = CreateLabel("Email:", FONT_LABEL, TEXT_COLOR, startY + spacing);
            var txtEmail = CreateTextBox(startY + spacing + 25);

            var lblPass = CreateLabel("Mật khẩu:", FONT_LABEL, TEXT_COLOR, startY + spacing * 2);
            var txtPass = CreateTextBox(startY + spacing * 2 + 25, isPassword: true);

            var btnConfirm = CreateButton("XÁC NHẬN ĐĂNG KÝ", 400, PRIMARY_COLOR, Color.White);
            btnConfirm.Click += (s, e) => {
                MessageBox.Show("Đăng ký thành công! Vui lòng đăng nhập lại.", "Thông báo");
                this.Close();
            };

            this.Controls.AddRange(new Control[] { picLogo, lblTitle, lblName, txtName, lblEmail, txtEmail, lblPass, txtPass, btnConfirm });
        }

        // create ui
        private PictureBox CreateLogo()
        {
            var pic = new PictureBox { Size = new Size(80, 80), SizeMode = PictureBoxSizeMode.Zoom };
            pic.Location = new Point((this.ClientSize.Width - 80) / 2, 20);
            try { pic.Image = Image.FromFile("logo.png"); } catch { pic.BackColor = PRIMARY_COLOR; }
            return pic;
        }

        private Label CreateLabel(string text, Font font, Color color, int top, int left = 40)
        {
            return new Label { Text = text, Location = new Point(left, top), Font = font, ForeColor = color, AutoSize = true };
        }

        private TextBox CreateTextBox(int top, bool isPassword = false)
        {
            return new TextBox { Location = new Point(40, top), Width = 300, Font = FONT_INPUT, BorderStyle = BorderStyle.FixedSingle, PasswordChar = isPassword ? '●' : '\0' };
        }

        private Button CreateButton(string text, int top, Color backColor, Color foreColor)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(40, top),
                Size = new Size(300, 45),
                BackColor = backColor,
                ForeColor = foreColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
    }
}