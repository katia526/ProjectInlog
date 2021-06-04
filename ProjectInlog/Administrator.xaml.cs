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
            string id = cbVerwijder.SelectedValue.ToString();
            var del = Convert.ToInt32(id);
            using (ProjectContext ctx = new ProjectContext())
            {
                ctx.Employees.Remove(ctx.Employees.FirstOrDefault(c => c.UserId == del));

                ctx.SaveChanges();
            }
        }

        private void cmbWerknemer_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName }).ToList();
                //BindingList<Employee> col = new BindingList<Employee>(MyMethodReturningList());
                cmbWerknemer.ItemsSource = col;
                cmbWerknemer.DisplayMemberPath = "Name";
                cmbWerknemer.SelectedValuePath = "Id";

            }
        }

        private void btnBewaar_Click(object sender, RoutedEventArgs e)
        {

            string sel = " ";
            txtError.Text = " ";

            string pattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
            Regex rg = new Regex(pattern);
            Match match = rg.Match(txtPassword.Text);
            if (txtFirstName.Text == "")
            {
              
                txtError.Visibility = Visibility.Visible;
               
                txtError.Background = Brushes.Yellow;
                txtError.Foreground = Brushes.Black;
                txtError.Text = "Geef een voornaam";
               
                //txtFirstName.Focus();
            }
            if (txtLastName.Text == "")
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Achternaam ontbreekt";
                txtLastName.Focus();
            }
            if (txtEmail.Text == "")
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Vul het email adres in ";
                txtEmail.Focus();
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Email voldoet niet aan vereiste. ";
                txtEmail.Focus();
            }
            if (txtPassword.Text == "")
            {
                txtError.Text = "vul het paswoord in";
                txtPassword.Focus();
            }

            else if (!match.Success)
            {
                txtError.Text = "het paswoord voldoet niet aan de voorschriften";
                txtPassword.Focus();
            }

            //else
            //{
            //    string pwd = txtPassword.Text;
            //    string salt = SecurityHelper.GenerateSalt(30);
            //    pwdHashed = SecurityHelper.HashPassword(pwd, salt, 10101, 30);
            //}
            if (txtUserName.Text == "")
            {
                txtError.Text = "vul de Usernaam in";
                txtUserName.Focus();
            }
            //string pass = Encryptor(txtPassword.Text);

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


            //string fu = cmbFunctie.SelectedValue.ToString();
           // var func = Convert.ToInt32(fu);

            string id = cmbWerknemer.SelectedValue.ToString();
            var sel = Convert.ToInt32(id);

            using (ProjectContext ctx = new ProjectContext())
            {
                ctx.Employees.FirstOrDefault(c => c.UserId == sel).FirstName = txtWrkFirstName.Text;
                ctx.Employees.FirstOrDefault(c => c.UserId == sel).LastName = txtWrkLastName.Text;
                ctx.Employees.FirstOrDefault(c => c.UserId == sel).Email = txtWrkEmail.Text;
                ctx.Employees.FirstOrDefault(c => c.UserId == sel).Function = res;
                ctx.Employees.FirstOrDefault(c => c.UserId == sel).Address = txtWrkAdres.Text;
                ctx.Employees.FirstOrDefault(c => c.UserId == sel).PostCode = txtWrkPostcode.Text;
                ctx.Employees.FirstOrDefault(c => c.UserId == sel).Telefoon = txtWrkTelefoon.Text;
                ctx.Employees.FirstOrDefault(c => c.UserId == sel).Woonplaats = txtWrkWoonplaats.Text;

                ctx.SaveChanges();

                txtWrkFirstName.Text = " ";
                txtWrkLastName.Text = " ";
                txtWrkEmail.Text = " ";
                txtWrkAdres.Text = " ";
                txtWrkPostcode.Text = " ";
                txtWrkTelefoon.Text = " ";
                txtWrkWoonplaats.Text = " ";
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

    }

    internal class Date
    {
    }

    
}
