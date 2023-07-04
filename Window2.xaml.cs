using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace ShoeShop
{
    /// <summary>
    /// Logika interakcji dla klasy Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            //połączenie z bazą
            InitializeComponent();
            string FileName = "Buty.mdf";
            string CurrentDirectory = Directory.GetCurrentDirectory();
            string ProjectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentDirectory).FullName).FullName).FullName;
            string FilePath = Path.Combine(ProjectDirectory, FileName);

            string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={FilePath};Integrated Security=True;Connect Timeout=30;";
            SqlConnection connection = new SqlConnection(conn);
            // koniec zmiennych
            connection.Open();



            //wstawianie do tabelki           
            SqlCommand command = new SqlCommand("SELECT * FROM NadchodzacePremiery", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            connection.Close();

            premiery.ItemsSource = dataTable.DefaultView;
        }

        private void powrot_Click(object sender, RoutedEventArgs e)
        {
            MainWindow r3 = new MainWindow();
            this.Close();
            r3.Show();
        }
    }
}
