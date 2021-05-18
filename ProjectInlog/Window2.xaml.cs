using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Text = " ";
            txtLastName.Text = " ";
            txtEmail.Text = " ";
            txtFunctie.Text = " ";
            txtPassword.Text = " ";
            txtUserName.Text = " ";
            txtAddress.Text = " ";
            txtError.Text = " ";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string sel = " ";
          

            string pattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
            Regex rg = new Regex(pattern);
            Match match = rg.Match(txtPassword.Text);
            if (txtFirstName.Text == null)
            {
                txtError.Text = "Geef een voornaam";
                txtFirstName.Focus();
            }
            if (txtLastName.Text == null)
            {
                txtError.Text = "Achternaam ontbreekt";
                txtLastName.Focus();
            }
            if (txtEmail.Text == null)
            {
                txtError.Text = "Vul het email adres in ";
                txtEmail.Focus();
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                txtError.Text = "Email voldoet niet aan vereiste. ";
                txtEmail.Focus();
            }
            if (txtPassword.Text == null)
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
            if (txtUserName.Text == null)
            {
                txtError.Text = "vul de Usernaam in";
                txtUserName.Focus();
            }
            using (ProjectContext ctx = new ProjectContext())
            {
                var result = ctx.Employees.FirstOrDefault(c => c.UserName == txtUserName.Text && c.Password == txtPassword.Text);
                if (result != null)
                {
                    txtError.Text = "Gebruiker bestaat reeds";

                }
                else
                {
                   
                    if (txtFunctie.Text == "magazijnier")
                    {
                        sel = "1";
                    }
                    else if (txtFunctie.Text == "verkoper")
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
                Password = txtPassword.Text,
                UserName = txtUserName.Text
                });

                    ctx.SaveChanges();

                    txtFirstName.Text = " ";
                    txtLastName.Text = " ";
                    txtEmail.Text = " ";
                    txtFunctie.Text = " ";
                    txtPassword.Text = " ";
                    txtUserName.Text = " ";
                    txtAddress.Text = " ";
                    txtError.Text = " ";
            
            }
   
        }
        //public class SecurityHelper
        //{
        //    public static string GenerateSalt(int nSalt)
        //    {
        //        var saltBytes = new byte[nSalt];

        //        using (var provider = new RNGCryptoServiceProvider())
        //        {
        //            provider.GetNonZeroBytes(saltBytes);
        //        }

        //        return Convert.ToBase64String(saltBytes);
        //    }

        //    public static string HashPassword(string password, string salt, int nIterations, int nHash)
        //    {
        //        var saltBytes = Convert.FromBase64String(salt);

        //        using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations))
        //        {
        //            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
        //        }
        //    }
        //}
    }
}
