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
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
      
        public Window3()
        {
            InitializeComponent();
            Laad();
        }
        

        private void cbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string id = cbUsers.SelectedValue.ToString();
            var del = Convert.ToInt32(id);

            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Employees.FirstOrDefault(c => c.UserId == del);
                txtFirstName.Text = col.FirstName;
                txtLastName.Text = col.LastName;
                txtEmail.Text = col.Email;
                //txtFunction.Text = col.Function;
                txtAddress.Text = col.Address;
            }

        }

        private void btnBewaar_Click(object sender, RoutedEventArgs e)
        {
            string id = cbUsers.SelectedValue.ToString();
            var sel = Convert.ToInt32(id);

            using (ProjectContext ctx = new ProjectContext())
            {
                ctx.Employees.FirstOrDefault(c => c.UserId == sel).FirstName = txtFirstName.Text;
                ctx.Employees.FirstOrDefault(c => c.UserId == sel).LastName = txtLastName.Text;
                ctx.Employees.FirstOrDefault(c => c.UserId == sel).Email = txtEmail.Text;
                ctx.Employees.FirstOrDefault(c => c.UserId == sel).Address = txtAddress.Text;

                ctx.SaveChanges();
                Laad();
            }
        }
        public class Employee
        {
            public int EmployeeId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        private void cbUsers_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            if (cbUsers.SelectedValue.ToString() != null)
            {
                string id = cbUsers.SelectedValue.ToString();
                var del = Convert.ToInt32(id);

                using (ProjectContext ctx = new ProjectContext())
                {
                    var col = ctx.Employees.FirstOrDefault(c => c.UserId == del);
                    txtFirstName.Text = col.FirstName;
                    txtLastName.Text = col.LastName;
                    txtEmail.Text = col.Email;
                    //  txtFunction.Text = col.Function;
                    txtAddress.Text = col.Address;
                }
            }
           
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void Laad()
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName }).ToList();

                cbUsers.ItemsSource = col;
                cbUsers.DisplayMemberPath = "Name";
                cbUsers.SelectedValuePath = "Id";
            }
        }
    }
}
