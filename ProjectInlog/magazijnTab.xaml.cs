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
    /// Interaction logic for magazijnTab.xaml
    /// </summary>
    public partial class magazijnTab : Window
    {
        public magazijnTab()
        {
            InitializeComponent();
        }

        private void btnBewaar_Click(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var result = ctx.Supplier.FirstOrDefault(c => c.S_Name == txtNaam.Text && c.S_Address == txtAdres.Text);

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
                        S_Address = txtAdres.Text,
                        s_PostCode = txtPostCode.Text,
                        s_City = txtWoonplaats.Text,
                        s_Contact = txtContact.Text,
                        S_Phone = txtTelefoon.Text,
                        S_Email = txtEmail.Text,
                        S_Contact = txtContact.Text
                    });
                    ctx.SaveChanges();
                    txtNaam.Text = " ";
                    txtAdres.Text = " ";
                    txtPostCode.Text = " ";
                    txtWoonplaats.Text = " ";
                    txtContact.Text = " ";
                    txtTelefoon.Text = " ";
                    txtEmail.Text = " ";
                    txtContact.Text = " ";
                }
            }
        }

        private void txtEditor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {

                //  var col = ctx.Supplier.AsEnumerable().Where(c => c.S_Name.StartsWith())
                var col = ctx.Supplier.Select(c => new { Id = c.SupplierId, Name = c.S_Name }).ToList();

                lbLeveranciers.ItemsSource = col;
                lbLeveranciers.DisplayMemberPath = "Name";
                lbLeveranciers.SelectedValuePath = "Id";
            }
        }
    }
}
