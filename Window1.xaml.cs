﻿using System;
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
using System.Globalization;

namespace ShoeShop
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Loginback_btt_Click(object sender, RoutedEventArgs e)
        {
            MainWindow r1 = new MainWindow();
            this.Close();
            r1.Show();
        }

        private void Register_btt_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Text == string.Empty || Password.Password == string.Empty)
            {
                MessageBox.Show("Wypełnij wszystkie pola!", "Błąd", MessageBoxButton.OK);
                Window1 okno = new Window1();
                okno.Show();
                this.Close();
            }
            else
            {
                try
                {
                    string FileName = "Buty.mdf";
                    string CurrentDirectory = Directory.GetCurrentDirectory();
                    string ProjectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentDirectory).FullName).FullName).FullName;
                    string FilePath = Path.Combine(ProjectDirectory, FileName);

                    string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={FilePath};Integrated Security=True;Connect Timeout=30;";
                    SqlConnection con = new SqlConnection(conn);


                    con.Open();
                    string add_data = "INSERT into Uzytkownicy values(@username, @password) ";
                    SqlCommand cmd = new SqlCommand(add_data, con);


                    cmd.Parameters.AddWithValue("@username", Username.Text);
                    cmd.Parameters.AddWithValue("@password", Password.Password);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Username.Text = "";
                    Password.Password = "";

                    MainWindow w1 = new MainWindow();
                    this.Close();
                    w1.Show();
                }
                catch
                {

                }
            }
        }
    }
}
