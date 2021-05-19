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
    /// Interaction logic for Verkopers.xaml
    /// </summary>
    public partial class Verkopers : Window
    {
        public Verkopers()
        {
            InitializeComponent();
            
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbVerkoper_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            using (ProjectContext ctx = new ProjectContext())
            {
               // var col = ctx.Employees.Where(c => c.Function == "3").ToList();
                // var col = ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName }).ToList();
                // var col = ctx.Employees.Where(s => (s.Function = "3").ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName }));
                //var col = ctx.Employees.Select(ctx.Employees.Where(s => s.Function == "3").(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName })).ToList();

                var col = ctx.Employees.Where(s => s.Function == "3")
                    .Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName }).ToList();

                cmbVerkoper.ItemsSource = col;
                cmbVerkoper.DisplayMemberPath = "Name";
                cmbVerkoper.SelectedValuePath = "Id";
            }

        }

        private void btnBewaar_Click(object sender, RoutedEventArgs e)
        {
            string id = cmbVerkoper.SelectedValue.ToString();
            var del = Convert.ToInt32(id);
           // DateTime now = DateTime.Now;


            using (ProjectContext ctx = new ProjectContext())
            {
              
            var result = ctx.Clients.FirstOrDefault(c => c.C_Name == txtNaam.Text && c.C_Adress == txtAdres.Text);

            if (result != null)
            {
                txtErrorMessage.Visibility = Visibility.Visible;
                txtErrorMessage.Text = " Deze Klant bestaat reeds!";
                    txtNaam.Text = " ";
                txtNaam.Focus();
            }
            else
            {
                    ctx.Clients.Add(new Client()
                    {
                        C_Name = txtNaam.Text,
                        C_Adress = txtAdres.Text,
                        C_PostCode = txtPostCode.Text,
                        C_Woonplaats = txtWoonplaats.Text,
                        C_Contact = txtContact.Text,
                        C_Phone = txtTelefoon.Text,
                        C_BtwNr = txtBTWnr.Text,
                        C_Email = txtEmail.Text,
                        C_Verkoper = del.ToString(),
                        C_ChangedAt = DateTime.Now
                    }) ; 
                ctx.SaveChanges();

                txtNaam.Text = " ";
                txtAdres.Text = " ";
                txtPostCode.Text = " ";
                txtWoonplaats.Text = " ";
                txtContact.Text = " ";
                txtTelefoon.Text = " ";
                txtEmail.Text = " ";
                txtBTWnr.Text = " ";
            }
        }

    }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Clients.Select(c => new { Id = c.ClientId, Name = c.C_Name }).ToList();

                lbKlant.ItemsSource = col;
                lbKlant.DisplayMemberPath = "Name";
                lbKlant.SelectedValuePath = "Id";


                //var result = ctx.Clients.FirstOrDefault(c => c.C_Name == txtNaam.Text && c.C_Adress == txtAdres.Text);


            }
        }

        private void lbKlant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbKlant.SelectedValue.ToString() != null)
            {
                string id = lbKlant.SelectedValue.ToString();
                var del = Convert.ToInt32(id);

                using (ProjectContext ctx = new ProjectContext())
                {
                    var col = ctx.Clients.FirstOrDefault(c => c.ClientId == del);
                    txtNaamKl.Text = col.C_Name;
                    txtAdresKl.Text = col.C_Adress;
                    txtWoonplaatsKl.Text = col.C_Woonplaats;
                }
            }
        }

        private void btnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            string id = lbKlant.SelectedValue.ToString();
            var del = Convert.ToInt32(id);
            using (ProjectContext ctx = new ProjectContext())
            {
                ctx.Clients.Remove(ctx.Clients.FirstOrDefault(c => c.ClientId == del));

                ctx.SaveChanges();

               
                    var col = ctx.Clients.Select(c => new { Id = c.ClientId, Name = c.C_Name }).ToList();

                    lbKlant.ItemsSource = col;
                    lbKlant.DisplayMemberPath = "Name";
                    lbKlant.SelectedValuePath = "Id";

                txtNaamKl.Text = " ";
                txtAdresKl.Text = " ";
                txtWoonplaatsKl.Text = " ";

            }
        }
    }
}
