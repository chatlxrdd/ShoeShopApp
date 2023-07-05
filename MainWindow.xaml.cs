using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace ShoeShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login2_btt_Click(object sender, RoutedEventArgs e)
        {
            Window1 r1 = new Window1();
            this.Close();
            r1.Show();
        }

        private void Login_btt_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == string.Empty || txtPassword.Password == string.Empty)
            {
                MessageBox.Show("Wypełnij wszystkie pola!", "Błąd", MessageBoxButton.OK);
                MainWindow okno = new MainWindow();
                okno.Show();
                this.Close();
            }
            else
            {
                string FileName = "Buty.mdf";
                string CurrentDirectory = Directory.GetCurrentDirectory();
                string ProjectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentDirectory).FullName).FullName).FullName;
                string FilePath = Path.Combine(ProjectDirectory, FileName);

                string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={FilePath};Integrated Security=True;Connect Timeout=30;";
                SqlConnection con = new SqlConnection(conn);

                con.Open();

                string add_data = "Select * From Uzytkownicy Where Login=@username and Haslo=@password ";
                string add_data2 = "Select LoginID from uzytkownicy where Login=@username and Haslo=@password ";

                SqlCommand cmd = new SqlCommand(add_data, con);
                SqlCommand cmd2 = new SqlCommand(add_data2, con);
                List<int> userId = new List<int>();

                cmd2.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd2.Parameters.AddWithValue("@password", txtPassword.Password);
                SqlDataReader reader = cmd2.ExecuteReader();

                while (reader.Read())
                {
                    userId.Add(reader.GetInt32(0));
                }
                reader.Close();

                if (userId.Count > 0)
                {
                    int c = userId[0];

                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Password);

                    cmd.ExecuteNonQuery();
                    int Count = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();

                    txtUsername.Text = "";
                    txtPassword.Password = "";

                    if (Count > 0)
                    {
                        Window3 u3 = new Window3();
                        this.Close();
                        u3.Show();
                    }
                    else
                    {
                        MessageBox.Show("Niepoprawny login lub hasło");
                    }
                }
                else
                {
                    con.Close(); // Zamknij połączenie, gdy userId.Count == 0
                    MessageBox.Show("Niepoprawny login lub hasło");
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 r2 = new Window2();
            this.Close();
            r2.Show();
        }
    }
}
