using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HRM_Prototype
{
    public partial class UC_Report : UserControl
    {
        // setup color
        private Color clrBackground = Color.FromArgb(244, 246, 249);
        private Color clrBlue = Color.FromArgb(13, 110, 253);
        private Color clrGreen = Color.FromArgb(25, 135, 84);
        private Color clrOrange = Color.FromArgb(253, 126, 20);

        public UC_Report()
        {
            InitializeComponent();
            this.BackColor = clrBackground;
            this.Load += (s, e) => SetupLayout();
        }

        private void SetupLayout()
        {
            this.Controls.Clear();

            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 1;
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));  // Header
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F)); // Cards
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));  // Charts

            mainLayout.Controls.Add(CreateHeaderPanel(), 0, 0);
            mainLayout.Controls.Add(CreateCardsPanel(), 0, 1);
            mainLayout.Controls.Add(CreateChartsPanel(), 0, 2);

            this.Controls.Add(mainLayout);
        }

        // header
        private Panel CreateHeaderPanel()
        {
            Panel pnl = new Panel() { Dock = DockStyle.Fill, Padding = new Padding(10) };

            Label lbl = new Label()
            {
                Text = "Báo cáo công ty",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Dock = DockStyle.Left
            };

            Button btn = new Button()
            {
                Text = "📥 Tải báo cáo",
                BackColor = clrBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 35),
                Dock = DockStyle.Right,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;

            pnl.Controls.Add(btn);
            pnl.Controls.Add(lbl);
            return pnl;
        }

        // cards
        private TableLayoutPanel CreateCardsPanel()
        {
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 3;
            tlp.RowCount = 1;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlp.Padding = new Padding(0, 0, 0, 10); 

            tlp.Controls.Add(CreateSingleCard("TỔNG THU NHẬP (THÁNG)", "250.000.000 ₫", clrBlue, "📅"), 0, 0);
            tlp.Controls.Add(CreateSingleCard("TỔNG THU NHẬP (NĂM)", "3.2 Tỷ", clrGreen, "💲"), 1, 0);
            tlp.Controls.Add(CreateSingleCard("PHẢN HỒI KHÁCH HÀNG", "98%", clrOrange, "💬"), 2, 0);

            return tlp;
        }

        private Panel CreateSingleCard(string title, string value, Color color, string icon)
        {
            Panel container = new Panel() { Dock = DockStyle.Fill, Padding = new Padding(5) };

            Panel content = new Panel() { Dock = DockStyle.Fill, BackColor = Color.White };

            Panel bar = new Panel() { Width = 5, Dock = DockStyle.Left, BackColor = color };
            Label t = new Label() { Text = title, Location = new Point(15, 15), Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.Gray, AutoSize = true };
            Label v = new Label() { Text = value, Location = new Point(15, 40), Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true };
            Label i = new Label() { Text = icon, Font = new Font("Segoe UI", 20), Dock = DockStyle.Right, AutoSize = true, TextAlign = ContentAlignment.TopRight };

            content.Controls.Add(t);
            content.Controls.Add(v);
            content.Controls.Add(i);
            content.Controls.Add(bar);

            container.Controls.Add(content);
            return container;
        }

        // charts
        private TableLayoutPanel CreateChartsPanel()
        {
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Fill;
            tlp.ColumnCount = 2;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));

            Panel pnlLine = CreateChartWrapper("BIỂU ĐỒ TÀI CHÍNH", CreateLineChart());
            Panel pnlPie = CreateChartWrapper("TỔNG CHI TIÊU", CreateDonutChart());

            tlp.Controls.Add(pnlLine, 0, 0);
            tlp.Controls.Add(pnlPie, 1, 0);

            return tlp;
        }

        private Panel CreateChartWrapper(string title, Chart chart)
        {
            Panel container = new Panel() { Dock = DockStyle.Fill, Padding = new Padding(5) };
            Panel content = new Panel() { Dock = DockStyle.Fill, BackColor = Color.White };

            Label lbl = new Label() { Text = title, Dock = DockStyle.Top, Height = 30, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = clrBlue, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(10, 0, 0, 0) };
            Panel line = new Panel() { Dock = DockStyle.Top, Height = 1, BackColor = Color.LightGray };

            chart.Dock = DockStyle.Fill;

            content.Controls.Add(chart);
            content.Controls.Add(line);
            content.Controls.Add(lbl);
            container.Controls.Add(content);
            return container;
        }

        // logic chart
        private Chart CreateLineChart()
        {
            Chart chart = new Chart();
            ChartArea area = new ChartArea();
            area.AxisX.MajorGrid.LineColor = Color.WhiteSmoke;
            area.AxisY.MajorGrid.LineColor = Color.WhiteSmoke;
            area.BorderColor = Color.White;
            chart.ChartAreas.Add(area);

            Series s = new Series { ChartType = SeriesChartType.Spline, BorderWidth = 3, Color = clrBlue, IsVisibleInLegend = false };
            s.Points.AddXY("Jan", 10); s.Points.AddXY("Feb", 40); s.Points.AddXY("Mar", 20);
            s.Points.AddXY("Apr", 50); s.Points.AddXY("May", 30); s.Points.AddXY("Jun", 70);
            chart.Series.Add(s);
            return chart;
        }

        private Chart CreateDonutChart()
        {
            Chart chart = new Chart();
            ChartArea area = new ChartArea();
            chart.ChartAreas.Add(area);

            Series s = new Series { ChartType = SeriesChartType.Doughnut, IsValueShownAsLabel = false };
            s.Points.AddXY("DỰ ÁN", 50);
            s.Points.AddXY("MKT", 30);
            s.Points.AddXY("KHÁC", 20);
            s.Points[0].Color = clrBlue; s.Points[1].Color = clrGreen; s.Points[2].Color = Color.Turquoise;

            Legend l = new Legend() { Docking = Docking.Bottom, Alignment = StringAlignment.Center };
            chart.Legends.Add(l);

            chart.Series.Add(s);
            return chart;
        }
    }
}