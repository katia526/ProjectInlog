using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {

        public Administrator()
        {
            InitializeComponent();

        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName }).ToList();

                cbVerwijder.ItemsSource = col;
                cbVerwijder.DisplayMemberPath = "Name";
                cbVerwijder.SelectedValuePath = "Id";
            }
        }

        private void btnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = cbVerwijder.SelectedValue.ToString();
                var del = Convert.ToInt32(id);
                using (ProjectContext ctx = new ProjectContext())
                {
                    ctx.Employees.Remove(ctx.Employees.FirstOrDefault(c => c.UserId == del));

                    ctx.SaveChanges();
                    MessageBox.Show("de gekozen gebruiker werd verwijderd");
                    
                        var col = ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName }).ToList();

                        cbVerwijder.ItemsSource = col;
                        cbVerwijder.DisplayMemberPath = "Name";
                        cbVerwijder.SelectedValuePath = "Id";
                   
                   
                }
            }
            catch (Exception)
            {
                MessageBox.Show("kies een medewerker");
            }
        }

        private void cmbWerknemer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string id = cmbWerknemer.SelectedValue.ToString();
                var del = Convert.ToInt32(id);

                using (ProjectContext ctx = new ProjectContext())
                {
                    var col = ctx.Employees.FirstOrDefault(c => c.UserId == del);

                    txtWrkEmail.Text = col.Email;

                    txtWrkAdres.Text = col.Address;
                    txtWrkPostcode.Text = col.PostCode;
                    txtWrkTelefoon.Text = col.Telefoon;
                    txtWrkFirstName.Text = col.FirstName;
                    txtWrkLastName.Text = col.LastName;
                    txtWrkWoonplaats.Text = col.Woonplaats;
                    cmbFunctie.SelectedIndex = Convert.ToInt32(col.Function);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("kies een medewerker");
            }
        }

        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName }).ToList();
                cmbWerknemer.ItemsSource = col;
                cmbWerknemer.DisplayMemberPath = "Name";
                cmbWerknemer.SelectedValuePath = "Id";
            }
        }

        private void btnBewaar_Click(object sender, RoutedEventArgs e)
        {
            var ok = 0;
            string sel = " ";
            txtError.Text = " ";

            string pattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
            Regex rg = new Regex(pattern);
            Match match = rg.Match(txtPassword.Text);
            if (txtFirstName.Text == "")
            {
                txtError.Visibility = Visibility.Visible;

                txtError.Text = "Geef een voornaam";
                MessageBox.Show("vul een naam in");

                txtFirstName.Focus();
            }

            else if (txtLastName.Text == "")
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Achternaam ontbreekt";
                MessageBox.Show("vul een naam in");
                txtLastName.Focus();

            }
            else if (txtUserName.Text == "")
            {
                txtError.Text = "vul de Usernaam in";
                MessageBox.Show("vul een usernaam in ");
                txtUserName.Focus();
            }

            else if (txtPassword.Text == "")
            {
                txtError.Text = "vul het paswoord in";
                MessageBox.Show("vul een paswoord in");
                txtPassword.Focus();
            }

            else if (!match.Success)
            {
                txtError.Text = "het paswoord voldoet niet aan de voorschriften";
                MessageBox.Show("het paswoord voldoet niet aan de voorschriften, minstens 8 karakters, 1 kleine letter, 1 hoofdletter, 1 cijfer, 1 vreemd teken");


                txtPassword.Focus();
            }
            else if (txtEmail.Text == "")
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Vul het email adres in ";
                MessageBox.Show("vul email in");
                txtEmail.Focus();
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Email voldoet niet aan vereiste. ";
                MessageBox.Show("Email voldoet in aan de vereisten");
                txtEmail.Focus();
            }

            else
            {

                using (ProjectContext ctx = new ProjectContext())
                {
                    var result = ctx.Employees.FirstOrDefault(c => c.UserName == txtUserName.Text && c.Password == txtPassword.Text);
                    if (result != null)
                    {
                        txtError.Text = "Gebruiker bestaat reeds";

                    }
                    else
                    {

                        if (cmbFunctie.Text == "magazijnier")
                        {
                            sel = "1";
                        }
                        else if (cmbFunctie.Text == "verkoper")
                        {
                            sel = "2";
                        }
                        else
                        {
                            sel = "3";
                        }
                    }
                    ctx.Employees.Add(new Employee()
                    {
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Email = txtEmail.Text,
                        Function = sel,
                        Password = Encryptor(txtPassword.Text),
                        UserName = txtUserName.Text,
                        Address = txtAdres.Text,
                        Woonplaats = txtWoonplaats.Text,
                        Telefoon = txtTelefoon.Text,
                        PostCode = txtPostCode.Text,
                        DatumUit = new DateTime(year: 1900, month: 01, day: 01)

                    });

                    ctx.SaveChanges();

                    txtFirstName.Text = " ";
                    txtLastName.Text = " ";
                    txtEmail.Text = " ";
                    txtPostCode.Text = " ";
                    txtPassword.Text = " ";
                    txtUserName.Text = " ";
                    txtAdres.Text = " ";
                    txtError.Text = " ";
                    txtWoonplaats.Text = " ";
                    txtTelefoon.Text = " ";
                    txtWrkUitDienst.Text = " ";
                }
            }
        }
    
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string res = " ";
            if (cmbFunctie.Text == "Administrator")
            {
                res = "1";
            }
            else if (cmbFunctie.Text == "verkoper")
            {
                res = "2";
            }
            else
            {
                res = "3";
            }


            try
            {

                string id = cmbWerknemer.SelectedValue.ToString();
                var sel = Convert.ToInt32(id);


               
                using (ProjectContext ctx = new ProjectContext())
                {
                    Employee wijzigemp = ctx.Employees.Where(c => c.UserId == sel).FirstOrDefault();

                    if (txtWrkFirstName.Text != "")
                    {
                        wijzigemp.FirstName = txtWrkFirstName.Text;
                    }
                    if (txtWrkLastName.Text != "")
                    {
                        wijzigemp.LastName = txtWrkLastName.Text;
                    }
                    if (txtWrkPostcode.Text != "")
                    {
                        wijzigemp.PostCode = txtWrkPostcode.Text;
                    }
                    if (txtWrkTelefoon.Text != "")
                    {
                        wijzigemp.Telefoon = txtWrkTelefoon.Text;
                    }
                    if (res != "")
                    {
                        wijzigemp.Function = res;
                    }
                    if (txtWrkAdres.Text != "")
                    {
                        wijzigemp.Address = txtWrkAdres.Text;
                    }
                    if (txtWrkWoonplaats.Text != "")
                    {
                        wijzigemp.Woonplaats = txtWrkWoonplaats.Text;
                    }
                    if (txtWrkEmail.Text != "")
                    {
                        wijzigemp.Email = txtWrkEmail.Text;
                    }
                    if (txtWrkUitDienst.Text != "")
                    {
                        wijzigemp.DatumUit = Convert.ToDateTime(txtWrkUitDienst.Text);
                    }

                    //ctx.Employees.FirstOrDefault(c => c.UserId == sel).FirstName = txtWrkFirstName.Text;
                    //ctx.Employees.FirstOrDefault(c => c.UserId == sel).LastName = txtWrkLastName.Text;
                    //ctx.Employees.FirstOrDefault(c => c.UserId == sel).Email = txtWrkEmail.Text;
                    //ctx.Employees.FirstOrDefault(c => c.UserId == sel).Function = res;
                    //ctx.Employees.FirstOrDefault(c => c.UserId == sel).Address = txtWrkAdres.Text;
                    //ctx.Employees.FirstOrDefault(c => c.UserId == sel).PostCode = txtWrkPostcode.Text;
                    //ctx.Employees.FirstOrDefault(c => c.UserId == sel).Telefoon = txtWrkTelefoon.Text;
                    //ctx.Employees.FirstOrDefault(c => c.UserId == sel).Woonplaats = txtWrkWoonplaats.Text;
                    //ctx.Employees.FirstOrDefault(c => c.UserId == sel).DatumUit = Convert.ToDateTime(txtWrkUitDienst.Text);
                    ctx.SaveChanges();

                    //new DateTime(Convert.ToDateTime(txtWrkUitDienst.Text)).ToString("dd/MM/yyyy")

                    txtWrkFirstName.Text = " ";
                    txtWrkLastName.Text = " ";
                    txtWrkEmail.Text = " ";
                    txtWrkAdres.Text = " ";
                    txtWrkPostcode.Text = " ";
                    txtWrkTelefoon.Text = " ";
                    txtWrkWoonplaats.Text = " ";
                    txtWrkUitDienst.Text = " ";
                }
            }
            catch(Exception)
            {
                MessageBox.Show("vul een naam in");
            }
        }

        private void cbFunctie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        static string Encryptor(string wachtwoord)
        {
            string encrypted = "";

            for (int i = 0; i < wachtwoord.Length; i++)
            {
                {
                    encrypted += 255 - wachtwoord[i];
                }
            }

            return encrypted;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnoverzicht_Click(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var colw = ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName, DateIn = (c.DatumIn).ToString("dd/MM/yyyy"), DateUit = (c.DatumUit) }).ToList();
                if (colw != null)
                {
                    lstOverzicht2.ItemsSource = colw;

                }
                else
                {
                    MessageBox.Show("lijst bevat geen elementen");
                }
            }
        }

        private void TabItem_Loaded_3(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var colw = ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName, DateIn = c.DatumIn, DateUit = c.DatumUit }).ToList();
                if (colw != null)
                {
                    lstOverzicht2.ItemsSource = colw;

                }
                else
                {
                    MessageBox.Show("lijst bevat geen elementen");
                }
            }
        }

        private void TabItem_Loaded_2(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var colx = ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName, Adres = c.Address, Woonplaats = c.Woonplaats, Tel = c.Telefoon, em = c.Email }).ToList();
                if (colx != null)
                {
                    lstOverzicht.ItemsSource = colx;

                }
                else
                {
                    MessageBox.Show("lijst bevat geen elementen");
                }
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();

            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();


            
            this.Close();

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.Hide();

            Verkopers verkopers = new Verkopers();

            verkopers.Show();



            this.Close();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.Hide();

            
            Magazijniers magazijniers = new Magazijniers();
            magazijniers.Show();



            this.Close();
        }
    }

    internal class Date
    {
    }

}
