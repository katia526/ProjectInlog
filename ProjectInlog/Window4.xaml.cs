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
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
            Laad();
           
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        public class ComboboxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public override string ToString() { return Text; }
        }
        public class TestClass
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public TestClass()
            {
            }
        }

        private void cbVerwijder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void Laad()
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName }).ToArray();

                cbVerwijder.ItemsSource = col;
                cbVerwijder.DisplayMemberPath = "Name";
                cbVerwijder.SelectedValuePath = "Id";

            }
        }

        private void btnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            string id = cbVerwijder.SelectedValue.ToString();
            var del = Convert.ToInt32(id);

            using (ProjectContext ctx = new ProjectContext())
            {

                ctx.Employees.Remove(ctx.Employees.FirstOrDefault(c => c.UserId == del));
                ctx.SaveChanges();
                Laad();
            }
        }
    }
}
