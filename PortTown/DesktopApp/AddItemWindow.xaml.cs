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
    public partial class AddItemWindow : Window
    {
        public AddItemWindow()
        {
            InitializeComponent();
        }

        private void AddItemClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as AdminWindow).AddAddable(new Item(textbox1.Text, Int32.Parse(textbox2.Text)), (Application.Current.MainWindow as AdminWindow).itemTable, (Application.Current.MainWindow as AdminWindow).ItemTable);
            this.Hide();
        }
    }
}
