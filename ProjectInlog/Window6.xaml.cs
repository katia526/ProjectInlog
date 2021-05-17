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
    /// Interaction logic for Window6.xaml
    /// </summary>
    public partial class Window6 : Window
    {
        public Window6()
        {
            InitializeComponent();
        }

        private void btnBewaar_Click(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var result = ctx.Supplier.FirstOrDefault(c => c.S_Name == txtNaam.Text && c.S_Address == txtStraat.Text);

                if (result != null)
                {
                    txtErrorMessage.Visibility = Visibility.Visible;
                    txtErrorMessage.Text = " Deze leverancier bestaat reeds!";
                    txtNaam.Focus();
                }
                else
                {
                    ctx.Supplier.Add(new Supplier()
                    {
                        S_Name = txtNaam.Text,
                        S_Address = txtStraat.Text,
                        s_PostCode = txtpostcode.Text,
                        s_City = txtPlaats.Text,
                        s_Contact = txtContact.Text,
                        S_Phone = txtFoon.Text,
                        S_Email = txtEmail.Text
                    });
                    ctx.SaveChanges();
                    txtNaam.Text = " ";
                    txtStraat.Text = " ";
                    txtpostcode.Text = " ";
                    txtPlaats.Text = " ";
                    txtContact.Text = " ";
                    txtFoon.Text = " ";
                    txtEmail.Text = " ";
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
