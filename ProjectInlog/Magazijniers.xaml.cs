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
using static ProjectInlog.MainWindow;

namespace ProjectInlog
{
    /// <summary>
    /// Interaction logic for Magazijniers.xaml
    /// </summary>
    public partial class Magazijniers : Window
    {
        public Magazijniers()
        {
            InitializeComponent(); 
            btnVoegtoe.Visibility = Visibility.Hidden;
            btnPasAan.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
            btnMagazijn.Visibility = Visibility.Visible;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNieuwe_Click(object sender, RoutedEventArgs e)
        {
            btnVoegtoe.Visibility = Visibility.Visible;
            btnPasAan.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Visible;
            btnMagazijn.Visibility = Visibility.Hidden;

        }

        private void btnVoegtoe_Click(object sender, RoutedEventArgs e)
        {
            Window6 window6 = new Window6();
            window6.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Window7 window7 = new Window7();
            window7.Show();
        }
    }
}
