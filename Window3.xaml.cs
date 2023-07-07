using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace ShoeShop
{
    public partial class Window3 : Window
    {
        private SqlConnection con;
        private ObservableCollection<ZakupionyBut> zakupioneButy = new ObservableCollection<ZakupionyBut>();

        public Window3()
        {
            InitializeComponent();
            string FileName = "Buty.mdf";
            string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
            string ProjectDirectory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(CurrentDirectory).FullName).FullName).FullName;
            string FilePath = System.IO.Path.Combine(ProjectDirectory, FileName);
            string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={FilePath};Integrated Security=True;Connect Timeout=30;";
            con = new SqlConnection(conn);
            con.Open();
            LoadButyData();

            zakupyListView.ItemsSource = zakupioneButy;
        }

        private void LoadButyData()
        {
            SqlCommand cmd = new SqlCommand("SELECT Nazwa, Producent, NazwaSerii, Rozmiar, Cena FROM Buty", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string nazwa = reader.GetString(0);
                string producent = reader.GetString(1);
                string nazwaSerii = reader.GetString(2);
                double rozmiarDouble = reader.GetDouble(3);
                int rozmiar = Convert.ToInt32(rozmiarDouble);
                decimal cena = reader.GetDecimal(4);
                Buty buty = new Buty(nazwa, producent, nazwaSerii, rozmiar, cena);
                butyListView.Items.Add(buty);
            }
            reader.Close();
        }

        private void KupButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Buty selectedButy = (Buty)button.DataContext;
            MessageBox.Show($"Zakupiono buty: {selectedButy.Nazwa}");

            ZakupionyBut zakupionyBut = new ZakupionyBut
            {
                Nazwa = selectedButy.Nazwa,
                Producent = selectedButy.Producent,
                NazwaSerii = selectedButy.NazwaSerii,
                Rozmiar = selectedButy.Rozmiar,
                Cena = selectedButy.Cena
            };

            zakupioneButy.Add(zakupionyBut);
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow u3 = new MainWindow();
            this.Close();
            u3.Show();
        }

        private void Window3_Closed(object sender, EventArgs e)
        {
            con.Close();
        }
    }

    public class Buty
    {
        public string Nazwa { get; set; }
        public string Producent { get; set; }
        public string NazwaSerii { get; set; }
        public int Rozmiar { get; set; }
        public decimal Cena { get; set; }

        public Buty(string nazwa, string producent, string nazwaSerii, int rozmiar, decimal cena)
        {
            Nazwa = nazwa;
            Producent = producent;
            NazwaSerii = nazwaSerii;
            Rozmiar = rozmiar;
            Cena = cena;
        }
    }

    public class ZakupionyBut
    {
        public string Nazwa { get; set; }
        public string Producent { get; set; }
        public string NazwaSerii { get; set; }
        public int Rozmiar { get; set; }
        public decimal Cena { get; set; }
    }
}
