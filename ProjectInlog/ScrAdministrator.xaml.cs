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
            Administrator administrator = new Administrator();

            administrator.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            magazijnTab magazijnTab = new magazijnTab();
            magazijnTab.Show();
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            Verkopers verkopers = new Verkopers();
            verkopers.Show();
        }
    }
}
