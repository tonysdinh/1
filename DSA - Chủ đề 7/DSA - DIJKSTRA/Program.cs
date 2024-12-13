using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DijkstraTest2
{
    public class Location
    {
        
        private string nameLocation { get; set; }
        private string pointName { get; set; }
        private Point pointLocation { get; set; }

        public Location(string dia_diem, string ky_hieu, int x, int y)
        {
            nameLocation = dia_diem;
            pointName = ky_hieu;
            Point p = new Point(x, y);
            pointLocation = p;
        }
        public string getName()
        {
            return nameLocation;
        }
        public string getPointName()
        {
            return pointName;
        }
        public Point getPoint()
        {
            return pointLocation;
        }
    }

    public class Vertex
    {
        public String name;
        public int status;
        public int diem_truoc;
        public int pathLength;
        public Vertex(String ten_dinh)
        {
            this.name = ten_dinh;
        }
    }

    class SetUpGraph
    {
        public readonly int MAX_VERTICES = 100; // đỉnh tối đa trong đồ thị
        public int n = 0; // đỉnh hiện tại
        int e ;
        public double[,] adj; // ma trận kề lưu KC các đỉnh
        public Vertex[] vertexList; 
        private readonly int INFINITY = 9999999; 
        private readonly int PERMANENT = 2;
        private readonly int TEMPORARY = 1;
        private readonly int NIL = -1;
        public List<Point> listPoint = new List<Point>(); // lưu trữ vị trí đỉnh
        public List<Point> pathIndex = new List<Point>(); // lưu trữ vị trí đường đi 
        
        public SetUpGraph()
        {
            adj = new double[MAX_VERTICES, MAX_VERTICES];
            vertexList = new Vertex[MAX_VERTICES];
        }

        private void Dijkstra(int s) // gọi s là đỉnh nguồn
        {
            int v, c;
            for (v = 0; v < n; v++)
            {
                vertexList[v].status = TEMPORARY; // đặt trạng thái = 1
                vertexList[v].pathLength = INFINITY; // KC các đỉnh = inf
                vertexList[v].diem_truoc = NIL; // các đỉnh đã đi qua = -1 (null)
            }
            vertexList[s].pathLength = 0; // đặt đỉnh nguồn = 0
            while (true)
            {
                c = KC_min_toi_dinh_nguon_s(); // đặt c (đỉnh chưa được truy vấn có KC min đến s) = - 1
                if (c == NIL)
                    return;
                vertexList[c].status = PERMANENT; // đặt trạng thái c = 2
                for (v = 0; v < n; v++)
                {
                    if (Hai_dinh_ke(c, v) && vertexList[v].status == TEMPORARY)
                    {// Đk KC từ s đến đỉnh kề thông qua c ngắn hơn KC hiện tại, cập nhật KC và dinh_truoc
                        if (vertexList[c].pathLength + adj[c, v] < vertexList[v].pathLength)
                        {
                            vertexList[v].diem_truoc = c;
                            vertexList[v].pathLength = (int)(vertexList[c].pathLength + adj[c, v]);
                        }
                    }
                }
            }
        }
        
        public void Them_KC_2_quan(string v1, string v2, double D) // duyệt khoảng cách giữa 2 đỉnh, cụ thể là 2 quận bất kì có liên kết kề
        {
            int i = Lay_chi_so_dinh(v1);
            int j = Lay_chi_so_dinh(v2);
            adj[i, j] = D;
            adj[j, i] = D;
        }

        public void Tim_duong_di(string nguon, string dich,TextBox Tong_KC, TextBox Duong_di) // Tìm đường đi từ s đến v
        {
            int s = Lay_chi_so_dinh(nguon);
            Dijkstra(s);

            int v = Convert.ToInt32(dich);
            {
                if (v != s)
                {
                    if (vertexList[v].pathLength == INFINITY)
                    {
                        Duong_di.Text += "Không có đường đi";
                    }
                    else
                    {
                        Tim_duong(s, v,Tong_KC, Duong_di);
                    }
                }
            }
        }

        public void Tim_duong(int s, int v, TextBox Tong_KC, TextBox Duong_di)
        {
            int i, u;
            int[] path = new int[n];
            double km = 0;
            int count = 0;
            while (v != s)
            {
                count++;
                path[count] = v;
                u = vertexList[v].diem_truoc;
                km += adj[u, v];
                v = u;
            }
            double sl = km * 0.09;
            double sd = km * 2043;
            count++;
            if (count >= n)
            {
                MessageBox.Show("Lỗi");

            }
            path[count] = s;
            for (i = count; i >= 1; i--)
            {
                pathIndex.Add(listPoint[path[i]]);
                if (Duong_di.Text == "")
                {
                    Duong_di.Text += vertexList[path[i]].name;
                }
                else
                {
                    Duong_di.Text += " -> " + vertexList[path[i]].name;
                }
            }
            Tong_KC.Text = $"{km} KM";
       
        }

        public int Lay_chi_so_dinh(string s) // tìm kiếm và trả về chỉ số của một đỉnh
        {
            for (int i = 0; i < n; i++)
            {
                if (s.Equals(vertexList[i].name))
                    return i;
            }
            throw new System.InvalidOperationException("Lỗi");
        }

        public void Lay_dinh(string name)
        {
            vertexList[n++] = new Vertex(name);
        }
        private bool Hai_dinh_ke(int u, int v) // truy cập ma trận kề adj và kiểm tra tính kề tại toạ độ [u,v], kiểm tra 2 quận có đường đi liên kết hay ko
        {
            return adj[u, v] != 0;
        }

        private int KC_min_toi_dinh_nguon_s() // tìm và trả về chỉ số của đỉnh tạm thời có khoảng cách ngắn nhất từ đỉnh nguồn
        {
            int min = INFINITY;
            int x = NIL;
            for (int v = 0; v < n; v++)
            {
                if (vertexList[v].status == TEMPORARY && vertexList[v].pathLength < min)
                {
                    min = vertexList[v].pathLength;
                    x = v;
                }
            }
            return x; // kết quả là trả về chỉ số đỉnh có đường đi ngắn nhất
        }
        
    }

    internal class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
