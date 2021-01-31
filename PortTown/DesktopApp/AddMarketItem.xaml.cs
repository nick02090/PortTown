using DesktopApp.Enums;
using DesktopApp.Models;
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

namespace DesktopApp
{
    
    public partial class AddMarketItemWindow : Window
    {
        public AddMarketItemWindow()
        {
            InitializeComponent();
        }

        private void AddMarketItemClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as AdminWindow).AddMarketItem(new MarketItem(""));
            this.Hide();
        }
    }
}
