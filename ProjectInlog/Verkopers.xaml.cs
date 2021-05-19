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

            }
        }

        private void lbKlant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbKlant.Items.Count != 0)
           
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

        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Clients.Select(c => new { Id = c.ClientId, Name = c.C_Name }).ToList();

                cmbKlant.ItemsSource = col;
                cmbKlant.DisplayMemberPath = "Name";
                cmbKlant.SelectedValuePath = "Id";

            }
        }

        private void cmbKlant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string id = cmbKlant.SelectedValue.ToString();
            var del = Convert.ToInt32(id);

            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Clients.FirstOrDefault(c => c.ClientId == del);
                txtKltNaam.Text = col.C_Name;
                txtKltAdres.Text = col.C_Adress;
                txtKltContact.Text = col.C_Contact;
                txtKltEmail.Text = col.C_Email;
                txtKltPostcode.Text = col.C_PostCode;
                txtKltTelefoon.Text = col.C_Phone;
                txtKltWoonplaats.Text = col.C_Woonplaats;
                txtKltBtwNr.Text = col.C_BtwNr;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string id = cmbKlant.SelectedValue.ToString();
            var del = Convert.ToInt32(id);
            using (ProjectContext ctx = new ProjectContext())
            {
                Client S_tewijzigen = ctx.Clients.Where(c => c.ClientId == del).FirstOrDefault();
                S_tewijzigen.C_Name = txtKltNaam.Text;
                S_tewijzigen.C_Contact = txtKltContact.Text;
                S_tewijzigen.C_Woonplaats = txtKltWoonplaats.Text;
                S_tewijzigen.C_PostCode = txtKltPostcode.Text;
                S_tewijzigen.C_Email = txtKltEmail.Text;
                S_tewijzigen.C_Adress = txtKltAdres.Text;
                S_tewijzigen.C_BtwNr = txtKltBtwNr.Text;
                S_tewijzigen.C_Phone = txtKltTelefoon.Text;

                ctx.SaveChanges();
                txtKltNaam.Text = " ";
                txtKltAdres.Text = " ";
                txtKltContact.Text = " ";
                txtKltEmail.Text = " ";
                txtKltPostcode.Text = " ";
                txtKltTelefoon.Text = " ";
                txtKltBtwNr.Text = " ";
                txtKltWoonplaats.Text = " ";
            }
        }

        private void btnAlfa_Click(object sender, RoutedEventArgs e)
        {
            txtHoofd.Text = "Alfabetisch gesorteerd";
            using (ProjectContext ctx = new ProjectContext())
            {
               
                var col = ctx.Clients.OrderBy(c => c.C_Name).ToList()
                     .Select(c => new { Id = c.ClientId, Name = c.C_Name, Phone = c.C_Phone }).ToList();
                if (col != null)
                {
                    lstKlant.ItemsSource = col;
                   
                }
            }
        }

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            txtHoofd.Text = "Postcode: " + txtPost.Text;
            using (ProjectContext ctx = new ProjectContext())
            {
                //var col = ctx.Clients.Where(s => s.C_PostCode == txtPost.Text);
                    
                var col = ctx.Clients.Where(s => s.C_PostCode == txtPost.Text)
                  .Select(c => new { Id = c.ClientId, Name = c.C_Name, Phone = c.C_Phone }).ToList();

                if (col != null)
                {
                    lstKlant.ItemsSource = col;
                }
            }    
        }

        private void TabItem_Loaded_2(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Employees.Where(s => s.Function == "3")
                   .Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName }).ToList();

                cmbVerk.ItemsSource = col;
                cmbVerk.DisplayMemberPath = "Name";
                cmbVerk.SelectedValuePath = "Id";
               
                var coll = ctx.Clients.Select(c => new { Id = c.ClientId, Name = c.C_Name }).ToList();

                cmbKlnt.ItemsSource = coll;
                cmbKlnt.DisplayMemberPath = "Name";
                cmbKlnt.SelectedValuePath = "Id";

                var prod = ctx.Products.Select(c => new { Id = c.ProductId, Name = c.Description, Prijs = c.Price }).ToList();

                cmbProd.ItemsSource = prod;
                cmbProd.DisplayMemberPath = "Name";
                cmbProd.SelectedValuePath = "Id";

            }    

        }

        private void cmbProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string id = cmbProd.SelectedValue.ToString();
            var del = Convert.ToInt32(id);
            using (ProjectContext ctx = new ProjectContext())
            {
                Product prijs = ctx.Products.Where(c => c.ProductId == del).FirstOrDefault();
             //       .Select(c => new { Id = c.ProductId, Name = c.Description }).ToList();

               // Client S_tewijzigen = ctx.Clients.Where(c => c.ClientId == del).FirstOrDefault();


                txtPrijs.Text = prijs.Price.ToString();
            }
        }

        private void btnKeep_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
