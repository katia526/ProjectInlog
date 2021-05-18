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
                        S_Website = txtWebsite.Text

                    }); ;
                    ctx.SaveChanges();
                    txtNaam.Text = " ";
                    txtAdres.Text = " ";
                    txtPostCode.Text = " ";
                    txtWoonplaats.Text = " ";
                    txtContact.Text = " ";
                    txtTelefoon.Text = " ";
                    txtEmail.Text = " ";
                    txtWebsite.Text = " ";
                }
            }
           
        }

        private void txtEditor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void lbLeveranciers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
        public void LaadLeveranciers()
        {
           
        }

        private void TabItem_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
           
        }

        private void TabItem_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            LaadLeveranciers();
        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
           
           
                Laad();
           
        }

        private void lbLeveranciers_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //txtNaamBedrijf = lbLeveranciers.SelectedValue

           


        }

        private void lbLeveranciers_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
          if (lbLeveranciers.SelectedValue.ToString() != null)
           {
                string id = lbLeveranciers.SelectedValue.ToString();
                var del = Convert.ToInt32(id);

                using (ProjectContext ctx = new ProjectContext())
                {
                    var col = ctx.Supplier.FirstOrDefault(c => c.SupplierId == del);
                    txtNaamBedrijf.Text = col.S_Name;
                    txtAdres.Text = col.S_Address;
                    txtWoonplaatsBedrijf.Text = col.s_City;
                }
           }
        }

        private void btnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            string id = lbLeveranciers.SelectedValue.ToString();
            var del = Convert.ToInt32(id);
            using (ProjectContext ctx = new ProjectContext())
            {
                ctx.Supplier.Remove(ctx.Supplier.FirstOrDefault(c => c.SupplierId == del));

                ctx.SaveChanges();
                Laad();
            }
        }
        //private bool UserFilter(object item)
        //{
        //    if (String.IsNullOrEmpty(txtFilter.Text))
        //        return true;
        //    else
        //        return ((item as User).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        //}
        //public class User
        //{
        //    public string Name { get; set; }

        //    public int Id { get; set; }

        //}

        //private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    CollectionViewSource.GetDefaultView(lbLeveranciers.ItemsSource).Refresh();
        //}
        public void Laad()
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Supplier.Select(c => new { Id = c.SupplierId, Name = c.S_Name }).ToList();
                if (col != null)
                {
                    lbLeveranciers.ItemsSource = col;
                    lbLeveranciers.DisplayMemberPath = "Name";
                    lbLeveranciers.SelectedValuePath = "Id";
                }
            }
              
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbLeverancier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Supplier.Select(c => new { Id = c.SupplierId, Name = c.S_Name }).ToList();
                if (col != null)
                {
                    cmbLeverancier.ItemsSource = col;
                    cmbLeverancier.DisplayMemberPath = "Name";
                    cmbLeverancier.SelectedValuePath = "Id";
                }
            }

            if (cmbLeverancier.SelectedValue.ToString() != null)
            {
                string id = cmbLeverancier.SelectedValue.ToString();
                var del = Convert.ToInt32(id);

                using (ProjectContext ctx = new ProjectContext())
                {
                    var col = ctx.Supplier.FirstOrDefault(c => c.SupplierId == del);
                    txtLevNaam.Text = col.S_Name;
                    txtLevAdres.Text = col.S_Address;
                    txtLevContact.Text = col.s_Contact;
                    txtLevEmail.Text = col.S_Email;
                    txtLevPostcode.Text = col.s_PostCode;
                    txtLevTelefoon.Text = col.S_Phone;
                    txtLevWebsite.Text = col.S_Website;
                    txtLevWoonplaats.Text = col.s_City;
                                        
                }
            }
        }

        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Supplier.Select(c => new { Id = c.SupplierId, Name = c.S_Name }).ToList();
                if (col != null)
                {
                    cmbLeverancier.ItemsSource = col;
                    cmbLeverancier.DisplayMemberPath = "Name";
                    cmbLeverancier.SelectedValuePath = "Id";
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string id = cmbLeverancier.SelectedValue.ToString();
            var del = Convert.ToInt32(id);
            using (ProjectContext ctx = new ProjectContext())
            {
                Supplier S_tewijzigen = ctx.Supplier.Where(c => c.SupplierId == del).FirstOrDefault();
                S_tewijzigen.S_Name = txtLevNaam.Text;
                S_tewijzigen.s_Contact = txtLevContact.Text;
                S_tewijzigen.s_City = txtLevWoonplaats.Text;
                S_tewijzigen.S_Email = txtLevEmail.Text;
                S_tewijzigen.S_Address = txtLevAdres.Text;
                S_tewijzigen.s_PostCode = txtLevPostcode.Text;
                S_tewijzigen.S_Website = txtLevWebsite.Text;

                ctx.SaveChanges();
                txtLevNaam.Text = " ";
                txtLevAdres.Text = " ";
                txtLevContact.Text = " ";
                txtLevEmail.Text = " ";
                txtLevPostcode.Text = " ";
                txtLevTelefoon.Text = " ";
                txtLevWebsite.Text = " ";
                txtLevWoonplaats.Text = " ";
            }
        }

        private void btnEinde_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnAlfa_Click(object sender, RoutedEventArgs e)
        {

            txtHoofd.Text = "Alfabetisch gesorteerd";
            using (ProjectContext ctx = new ProjectContext())
            {
              //  var col = ctx.Supplier.Select(c => new { Id = c.SupplierId, Name = c.S_Name }).ToList();
                var col = ctx.Supplier.OrderBy(c => c.S_Name ).ToList();
                if (col != null)
                {
                    lstLev.ItemsSource = col;
                    //lstLev.DisplayMemberPath = "Name";
                    //lstLev.SelectedValuePath = "Id";
                }
            }
        }

        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            if (txtPost.Text == null)
            {
                txtHoofd.Text = "Vul een postcode in";
                txtPost.Focus();
            }
            else
            {
                using (ProjectContext ctx = new ProjectContext())
                {
                    // var col = ctx.Supplier.AddRange(ctx.Supplier.Where(c => c.s_PostCode == txtPost.Text)).ToList();

                    //ctx.Supplier.Select(p => new {
                    //    p,
                    //    leeftijdscategorie = (p.age >= 40) ? "Ouder dan 40" : "Jonger dan 40";




                    var col = ctx.Supplier.Where(c => c.s_PostCode == txtPost.Text)
                        .Select(s => new { Name = s.S_Name, Id = s.SupplierId}).ToList();
                    txtHoofd.Text = txtPost.Text;
                    lstLev.ItemsSource = col;
                }
                   
            }
        }
    }
}
