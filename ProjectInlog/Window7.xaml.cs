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
    /// Interaction logic for Window7.xaml
    /// </summary>
    public partial class Window7 : Window
    {
        public Window7()
        {
            InitializeComponent();

            using (ProjectContext ctx = new ProjectContext())
            {

              //  var col = ctx.Supplier.AsEnumerable().Where(c => c.S_Name.StartsWith())
               var col = ctx.Supplier.Select(c => new { Id = c.SupplierId, Name = c.S_Name }).ToList();
               
                cbLeverancier.ItemsSource = col;
                cbLeverancier.DisplayMemberPath = "Name";
                cbLeverancier.SelectedValuePath = "Id";
            }
        }

        private void cbLeverancier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
                  
            string id = cbLeverancier.SelectedValue.ToString();
            var del = Convert.ToInt32(id);
            using (ProjectContext ctx = new ProjectContext())
            {
                ctx.Supplier.Remove(ctx.Supplier.FirstOrDefault(c => c.SupplierId == del));

                ctx.SaveChanges();
            }
            
        }
    }
}
