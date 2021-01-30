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
    public partial class AddResourceWindow : Window
    {
        public AddResourceWindow()
        {
            InitializeComponent();
        }

        private void AddResourceClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as AdminWindow).AddResource(new Resource(textbox1.Text));
            this.Hide();
        }
    }
}
