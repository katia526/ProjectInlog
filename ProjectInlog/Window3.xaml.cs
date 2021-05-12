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
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
            using (ProjectContext ctx = new ProjectContext())
            {
                //var col = ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName });

                ////var col = ctx.Albums.Select(c => new { Id = c.AlbumId, Name = c.Name }).ToList();
                //// var col = ctx.Albums.Select(c => c.A_ArtistId == (Convert.ToInt32(cbArtiest.SelectedItem))
                ////     .Where(c => new { Id = c.AlbumId, Name = c.Name, Aid = c.A_ArtistId }).ToList();
                ////   var col = ctx.Albums.Select(c => c.A_ArtistId == index);

                ////cbAlbum.DataSource = col;
                ////cbAlbum.DisplayMember = "Name";
                ////cbAlbum.ValueMember = "Id";


                //cbUsers.DataSource = col;
                //cbUsers.DisplayMember = "Name";
                //cbUsers.
                this.DataContext = ctx.Employees;
               
            }
        }
       public class Employee
        {
            public int EmployeeId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}
