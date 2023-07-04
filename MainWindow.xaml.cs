using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
                //logowanie przez bazę do UserPanel
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
