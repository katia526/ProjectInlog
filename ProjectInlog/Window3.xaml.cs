﻿using System;
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
                var col = ctx.Employees.Select(c => new { Id = c.UserId, Name = c.FirstName + " " + c.LastName }).ToList();
               
                cbUsers.ItemsSource = col;
                cbUsers.DisplayMemberPath = "Name";
                cbUsers.SelectedValuePath = "Id";
            }
        }
        

        private void cbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string id = cbUsers.SelectedValue.ToString();
            var del = Convert.ToInt32(id);

            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Employees.FirstOrDefault(c => c.UserId == del);
                txtFirstName.Text = col.FirstName;
                txtLastName.Text = col.LastName;
                txtEmail.Text = col.Email;
                //txtFunction.Text = col.Function;

            }

        }

        private void btnBewaar_Click(object sender, RoutedEventArgs e)
        {
            using (ProjectContext ctx = new ProjectContext())
            {
                ctx.Employees.FirstOrDefault(c => c.UserId == cbUsers.SelectedIndex).FirstName = txtFirstName.Text;
                ctx.Employees.FirstOrDefault(c => c.UserId == cbUsers.SelectedIndex).LastName = txtLastName.Text;
                ctx.Employees.FirstOrDefault(c => c.UserId == cbUsers.SelectedIndex).Email = txtEmail.Text;
                // ctx.Employees.FirstOrDefault(c => c.UserId == cbUsers.SelectedIndex).address = txtAddress.Text;

                ctx.SaveChanges();

            }
        }
        public class Employee
        {
            public int EmployeeId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        private void cbUsers_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string id = cbUsers.SelectedValue.ToString();
            var del = Convert.ToInt32(id);

            using (ProjectContext ctx = new ProjectContext())
            {
                var col = ctx.Employees.FirstOrDefault(c => c.UserId == del);
                txtFirstName.Text = col.FirstName;
                txtLastName.Text = col.LastName;
                txtEmail.Text = col.Email;
                //txtFunction.Text = col.Function;

            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
