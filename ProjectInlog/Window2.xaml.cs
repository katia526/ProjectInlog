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

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstName.Text == null)
            {
                txtError.Text = "Geef een voornaam";
                txtFirstName.Focus();
            }
            else if (txtLastName.Text == null)
            {
                txtError.Text = "Achternaam ontbreekt";
                txtLastName.Focus();
            }
            else if (txtEmail.Text == null)
            {
                txtError.Text = "Vul het email adres in ";
                txtEmail.Focus();
            }
            else if (txtPassword.Text == null)
            {
                txtError.Text = "vul het paswoord in";
                txtPassword.Focus();
            }
         //   else if (!Regex.IsMatch(txtPassword.Text, "^(?=.*[0 - 9])(?=.*[a - z])(?=.*[A - Z])(?=.*[@$!% *? &])([a - zA - Z0 - 9@$!% *? &]{ 8,})$"))
               else if(!Regex.IsMatch(txtPassword.Text, "^(?=.*?[A - Z])(?=.*?[a - z])(?=.*?[0 - 9])(?=.*?[#?!@$%^&*-]).{8,}$")) 
                    {
                txtError.Text = "het paswoord voldoet niet aan de voorschriften";
                txtPassword.Focus();
            }
            else if (txtUserName.Text == null)
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
                    string sel = " ";
                    if (txtFunctie.Text == "magazijnier")
                    {
                        sel = "1";
                    }
                    else
                    {
                        sel = "2";
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
                }


            }
            
        }
    }
}
