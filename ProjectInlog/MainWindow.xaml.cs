using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

            //DateTime DatumUit = DateTime.Now.ToString("MM/dd/yyyy");
            DateTime DatumUit = new DateTime(1900, 01, 01);

            //using (ProjectContext ctx = new ProjectContext())
            //{
            //    ctx.Employees.Add(new Employee()
            //    {
            //        FirstName = "Jef",
            //        LastName = "verachtert",
            //        Function = "1",
            //        Email = "katia@hotmail.com",
            //        Password = "178182158133202201218218",
            //        UserName = "Jefke",
            //        Address = "Markt 5",
            //        PostCode = "2440",
            //        Woonplaats = "Geel",
            //        DatumIn = new DateTime(year: 2018, month: 5, day: 03)
            //    });
            //    ctx.Employees.Add(new Employee()
            //    {
            //        FirstName = "Mieke",
            //        LastName = "ver",
            //        Function = "2",
            //        Email = "m@hotmail.com",
            //        Password = "178182158133202201218218",
            //        UserName = "Mieke",
            //        Address = "Markt 5",
            //        PostCode = "2440",
            //        Woonplaats = "Geel",
            //        DatumIn = new DateTime(year: 2019, month: 5, day: 03)
            //    });
            //    ctx.Employees.Add(new Employee()
            //    {
            //        FirstName = "Louis",
            //        LastName = "p",
            //        Function = "3",
            //        Email = "p@hotmail.com",
            //        Password = "178182158133202201218218",
            //        UserName = "Louis",
            //        Address = "Markt 5",
            //        PostCode = "2440",
            //        Woonplaats = "Geel",
            //        DatumIn = new DateTime(year: 2020, month: 5, day: 03)
            //    });

            //    ctx.Clients.Add(new Client()
            //    {
            //        C_Name = "Biermans",
            //        C_Adress = "Markt 2",
            //        C_PostCode = "2440",
            //        C_Woonplaats = "Geel",
            //        C_CreatedAt = new DateTime(year: 2021, month: 5, day: 03)

            //    });

            //    ctx.Supplier.Add(new Supplier()
            //    {
            //        S_Name = "Dams",
            //        S_Address = "Logen 56",
            //        s_PostCode = "2440",
            //        s_City = "Geel",
            //        s_Contact = "Jef",
            //        S_Phone = "014 - 56 56 56",
            //        S_Email = "jef@dams.be",
            //        S_Website = "www.dams.be",
            //        S_CreatedAt = new DateTime(year: 2021, month: 5, day: 03)

            //    });

                //ctx.Products.Add(new Product()
                //{
                //    SupplierId = 1,
                //    Description = "MelkChocolade",
                //    Stock = 10,
                //    Ordered = 5,
                //    Place = "1A2B",
                //    Price = 1.45,

                //    P_CreatedAt = new DateTime(year: 2021, month: 5, day: 03)

                //});
                //ctx.Products.Add(new Product()
                //{
                //    SupplierId = 1,
                //    Description = "PureChocolade",
                //    Stock = 10,
                //    Ordered = 5,
                //    Place = "1A2C",
                //    Price = 1.99,

                //    P_CreatedAt = new DateTime(year: 2021, month: 5, day: 03)

                //});


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
            public string Woonplaats { get; set; }
            public string Telefoon { get; set; }
            public string PostCode { get; set; }


            [DataType(DataType.Date)]
            public DateTime DatumIn { get; set; } = DateTime.Now;

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            [Display(Name = "Date")]
            public DateTime? DatumUit { get; set; }

            //[DataType(DataType.Date)]
            //public DateTime DatumUit { get; set; } 


            public Client Client { get; set; }
        }
        public class Client
        {
            [Key]
            public int ClientId { get; set; }
            public string C_Name { get; set; }
            public string C_Adress { get; set; }
            public string C_Woonplaats { get; set; }
            public string C_PostCode { get; set; }
            public string C_Phone { get; set; }
            public string C_BtwNr { get; set; }
            public string C_Verkoper { get; set; }
            public string C_Email { get; set; }
            public string C_Contact { get; set; }
            [DataType(DataType.Date)]
            public DateTime C_CreatedAt { get; set; } = DateTime.Now;
            [DataType(DataType.Date)]
            public DateTime C_ChangedAt { get; set; } = DateTime.Now;
            public int UserId { get; set; }

            
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
            public string S_Website { get; set; }
         

            [DataType(DataType.Date)]
            public DateTime S_CreatedAt { get; set; } = DateTime.Now;

        }
        public class Product
        {
            [Key]
            public int ProductId { get; set; }
            public int SupplierId { get; set; }
            public string Description { get; set; }
            public int Stock { get; set; }
            public int Ordered { get; set; }
            public string Place { get; set; }
            public double Price { get; set; }
            [DataType(DataType.Date)]
            public DateTime P_CreatedAt { get; set; } = DateTime.Now;
            [DataType(DataType.Date)]
            public DateTime P_UpdatedAt { get; set; } = DateTime.Now;

            public Supplier Supplier { get; set; }
            //public OrderLine OrderLine { get; set; }

        }
        public class Order
        {
            [Key]
            public int OrderId { get; set; }
            public int ClientId { get; set; }
            public int VerkId { get; set; }
           
            [DataType(DataType.Date)]
            public DateTime OrderedAt { get; set; } = DateTime.Now;
            [DataType(DataType.Date)]
            public DateTime DeliveredAt { get; set; } = DateTime.Now;
            public bool Invoice { get; set; }

            public Client Client { get; set; }
            //public OrderLine OrderLine { get; set; }
            public virtual ICollection<OrderLine> OrderLine { get; set; }
            public virtual Invoice Invoices { get; set; }
        }
        public class OrderLine
        {
            //[Key]
           // public int OrderLineId { get; set; }
            //[Key, Column(Order = 1)]

            //[Key, Column(Order = 2)]

            public int O_Aantal { get; set; }

            //[ForeignKey("Order")]
            [Key, Column(Order = 1)]
            public int OrderId { get; set; }
            public virtual Order Order { get; set; }
            //[ForeignKey("Product")]
            [Key, Column(Order = 2)]
            public int ProductId { get; set; }
            public virtual Product Product { get; set; }
            public object OrderLin { get; internal set; }
            //public ICollection<Order> Orders { get; set; }
        }

        public class Invoice
        {
            //[ForeignKey("Order")]
            [Key]
            public int InvoiceId { get; set; }
           
            public int OrderId { get; set; }
            //public int ClientId { get; set; }
            public double Amount { get; set; }
            public bool Status { get; set; }

            [DataType(DataType.Date)]
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            [DataType(DataType.Date)]
            public DateTime PayedAt { get; set; } = DateTime.Now;
            //[Required]
            //public virtual Order order { get; set; }
            //public virtual Client client { get; set; }

        }
       
        public class ProjectContext : DbContext
        {
            public ProjectContext() : base("name=ProjectDBConnectString")
            {
                //Database.SetInitializer(new CreateDatabaseIfNotExists<ProjectContext>());
               Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ProjectContext>());
               //Database.SetInitializer(new DropCreateDatabaseAlways<ProjectContext>());
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
            if (txtUserName.Text == "" || txtPassword.Password == "")
            {
                MessageBox.Show("vul paswoord en username in");
                txtUserName.Focus();
            }
            else
            {
                Login();
            }
        }
        private void Login()
        { 
            using (ProjectContext ctx = new ProjectContext())
            {

                string pass = Encryptor(txtPassword.Password);
                var result = ctx.Employees.FirstOrDefault(c => c.UserName == txtUserName.Text && c.Password == pass);
                if (result == null)
                {
                    MessageBox.Show("fout paswoord of username");
                    txtUserName.Focus();
                }
                
                if (result != null && result.Function == "1")
                {
                   

                    this.Hide();

                    ScrAdministrator scrAdministrator = new ScrAdministrator();

                    scrAdministrator.Show();

                    
                    this.Close();


                }
  
               
                if (result != null && result.Function == "2")
                {
                    this.Hide();
                    magazijnTab magazijnTab = new magazijnTab();
                    magazijnTab.Show();
                    this.Close();
                }
                if (result != null && result.Function == "3")
                {
                    this.Hide();
                    Verkopers verkopers = new Verkopers();

                    verkopers.Show();
                    this.Close();
                }
            }
        }
     
        public class thisDate1
        {
            public DateTime thisdate { get; set; }
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

        private void btnexit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
