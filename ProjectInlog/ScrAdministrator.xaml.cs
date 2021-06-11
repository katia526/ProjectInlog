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
using System.Windows.Shapes;

namespace ProjectInlog
{
    /// <summary>
    /// Interaction logic for ScrAdministrator.xaml
    /// </summary>
    public partial class ScrAdministrator : Window
    {
        public ScrAdministrator()
        {
            InitializeComponent();
           
        }

        private void btnProfiel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Administrator administrator = new Administrator();

            administrator.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }

        private void btnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            magazijnTab magazijnTab = new magazijnTab();
            magazijnTab.Show();
            this.Close();
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Verkopers verkopers = new Verkopers();
            verkopers.Show();
            this.Close();
        }
    }
}
