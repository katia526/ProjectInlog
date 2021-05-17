using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectInlog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //using (ProjectContext ctx = new ProjectContext())
            //{
            //    ctx.Employees.Add(new Employee()
            //    {
            //        FirstName = "Jef",
            //        LastName = "verachtert",
            //        Function = "1",
            //        Email = "katia@hotmail.com",
            //        Password = "abcd",
            //        UserName = "Jefke",
            //        Address = "Markt 5",
            //    });
            //    ctx.Employees.Add(new Employee()
            //    {
            //        FirstName = "Mieke",
            //        LastName = "ver",
            //        Function = "2",
            //        Email = "m@hotmail.com",
            //        Password = "MIaz56%%",
            //        UserName = "Mieke",
            //        Address = "Markt 5",
            //    });
            //    ctx.Employees.Add(new Employee()
            //    {
            //        FirstName = "Louis",
            //        LastName = "p",
            //        Function = "3",
            //        Email = "p@hotmail.com",
            //        Password = "MIaz56%%",
            //        UserName = "Louis",
            //        Address = "Markt 5",
            //    });

            //    ctx.Clients.Add(new Client()
            //    {
            //        C_Name = "Biermans",
            //        C_Adress = "Markt 2",
            //        C_City = "2440 Geel",
            //        C_CreatedAt = new DateTime(year: 2021, month: 5, day: 03)

            //    });
            //    ctx.SaveChanges();
            //}

        }
        public class Employee
        {
            [Key]
            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Function { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
            public string Address { get; set; }
            public Client Client { get; set; }

        }
        public class Client
        {
            [Key]
            public int ClientId { get; set; }
            public string C_Name { get; set; }
            public string C_Adress { get; set; }
            public string C_City { get; set; }
            public string C_Phone { get; set; }
            public string TVA { get; set; }
            public string Representative { get; set; }
            [DataType(DataType.Date)]
            public DateTime C_CreatedAt { get; set; } = DateTime.Now;

        }

        public class Supplier
        {
            [Key]
            public int SupplierId { get; set; }
            public string S_Name { get; set; }
            public string S_Address { get; set; }
            public string s_PostCode { get; set; }
            public string s_City { get; set; }
            public string s_Contact { get; set; }
            public string S_Phone { get; set; }
            public string S_Email { get; set; }
         

            [DataType(DataType.Date)]
            public DateTime S_CreatedAt { get; set; } = DateTime.Now;

        }
        public class Product
        {
            [Key]
            public int ProductId { get; set; }
            public int P_SupplierId { get; set; }
            public string Description { get; set; }
            public int stock { get; set; }
            public int Ordered { get; set; }
            public string Place { get; set; }
            public double Price { get; set; }
            [DataType(DataType.Date)]
            public DateTime P_CreatedAt { get; set; } = DateTime.Now;
            [DataType(DataType.Date)]
            public DateTime P_UpdatedAt { get; set; } = DateTime.Now;

            public Supplier Supplier { get; set; }


        }
        public class Order
        {
            [Key]
            public int OrderId { get; set; }
            public int O_ClientId { get; set; }
            public int Orderline { get; set; }
            [DataType(DataType.Date)]
            public DateTime OrderedAt { get; set; } = DateTime.Now;
            [DataType(DataType.Date)]
            public DateTime DeliveredAt { get; set; } = DateTime.Now;
            public bool Invoice { get; set; }

            public Client Client { get; set; }
        }

        public class Invoice
        {
            [Key]
            public int InvoiceId { get; set; }
            public int I_OrderId { get; set; }
            public int I_ClientId { get; set; }
            public bool Status { get; set; }

            [DataType(DataType.Date)]
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            [DataType(DataType.Date)]
            public DateTime PayedAt { get; set; } = DateTime.Now;
            public Order order { get; set; }
            public Client client { get; set; }

        }
        public class OrderLine
        {
            [Key]
            public int O_OrderId { get; set; }
            public int O_ProductId { get; set; }

            public Order Order { get; set; }
            public Product Product { get; set; }
        }
        public class ProjectContext : DbContext
        {
            public ProjectContext() : base("name=ProjectDBConnectString")
            {
                // Database.SetInitializer(new CreateDatabaseIfNotExists<ProjectContext>());
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ProjectContext>());
                //  Database.SetInitializer(new DropCreateDatabaseAlways<ProjectContext>());
            }

            public DbSet<Employee> Employees { get; set; }
            public DbSet<Client> Clients { get; set; }
            public DbSet<Supplier> Supplier { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Invoice> Invoices { get; set; }
            public DbSet<OrderLine> OrderLines { get; set; }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                var result = ctx.Employees.FirstOrDefault(c => c.UserName == txtUserName.Text && c.Password == txtPassword.Password);
                
                if (result != null && result.Function == "1")
                {
                    
                    ScrAdministrator scrAdministrator = new ScrAdministrator();

                    scrAdministrator.Show();
                }
                //string pwd = txtPassword.Password;
                //string salt = SecurityHelper.GenerateSalt(30);
                //string pwdHashed = SecurityHelper.HashPassword(pwd, salt, 10101, 30);
                //result = ctx.Employees.FirstOrDefault(c => c.UserName == txtUserName.Text && c.Password == pwdHashed);
                result = ctx.Employees.FirstOrDefault(c => c.UserName == txtUserName.Text && c.Password == txtPassword.Password);
               
                if (result != null && result.Function == "2")
                {

                    Magazijniers magazijniers = new Magazijniers();
                    magazijniers.Show();
                }
                if (result != null && result.Function == "3")
                {

                    Vertegenwoordigers vertegenwoordigers = new Vertegenwoordigers();

                    vertegenwoordigers.Show();
                }
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
