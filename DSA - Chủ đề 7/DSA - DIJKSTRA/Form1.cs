using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DijkstraTest2
{
     
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public double Tinh_KC(string a, string b)
        {
            Point p1 = Locations_1[g.Lay_chi_so_dinh(a)].getPoint();
            Point p2 = Locations_1[g.Lay_chi_so_dinh(b)].getPoint();
            int xDiff = p1.X - p2.X;
            int yDiff = p1.Y - p2.Y;
            return Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2)) ;
        }

        SetUpGraph g = new SetUpGraph();
        public List<Location> Locations_1 = new List<Location>();
        string[,] Dist;
        private void Form1_Load(object creator, EventArgs e) //Goi ten cac dia diem va set vi tri
        {
            Location Q1 = new Location("Quận 1", "A", 336, 272);
            Location Q5 = new Location("Quận 5", "B", 288, 309);
            Location Q7 = new Location("Quận 7", "C", 391, 361);
            Location Q9 = new Location("Quận 9", "D", 573, 183);
            Location Q12= new Location("Quận 12", "E", 274, 89);
            Location TB = new Location("Tân Bình", "F", 249, 209);
            Location BTH= new Location("Bình Thạnh", "G", 351, 205);
            Location TD = new Location("Thủ Đức", "H", 432, 106);
            Location BT = new Location("Bình Tân", "I", 141, 280);

            string[,] Dist =
            {
                {"Thủ Đức","Quận 9"},
                {"Thủ Đức","Quận 12"},
                {"Thủ Đức","Bình Thạnh"},
                {"Quận 12","Tân Bình"},
                {"Quận 12","Bình Tân"},
                {"Tân Bình","Quận 1"},
                {"Tân Bình","Bình Thạnh"},
                {"Tân Bình","Bình Tân"},
                {"Quận 5","Quận 7"},
                {"Quận 5","Quận 1"},
                {"Quận 5","Bình Tân"},
                {"Quận 7","Quận 1"},
                {"Quận 7","Quận 9"},
                {"Quận 9","Bình Thạnh"},
                {"Quận 1","Bình Thạnh"},
            };

            Locations_1.Add(Q1);
            Locations_1.Add(Q5);
            Locations_1.Add(Q7);
            Locations_1.Add(Q9);
            Locations_1.Add(Q12);
            Locations_1.Add(TB);
            Locations_1.Add(BTH);
            Locations_1.Add(TD);
            Locations_1.Add(BT);

            DATA_DI.Items.Add("Quận 1");
            DATA_DI.Items.Add("Quận 5");
            DATA_DI.Items.Add("Quận 7");
            DATA_DI.Items.Add("Quận 9");
            DATA_DI.Items.Add("Quận 12");
            DATA_DI.Items.Add("Tân Bình");
            DATA_DI.Items.Add("Bình Thạnh");
            DATA_DI.Items.Add("Thủ Đức");
            DATA_DI.Items.Add("Bình Tân");

            DATA_DEN.Items.Add("Quận 1");
            DATA_DEN.Items.Add("Quận 5");
            DATA_DEN.Items.Add("Quận 7");
            DATA_DEN.Items.Add("Quận 9");
            DATA_DEN.Items.Add("Quận 12");
            DATA_DEN.Items.Add("Tân Bình");
            DATA_DEN.Items.Add("Bình Thạnh");
            DATA_DEN.Items.Add("Thủ Đức");
            DATA_DEN.Items.Add("Bình Tân");
 
            Graphics graph = Map.CreateGraphics();

            for (int i = 0; i < Locations_1.Count; i++)
            {
                g.listPoint.Add(Locations_1[i].getPoint());
                g.Lay_dinh(Locations_1[i].getName());
            }
            for (int i = 0; i < Dist.GetLength(0); i++)
            {
                double D = Math.Round(Tinh_KC(Dist[i, 0], Dist[i, 1])/13.3,2);
                g.Them_KC_2_quan(Dist[i, 0], Dist[i, 1], D);
            }
            DrawLine();
        }
        //Vẽ bản đồ ra Panel
        private void Map_Paint(object creator, PaintEventArgs e)
        {
            Graphics graph = Map.CreateGraphics();
            for (int i = 0; i < Locations_1.Count; i++)
            {
                SolidBrush brush = new SolidBrush(Color.SeaGreen);
                Brush pointName = new SolidBrush(Color.White);
                graph.FillEllipse(brush, Locations_1[i].getPoint().X - 3, Locations_1[i].getPoint().Y - 2, 18, 18);
                graph.DrawString(Locations_1[i].getPointName(), new Font("Arial", 8), pointName, Locations_1[i].getPoint().X, Locations_1[i].getPoint().Y);
            }
            DrawLine();
        }

        private void DrawLine() // Nối các tuyến đường 
        {
            DrawLine("Thủ Đức", "Quận 9");
            DrawLine("Thủ Đức", "Quận 12");
            DrawLine("Thủ Đức", "Bình Thạnh");
            DrawLine("Quận 12", "Tân Bình");
            DrawLine("Quận 12", "Bình Tân");
            DrawLine("Tân Bình", "Quận 1");
            DrawLine("Tân Bình", "Bình Thạnh");
            DrawLine("Tân Bình", "Bình Tân");
            DrawLine("Quận 5", "Quận 1");
            DrawLine("Quận 5", "Quận 7");
            DrawLine("Quận 5", "Bình Tân");
            DrawLine("Quận 7", "Quận 1");
            DrawLine("Quận 7", "Quận 9");
            DrawLine("Quận 9", "Bình Thạnh");
            DrawLine("Quận 1", "Bình Thạnh");
        }
        private void DrawLine(string a, string b)
        {
            Graphics graph = Map.CreateGraphics();
            int x = g.Lay_chi_so_dinh(a);
            int y = g.Lay_chi_so_dinh(b);
            Pen p = new Pen(Color.Black, 2);
            Point p1 = new Point(g.listPoint[x].X, g.listPoint[x].Y);
            Point p2 = new Point(g.listPoint[y].X, g.listPoint[y].Y);
            graph.DrawLine(p, p1, p2);
            graph.DrawString($"{g.adj[x, y]}", new Font("Fira Code", 10), Brushes.Black, new Point((p1.X + p2.X) / 2 - 8, (p1.Y + p2.Y) / 2 + 8));
        }
        private void Nguon_moi_tu_chi_so_dinh(object creator, EventArgs e) // Hàm chọn điểm đi
        {
            if (DATA_DI.SelectedIndex != -1 && DATA_DEN.SelectedIndex != -1)
            {
                Map.Controls.Clear();
                Map.Refresh();
                DrawLine();
                g.pathIndex.Clear();
                Tong_KC.Clear();
                Duong_di.Clear();
                g.Tim_duong_di(DATA_DI.SelectedItem.ToString(), DATA_DEN.SelectedIndex.ToString(), Tong_KC, Duong_di);
                for (int i = 0; i < g.pathIndex.Count - 1; i++)
                {
                    DrawPathLine(i);
                }
            }
            if (DATA_DI.SelectedIndex == DATA_DEN.SelectedIndex)
            {
                MessageBox.Show("Địa điểm đi và đến không thể trùng nhau");
            }
        }
        private void Dich_moi_tu_chi_so_dinh(object creator, EventArgs e) // Hàm chọn điểm đến
        {
            if (DATA_DI.SelectedIndex != -1 && DATA_DEN.SelectedIndex != -1)
            {
                Map.Controls.Clear();
                Map.Refresh();
                DrawLine();
                g.pathIndex.Clear();
                Tong_KC.Clear();
                Duong_di.Clear();
                g.Tim_duong_di(DATA_DI.SelectedItem.ToString(), DATA_DEN.SelectedIndex.ToString(), Tong_KC, Duong_di);
                for (int i = 0; i < g.pathIndex.Count - 1; i++)
                {
                    DrawPathLine(i);
                }
            }
            if (DATA_DI.SelectedIndex == DATA_DEN.SelectedIndex)
            {
                MessageBox.Show("Địa điểm đi và đến không thể trùng nhau");
            }
        }
        //Vẽ lại đường đi ngắn nhất
        private void DrawPathLine(int i)
        {
            Graphics graph = Map.CreateGraphics();
            Pen p = new Pen(Color.Aqua,3);
            Point p1 = new Point(g.pathIndex[i].X, g.pathIndex[i].Y);
            Point p2 = new Point(g.pathIndex[i + 1].X, g.pathIndex[i + 1].Y);
            graph.DrawLine(p, p1, p2);
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Map_Click(object sender, EventArgs e)
        {
            DrawLine();
        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lable12_Click(object sender, EventArgs e)
        {

        }

        private void Tong_KC_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
