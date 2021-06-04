using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
        List<Bestelling> bestellingen = new List<Bestelling>();
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
            txtErrorMessage.Text = " ";
            if (String.IsNullOrEmpty(txtNaam.Text) )
            {
                txtErrorMessage.Text = null;
                txtErrorMessage.Foreground = Brushes.White;
                txtErrorMessage.Background = Brushes.Red;
                txtErrorMessage.Visibility = Visibility.Visible;
                txtErrorMessage.Text = string.Format(" Vul een naam in!");
                MessageBox.Show("vul een naam in");
              
                txtNaam.Select(txtNaam.Text.Length,0);
            }
            if (String.IsNullOrEmpty(txtAdres.Text))
            {
                txtErrorMessage.Text = null;
                txtErrorMessage.Foreground = Brushes.White;
                txtErrorMessage.Background = Brushes.Red;
                txtErrorMessage.Visibility = Visibility.Visible;
                txtErrorMessage.Text = string.Format(" Vul een adres in!");
                MessageBox.Show("vul een adres in");

                txtAdres.Select(txtAdres.Text.Length, 0);
            }
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
                if (txtKltNaam.Text != "")
                {
                    S_tewijzigen.C_Name = txtKltNaam.Text;
                }
                if (txtKltAdres.Text != "")
                {
                    S_tewijzigen.C_Adress = txtKltAdres.Text;
                }
                if (txtKltPostcode.Text != "")
                {
                    S_tewijzigen.C_PostCode = txtKltPostcode.Text;
                }
                if (txtKltTelefoon.Text != "")
                {
                    S_tewijzigen.C_Phone = txtKltTelefoon.Text;
                }
                if (txtKltContact.Text != "")
                {
                    S_tewijzigen.C_Contact = txtKltContact.Text;
                }
                if (txtKltBtwNr.Text != "")
                {
                    S_tewijzigen.C_BtwNr = txtKltBtwNr.Text;
                }
                if (txtKltWoonplaats.Text != "")
                {
                    S_tewijzigen.C_Woonplaats = txtKltWoonplaats.Text;
                }
                if (txtKltEmail.Text != "")
                {
                    S_tewijzigen.C_Email = txtKltEmail.Text;
                }
              
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
               
                var col = ctx.Clients.OrderBy(c => c.C_Name)
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

        public void btnKeep_Click(object sender, RoutedEventArgs e)
        {
            lstbe.ItemsSource = " ";
            string idP = cmbProd.SelectedValue.ToString();
            var pro = Convert.ToInt32(idP);
            
            cmbKlnt.IsEnabled = false;
            cmbVerk.IsEnabled = false;

            bestellingen.Add(new Bestelling()
            {
                Aantal = Convert.ToInt32(txtAantal.Text),
                Prijs = Convert.ToDouble(txtPrijs.Text),
                Product = idP,
                Pnaam = cmbProd.Text
            });


            //foreach (var bestelling in bestellingen)
            //{
            lstbe.ItemsSource = bestellingen;
            //}



            //foreach (var bestelling in bestellingen)
            //{
            //    string[] arr = new string[10];
            //    ListViewItem itm;
            //    arr[0] = txtAantal.Text;
            //    arr[1] = txtPrijs.Text;
            //    arr[2] = cmbProd.Text;
            //    itm = new ListViewItem(arr);
            //    lstView.Items.Add(itm);

            //lstView.Items.Add(bestelling.Product, bestelling.Aantal, bestelling.Prijs);
            //lstView.Items.Add(item.Aantal);
            //lstView.Items.Add({ prijs = item.Prijs });
            //    lstView.Items.Add(new ListViewItem(new string[] { txtAantal.Text, txtPrijs.Text}));
            //}
            txtAantal.Text = " ";
            txtPrijs.Text = " ";
           
        }
        class Bestelling
        {
           
            public string Product { get; set; }
            public string Pnaam { get; set; }
            public int Aantal { get; set; }
            public double Prijs { get; set; }
            
        }
        class Klient
        {

            public string Knaam { get; set; }
            public string Kadres { get; set; }
            public int Kwoonplaats { get; set; }
            public double Kbtw { get; set; }

        }
       

        private void btnSlaOp_Click(object sender, RoutedEventArgs e)
        {
            string id = cmbKlnt.SelectedValue.ToString();
            var klt = Convert.ToInt32(id);

            string idV = cmbVerk.SelectedValue.ToString();
            var lev = Convert.ToInt32(idV);

            using (ProjectContext ctx = new ProjectContext())
            {

                ctx.Orders.Add(new Order()
                {
                    ClientId = klt,
                    VerkId = lev,
                    OrderedAt = DateTime.Now,
                    Invoice = false
   
                });

             


                foreach (var bestelling in bestellingen)
                {
                    ctx.OrderLines.Add(new OrderLine()
                    {
                      
                       ProductId = Convert.ToInt32(bestelling.Product),
                       O_Aantal = Convert.ToInt32(bestelling.Aantal)
                    });

                }
                ctx.SaveChanges();
            }
            txtAantal.Text = " ";
            txtPrijs.Text = " ";
            lstbe.ItemsSource = " ";
            cmbKlnt.IsEnabled = true;
            cmbVerk.IsEnabled = true;

        }

        private void TabItem_Loaded_3(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var klant = ctx.Clients.Select(c => new { Id = c.ClientId, Name = c.C_Name }).ToList();

                cmbKlan.ItemsSource = klant;
                cmbKlan.DisplayMemberPath = "Name";
                cmbKlan.SelectedValuePath = "Id";
            }

                
        }

        private void cmbKlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string id = cmbKlan.SelectedValue.ToString();
            var klt = Convert.ToInt32(id);
            using (ProjectContext ctx = new ProjectContext())
            {

                

                var sell = ctx.OrderLines.Join(ctx.Orders,
                s => s.Order.OrderId,
                a => a.OrderId,
                (s, a) => new { s, a })
                    .Where(z => z.a.ClientId == klt);
                var best = sell.Join(ctx.Products,
                    sa => sa.s.ProductId,
                    alb => alb.ProductId,
                    (sa, alb) => new { Name = alb.Description, Price = alb.Price, Aantal = sa.s.O_Aantal, Ordr = sa.a.OrderId, Tot = alb.Price *sa.s.O_Aantal }).ToList();

                

                lstbest.ItemsSource = best;
                
            }
        }

        private void txtAantal_TextChanged(object sender, TextChangedEventArgs e)
        {
            //double prs = Convert.ToInt32(txtPrijs.Text);
            //var aan = Convert.ToInt32(txtAantal.Text);
            //double tot = ( prs * aan);
            //txtTotaal.Text = tot.ToString();
        }

        private void btnFact_Click(object sender, RoutedEventArgs e)
        {
            
            DateTime Datum = DateTime.Now;
            string selected = cmbKl.Text;

            string id = cmbKl.SelectedValue.ToString();
            var klt = Convert.ToInt32(id);
            string file = ($"{selected}{Datum:yyyy.M.dd}.txt");
            using (StreamWriter sw = new StreamWriter(file))
            {
                using (ProjectContext ctx = new ProjectContext())
                {
                    
                    var sell = ctx.OrderLines.Join(ctx.Orders,
                s => s.Order.OrderId,
                a => a.OrderId,
                (s, a) => new { s, a })
                    .Where(z => z.a.ClientId == klt);
                    var best = sell.Join(ctx.Products,
                        sa => sa.s.ProductId,
                        alb => alb.ProductId,
                    //  (sa, alb) => new { Name = alb.Description, Price = alb.Price, Aantal = sa.s.O_Aantal, Kl = sa.a.ClientId, Ordr = sa.a.OrderId, Tot = Math.Round(alb.Price * sa.s.O_Aantal) }).ToList();
                    (sa, alb) => new { sa, alb });
                    var ok = best.Join(ctx.Clients,
                        b => b.sa.a.ClientId,
                        c => c.ClientId,
                        (b, c) => new { Name = b.alb.Description, Price = b.alb.Price, Aantal = b.sa.s.O_Aantal, 
                            Kl = b.sa.a.ClientId, Ordr = b.sa.a.OrderId, KL_name = c.C_Name, KL_adres = c.C_Adress, KL_woonplaats = c.C_Woonplaats, KL_postcode = c.C_PostCode, KL_btw = c.C_BtwNr, Tot = Math.Round(b.alb.Price * b.sa.s.O_Aantal) }).ToList();


                    var eind = false;
                    var tel = 0;
                    double tota = 0;
                    foreach (var item in ok)
                    {
                        
                        if (tel != item.Ordr)
                        {
                            if ( eind)
                            {
                                sw.WriteLine();
                                sw.WriteLine("---------------------------------------------------------------------------------");
                                sw.WriteLine();
                                sw.WriteLine($"Totaal factuur zonder BTW is {tota}");
                                sw.WriteLine($"BTW bedrag is {tota * 0.06}");
                                sw.WriteLine($"Totaal met BTW bedraagt {tota += (tota * 0.06)}");
                                sw.WriteLine("<% @Page %>");
                                sw.WriteLine();
                            }
                             

                            sw.WriteLine("Factuur");
                            sw.WriteLine();
                            sw.WriteLine($"{ item.KL_name}");
                            sw.WriteLine($"{ item.KL_adres}");
                            sw.WriteLine($"{ item.KL_postcode}  {item.KL_woonplaats}");
                            sw.WriteLine($"{ item.KL_btw}");
                            sw.WriteLine();
                            sw.WriteLine($"Ordernummer =  { item.Ordr}");
                            sw.WriteLine();
                            sw.WriteLine("Omschrijving    Prijs    Aantal    Totaal");
                            sw.WriteLine();
                            sw.WriteLine("---------------------------------------------------------------------------------");
                            sw.WriteLine();
                            sw.WriteLine($"{item.Name}      {item.Price}     {item.Aantal}             {item.Tot}");
                            if (tel != item.Ordr)
                            {
                                eind = true;
                            }
                            tel = item.Ordr;
                            tota =+ item.Tot;
                        }
                        
                        else
                        {
                            sw.WriteLine($"{item.Name}    {item.Price}      {item.Aantal}           {item.Tot}");
                            tota += item.Tot;
                            //if (tel != item.Ordr)
                            //{
                            //    sw.WriteLine();
                            //    sw.WriteLine("---------------------------------------------------------------------------------");
                            //    sw.WriteLine();
                            //    sw.WriteLine($"Totaal factuur zonder BTW is {tota}");
                            //    sw.WriteLine($"BTW bedrag is {tota * 0.06}");
                            //    sw.WriteLine($"Totaal met BTW bedraagt {tota += (tota * 0.06)}");
                            //}
                            if (tel != item.Ordr)
                            {
                                eind = true;
                            }
                            tel = item.Ordr;
                            
                        }
                        

                    }
                    sw.WriteLine();
                    sw.WriteLine("---------------------------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine($"Totaal factuur zonder BTW is {tota}");
                    sw.WriteLine($"BTW bedrag is {tota * 0.06}");
                    sw.WriteLine($"Totaal met BTW bedraagt {tota += (tota * 0.06)}");

                    ctx.Invoices.Add(new Invoice()
                    {
                        Amount = tota,
                        Status = false,
                        CreatedAt = Datum,
                      PayedAt = Datum,
                      OrderId = tel
                    });

                  //  Order S_Change = ctx.Orders.Where(c => c.OrderId == ).FirstOrDefault();
                    //ctx.SaveChanges();
                    try
                    {
                        // Your code...
                        // Could also be before try if you know the exception occurs in SaveChanges

                       ctx.SaveChanges();
                    }
                    catch (DbEntityValidationException f)
                    {
                        foreach (var eve in f.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                

                }
               

            }
        }

        private void TabItem_Loaded_4(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var klant = ctx.Clients.Select(c => new { Id = c.ClientId, Name = c.C_Name }).ToList();

                cmbKl.ItemsSource = klant;
                cmbKl.DisplayMemberPath = "Name";
                cmbKl.SelectedValuePath = "Id";
            }
        }

        private void cmbVerk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cmbVerk.IsEnabled = false;
        }
    }
}
