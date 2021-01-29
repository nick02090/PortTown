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
namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public EditUserWindow()
        {
            InitializeComponent();
        }

        private void EditUserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var selectedRow = (Application.Current.MainWindow as AdminWindow).GetSelectedRows((Application.Current.MainWindow as AdminWindow).UserTable)[0].Row;

            textbox1.Text = selectedRow[1].ToString();
            textbox2.Text = selectedRow[2].ToString();
            textbox3.Text = selectedRow[3].ToString();
        }

        private void EditUserClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as AdminWindow).EditAddable(new User(textbox1.Text, textbox2.Text, textbox3.Text), (Application.Current.MainWindow as AdminWindow).userTable, (Application.Current.MainWindow as AdminWindow).UserTable);
            this.Hide();
        }
    }
}
