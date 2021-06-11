

using Syncfusion.Drawing;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
//using System.Data.Entity.Validation;
//using System.IO;
using System.Linq;

using System.Windows;
using System.Windows.Controls;
using static ProjectInlog.MainWindow;

using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Image = Xceed.Document.NET.Image;
using Picture = Xceed.Document.NET.Picture;
//using DataTable = System.Data.DataTable;
//using System.Diagnostics;
//using Syncfusion.Drawing;
//using System.Drawing;

//using System.Drawing;

namespace ProjectInlog
{
    /// <summary>
    /// Interaction logic for Verkopers.xaml
    /// </summary>
    public partial class Verkopers : System.Windows.Window
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


            var id = " ";
            if (cmbVerkoper.SelectedValue != null)
            {
                id = cmbVerkoper.SelectedValue.ToString();
                Bewaar(id);
            }
            else
            {
                MessageBox.Show("kies een verkoper");
                cmbVerkoper.Focus();
            }
        }
        private void Bewaar(string id)
        {
            var del = Convert.ToInt32(id);
            txtErrorMessage.Text = " ";
            if (String.IsNullOrEmpty(txtNaam.Text))
            {
                txtErrorMessage.Text = null;
                
                txtErrorMessage.Visibility = Visibility.Visible;
                txtErrorMessage.Text = string.Format(" Vul een naam in!");
                MessageBox.Show("vul een naam in");

                txtNaam.Select(txtNaam.Text.Length, 0);
            }
            if (String.IsNullOrEmpty(txtAdres.Text))
            {
                txtErrorMessage.Text = null;
               
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
                    });
                    ctx.SaveChanges();
                    MessageBox.Show("klant werd succesvol toegevoegd");
                    txtNaam.Text = " ";
                    txtAdres.Text = " ";
                    txtPostCode.Text = " ";
                    txtWoonplaats.Text = " ";
                    txtContact.Text = " ";
                    txtTelefoon.Text = " ";
                    txtEmail.Text = " ";
                    txtBTWnr.Text = " ";
                    LaadKlant();
                    
                }
            }
        }


        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            LaadKlant();
        }


        private void LaadKlant()
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
                if (id != "")
                {
                    var del = Convert.ToInt32(id);

                    using (ProjectContext ctx = new ProjectContext())
                    {
                        var col = ctx.Clients.FirstOrDefault(c => c.ClientId == del);
                        txtNaamKl.Text = col.C_Name;
                        txtAdresKl.Text = col.C_Adress;
                        txtWoonplaatsKl.Text = col.C_Woonplaats;
                    }
                }
                else
                {
                    MessageBox.Show("kies een klant ");
                }
            }
        }

        private void btnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
            string id = lbKlant.SelectedValue.ToString();
            
                var del = Convert.ToInt32(id);
                using (ProjectContext ctx = new ProjectContext())
                {
                    ctx.Clients.Remove(ctx.Clients.FirstOrDefault(c => c.ClientId == del));

                    ctx.SaveChanges();
                    MessageBox.Show("klant werd verwijdert");

                    var col = ctx.Clients.Select(c => new { Id = c.ClientId, Name = c.C_Name }).ToList();

                    lbKlant.ItemsSource = col;
                    lbKlant.DisplayMemberPath = "Name";
                    lbKlant.SelectedValuePath = "Id";

                    txtNaamKl.Text = " ";
                    txtAdresKl.Text = " ";
                    txtWoonplaatsKl.Text = " ";

                }
            }
            catch(Exception)
            {
                MessageBox.Show("kies een klant!!");
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

            try
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
                    MessageBox.Show("de klant gegevens werden aangepast");
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
            catch(Exception)
            {
                MessageBox.Show($"selecteer een klant ");

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
            try
            {

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

                lstbe.ItemsSource = bestellingen;

                txtAantal.Text = " ";
                txtPrijs.Text = " ";
            }
            catch(Exception)
            {
                MessageBox.Show("er zijn geen velden ingevuld");
            }
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
            try
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
                MessageBox.Show("uw bestelling werd weggeschreven");
            }
            catch(Exception)
            {
                MessageBox.Show("er zijn geen velden ingevuld");
            }
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
                    (sa, alb) => new { Name = alb.Description, Price = alb.Price, Aantal = sa.s.O_Aantal, Ordr = sa.a.OrderId, Tot = alb.Price * sa.s.O_Aantal }).ToList();



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
                        (b, c) => new
                        {
                            Name = b.alb.Description,
                            Price = b.alb.Price,
                            Aantal = b.sa.s.O_Aantal,
                            Kl = b.sa.a.ClientId,
                            Ordr = b.sa.a.OrderId,
                            KL_name = c.C_Name,
                            KL_adres = c.C_Adress,
                            KL_woonplaats = c.C_Woonplaats,
                            KL_postcode = c.C_PostCode,
                            KL_btw = c.C_BtwNr,
                            Tot = Math.Round(b.alb.Price * b.sa.s.O_Aantal)
                        }).ToList();


                    var eind = false;
                    var tel = 0;
                    double tota = 0;
                    foreach (var item in ok)
                    {

                        if (tel != item.Ordr)
                        {
                            if (eind)
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
                            tota = +item.Tot;
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

        private void btnword_Click(object sender, RoutedEventArgs e)
        {
            //  CreateDocument();
            CreateTest();

        }
        //public FormattedText(string textToFormat, Typeface typeface, double emSize, System.Windows.Media.Brush foreground)
        //{

        //}



        //public String titleFormat { get; set; }
        //private void CreateDocument()
        //{

        //    DateTime Datum = DateTime.Now;
        //    string selected = cmbKl.Text;

        //    string id = cmbKl.SelectedValue.ToString();
        //    var klt = Convert.ToInt32(id);

        //    string file = ($"{selected}{Datum:yyyy.M.dd}.pdf");

        //    //         string fileName = @"C:\Users\HP\source\repos\ProjectInlog\ProjectInlog\bin\Debug\temp1.docx";
        //    //         var doc = DocX.Create(file);


        //    //         using (ProjectContext ctx = new ProjectContext())
        //    //         {

        //    //             var sell = ctx.OrderLines.Join(ctx.Orders,
        //    //         s => s.Order.OrderId,
        //    //         a => a.OrderId,
        //    //         (s, a) => new { s, a })
        //    //             .Where(z => z.a.ClientId == klt);
        //    //             var best = sell.Join(ctx.Products,
        //    //                 sa => sa.s.ProductId,
        //    //                 alb => alb.ProductId,
        //    //             //  (sa, alb) => new { Name = alb.Description, Price = alb.Price, Aantal = sa.s.O_Aantal, Kl = sa.a.ClientId, Ordr = sa.a.OrderId, Tot = Math.Round(alb.Price * sa.s.O_Aantal) }).ToList();
        //    //             (sa, alb) => new { sa, alb });
        //    //             var ok = best.Join(ctx.Clients,
        //    //                 b => b.sa.a.ClientId,
        //    //                 c => c.ClientId,
        //    //                 (b, c) => new
        //    //                 {
        //    //                     Name = b.alb.Description,
        //    //                     Price = b.alb.Price,
        //    //                     Aantal = b.sa.s.O_Aantal,
        //    //                     Kl = b.sa.a.ClientId,
        //    //                     Ordr = b.sa.a.OrderId,
        //    //                     KL_name = c.C_Name,
        //    //                     KL_adres = c.C_Adress,
        //    //                     KL_woonplaats = c.C_Woonplaats,
        //    //                     KL_postcode = c.C_PostCode,
        //    //                     KL_btw = c.C_BtwNr,
        //    //                     Tot = Math.Round(b.alb.Price * b.sa.s.O_Aantal)
        //    //                 }).ToList();







        //    using (PdfDocument pdf = new PdfDocument())
        //    {
        //        PdfPage Page = pdf.AddPage();
        //        PdfPage page = pdf.Pages.Add();
        //        PdfGraphics graph = page.Graphics;

        //        //PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
        //        ////Add a page
        //        //PdfSharp.Pdf.PdfPage page = pdf.AddPage();
        //        // PdfGraphics graph = ;
        //        PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 24, PdfFontStyle.Bold);
        //        PdfFont font1 = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
        //        graph.DrawString("Factuur", font, PdfBrushes.Black, new PointF(10, 50));
        //        ;
        //        //string file = ($"{selected}{Datum:yyyy.M.dd}.txt");

        //        using (ProjectContext ctx = new ProjectContext())
        //        {

        //            var sell = ctx.OrderLines.Join(ctx.Orders,
        //        s => s.Order.OrderId,
        //        a => a.OrderId,
        //        (s, a) => new { s, a })
        //            .Where(z => z.a.ClientId == klt);
        //            var best = sell.Join(ctx.Products,
        //                sa => sa.s.ProductId,
        //                alb => alb.ProductId,
        //            //  (sa, alb) => new { Name = alb.Description, Price = alb.Price, Aantal = sa.s.O_Aantal, Kl = sa.a.ClientId, Ordr = sa.a.OrderId, Tot = Math.Round(alb.Price * sa.s.O_Aantal) }).ToList();
        //            (sa, alb) => new { sa, alb });
        //            var ok = best.Join(ctx.Clients,
        //                b => b.sa.a.ClientId,
        //                c => c.ClientId,
        //                (b, c) => new
        //                {
        //                    Name = b.alb.Description,
        //                    Price = b.alb.Price,
        //                    Aantal = b.sa.s.O_Aantal,
        //                    Kl = b.sa.a.ClientId,
        //                    Ordr = b.sa.a.OrderId,
        //                    KL_name = c.C_Name,
        //                    KL_adres = c.C_Adress,
        //                    KL_woonplaats = c.C_Woonplaats,
        //                    KL_postcode = c.C_PostCode,
        //                    KL_btw = c.C_BtwNr,
        //                    Tot = Math.Round(b.alb.Price * b.sa.s.O_Aantal)
        //                }).ToList();


        //            var eind = false;
        //            var tel = 0;
        //            double tota = 0;
        //            //   graph.DrawString(, font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
        //            var i = 150;
        //            var j = 50;
        //            foreach (var item in ok)
        //            {
        //                if (tel == 0)
        //                {
        //                    var gem = item.KL_postcode + " " + item.KL_woonplaats;
        //                    graph.DrawString(item.KL_name, font1, PdfBrushes.Black, new PointF(10, 30));
        //                    graph.DrawString(item.KL_adres, font1, PdfBrushes.Black, new PointF(10, 40));
        //                    graph.DrawString(gem, font1, PdfBrushes.Black, new PointF(10, 50));
        //                    graph.DrawString(item.KL_btw, font1, PdfBrushes.Black, new PointF(10, 60));
        //                    graph.DrawString("Product", font1, PdfBrushes.Black, new PointF(10, 70));
        //                    DataTable datatable = new DataTable();

        //                    datatable.Columns.Add("Product");
        //                    datatable.Columns.Add("Description");
        //                    datatable.Columns.Add("Quantity");
        //                    datatable.Columns.Add("Price");
        //                    tel = 1;
        //                }

        //                graph.DrawString(item.Name, font1, PdfBrushes.Black, new PointF(0, 160));
        //                graph.DrawString((item.Aantal).ToString(), font1, PdfBrushes.Black, new PointF(10, 30));
        //                graph.DrawString((item.Price).ToString(), font1, PdfBrushes.Black, new PointF(10, 30));
        //                graph.DrawString((item.Tot).ToString(), font1, PdfBrushes.Black, new PointF(10, 30));
        //            }
        //        }
        //        pdf.Save("firstpage.pdf");
        //        Process.Start("Firstpage.pdf");

        //        pdf.Close(true);

        //    }
        //}
        private void CreateTest()
        {
            DateTime Datum = DateTime.Now;
            string selected = cmbKl.Text;
            if (selected == "")
            {
                MessageBox.Show("Kies een klant voor het maken van de factuur");
            }
            else
            {
                
                string id = cmbKl.SelectedValue.ToString();
                var klt = Convert.ToInt32(id);

                string file = ($"{selected}{Datum:yyyy.M.dd}.docx");
                //string file2 = ($"{selected}{Datum:yyyy.M.dd}.pdf");
                string dat = Datum.ToString("dd  MMMM  yyyy");
                string title = "Factuur ";
                Formatting titleFormat = new Formatting();
                //Specify font family  
                titleFormat.FontFamily = new Xceed.Document.NET.Font("arial");
                //Specify font size  
                titleFormat.Size = 25D;
                titleFormat.Position = 30;
                titleFormat.FontColor = System.Drawing.Color.Orange;
                titleFormat.UnderlineColor = System.Drawing.Color.Gray;
                titleFormat.Italic = true;
                titleFormat.Bold = true;
                titleFormat.Spacing = 20;


                Formatting textParagraphFormat = new Formatting();
                textParagraphFormat.FontFamily = new Xceed.Document.NET.Font("times new roman");
                textParagraphFormat.Size = 20D;
                textParagraphFormat.Spacing = 1;


                //string fileName = @"firstpage.docx";
                var doc = DocX.Create(file);
                // doc.InsertParagraph("Factuur");
                Table v = doc.AddTable(1, 2);
                v.Alignment = Alignment.center;
                v.Design = TableDesign.ColorfulList;



                Image img = doc.AddImage(@"C:\Users\HP\source\repos\ProjectInlog\ProjectInlog\Images\meubels.png");
                Picture pic = img.CreatePicture();
                Paragraph p1 = doc.InsertParagraph();
                p1.AppendPicture(pic);
                // Paragraph.AppendPicture(pic);
                Paragraph paragraphTitle = doc.InsertParagraph(title, false, titleFormat);

                //v.Rows[0].Cells[0].Paragraphs.First().Append(p1);
                //v.Rows[0].Cells[1].Paragraphs.First().Append("BB");



                using (ProjectContext ctx = new ProjectContext())
                {
                    //DataRow LastRow = ctx.Invoices.Rows.Count() ;
                    //var aan = Convert.ToInt32(ctx.Invoices.LastOrDefault());


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
                        (b, c) => new
                        {
                            Name = b.alb.Description,
                            Price = b.alb.Price,
                            Aantal = b.sa.s.O_Aantal,
                            Kl = b.sa.a.ClientId,
                            Ordr = b.sa.a.OrderId,
                            KL_name = c.C_Name,
                            KL_adres = c.C_Adress,
                            KL_woonplaats = c.C_Woonplaats,
                            KL_postcode = c.C_PostCode,
                            KL_btw = c.C_BtwNr,
                            Tot = Math.Round((b.alb.Price * b.sa.s.O_Aantal), 2)
                        }).ToList();

                    if (ok.Count != 0)
                    {
                        //var eind = false;
                        var tel = 0;
                        double tota = 0;
                        var tt = 0;
                        //
                        //var ord = 0;
                        Table t = doc.AddTable(21, 5);
                        t.Alignment = Alignment.center;
                        t.Design = TableDesign.ColorfulList;
                        var i = 1;
                        foreach (var item in ok)
                        {

                            if (tel == 0)
                            {
                                var gem = item.KL_postcode + " " + item.KL_woonplaats;
                                doc.InsertParagraph($"FACTUUR { item.Ordr}                                                                                                         FURNITURE");
                                doc.InsertParagraph();
                                doc.InsertParagraph($"{item.KL_name}                                                                                                                        Industrieweg 20");
                                doc.InsertParagraph($"{item.KL_adres}                                                                                                                2440 GEEL");
                                doc.InsertParagraph(gem);
                                doc.InsertParagraph($"BTW {item.KL_btw}");
                                doc.InsertParagraph();

                                tel = 1;
                                t.Rows[0].Cells[0].Paragraphs.First().Append("Omschrijving");
                                t.Rows[0].Cells[1].Paragraphs.First().Append("Aantal");
                                t.Rows[0].Cells[2].Paragraphs.First().Append("EenheidsPrijs");
                                t.Rows[0].Cells[3].Paragraphs.First().Append("Totaal");
                                t.Rows[0].Cells[4].Paragraphs.First().Append("Order");
                                tt = item.Ordr;
                            }



                            //Create Table with 24 rows and 5 columns.  

                            //Fill cells by adding text.  
                            t.Rows[i].Cells[0].Paragraphs.First().Append(item.Name);
                            t.Rows[i].Cells[1].Paragraphs.First().Append(item.Aantal.ToString());
                            t.Rows[i].Cells[2].Paragraphs.First().Append("€" + item.Price.ToString());
                            t.Rows[i].Cells[3].Paragraphs.First().Append("€" + item.Tot.ToString());
                            t.Rows[i].Cells[4].Paragraphs.First().Append(item.Ordr.ToString());
                            tota += item.Tot;
                            i += 1;

                        }
                        ctx.Invoices.Add(new Invoice()
                        {
                            Amount = tota,
                            Status = false,
                            CreatedAt = Datum,
                            PayedAt = Datum,
                            OrderId = tt
                        });
                        ctx.SaveChanges();

                        t.Rows[18].Cells[2].Paragraphs.First().Append("Totaal zonder BTW");
                        t.Rows[18].Cells[3].Paragraphs.First().Append("€" + tota.ToString());
                        // t.Rows[21].Cells[0].);


                        var btw = Math.Round((tota * 0.21), 2);
                        t.Rows[19].Cells[2].Paragraphs.First().Append("BTW bedrag");
                        t.Rows[19].Cells[3].Paragraphs.First().Append("€" + btw.ToString());
                        var total = Math.Round((tota + btw), 2);
                        t.Rows[20].Cells[2].Paragraphs.First().Append("Totaal met BTW");
                        t.Rows[20].Cells[3].Paragraphs.First().Append("€" + total.ToString());
                        doc.InsertTable(t);

                        doc.InsertParagraph();
                        doc.InsertParagraph("Ondernemingsnummer: 0123 456 789");
                        doc.InsertParagraph("Bank              : ING Belgium NV");
                        doc.InsertParagraph("SWIFT/BIC         : BBRUBEBB");
                        doc.InsertParagraph("IBAN              : BE33 7894 6324 4587");


                        doc.Save();


                        

                        MessageBoxResult result = MessageBox.Show("Wenst u het factuur te beijken?", "Factuur aangemaakt", MessageBoxButton.YesNo, MessageBoxImage.Information);
                        if (result == MessageBoxResult.Yes)
                        {
                            //Process process = new Process();
                            //process.StartInfo.FileName = file;
                            //process.Start();

                            Process.Start("WINWORD.EXE", file);
                        }

                        // Process.Start("WINWORD.EXE", file);
                    }
                    else
                    {
                        MessageBox.Show("er is geen verkoop voor de gekozen klant");
                    }
                }
            }
        }
        public void Hoofding()
        {

        }

        private void btnEinde_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();

            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();


            this.Close();
        }
    }
}
