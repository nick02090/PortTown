using DesktopApp.Models;
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

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void AddUserClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as AdminWindow).AddAddable(new User(textbox1.Text, textbox2.Text, textbox3.Text, textbox4.Text), (Application.Current.MainWindow as AdminWindow).userTable, (Application.Current.MainWindow as AdminWindow).UserTable);
            this.Hide();
        }
    }
}
