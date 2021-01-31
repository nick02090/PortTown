using DesktopApp.Models;
using DesktopApp.Enums;
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
using System.Data;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddResourceWindow : Window
    {
        public DataTable DataTable;
        public DataGrid DataGrid;
        public AddResourceWindow(DataGrid dataGrid, DataTable dataTable)
        {
            InitializeComponent();
            DataTable = dataTable;
            DataGrid = dataGrid;
        }

        private void AddResourceClick(object sender, RoutedEventArgs e)
        {
            DataRow dr = DataTable.NewRow();
            dr[0] = (ResourceType)Enum.ToObject(typeof(ResourceType), resourceTypeDropdown.SelectedIndex); ;
            dr[1] = Int32.Parse(textbox1.Text);
            DataTable.Rows.Add(dr);
            DataGrid.ItemsSource = DataTable.DefaultView;
            this.Hide();
        }
    }
}
