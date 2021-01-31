using System;
using System.Collections.Generic;
using System.Data;
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
using DesktopApp.Models;
using System.Net.Http;
namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        //public Guid userId;
        public User User;
        public EditUserWindow(User user)
        {
            InitializeComponent();
            User = user;
        }

        private void EditUserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //var selectedRow = (Application.Current.MainWindow as AdminWindow).GetSelectedRows((Application.Current.MainWindow as AdminWindow).UserTable)[0].Row;

            //userId = (Guid)selectedRow[0];
            textbox1.Text = User.Username;
            textbox2.Text = User.Email;
            //textbox3.Text = selectedRow[3].ToString();
            textbox3.Text = "";
            textbox4.Text = User.Town.Name;
            textbox5.Text = User.Town.Level.ToString();
        }

        private async void EditUserClick(object sender, RoutedEventArgs e)
        {
            //Town newTown = new Town();
            User.Town.Name = textbox4.Text;
            User.Town.Level = Int32.Parse(textbox5.Text);
            await (Application.Current.MainWindow as AdminWindow).EditUser(User, new User(textbox1.Text, textbox2.Text, textbox3.Text));
            this.Hide();
        }
    }
}
