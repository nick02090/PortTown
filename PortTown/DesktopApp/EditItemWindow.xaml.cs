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
    
    public partial class EditItemWindow : Window
    {
        public EditItemWindow()
        {
            InitializeComponent();
        }

        public DataTable buildTable { get; set; }
        public DataTable upgradeTable { get; set; }
        public DataTable activeTable = null;
        public DataGrid activeGrid = null;

        private void EditItemWindowWindow_Loaded(object sender, RoutedEventArgs e)
        {

            DataColumn dc1, dc2;
            buildTable = new DataTable("buildResources");
            //dc1 = new DataColumn("Id", typeof(Guid));
            dc1 = new DataColumn("Type", typeof(string));
            dc2 = new DataColumn("Quantity", typeof(int));
            buildTable.Columns.Add(dc1);
            buildTable.Columns.Add(dc2);
            BuildResourcesTable.ItemsSource = buildTable.DefaultView;

            removeBuildResourceButton.IsEnabled = false;
            editBuildResourceButton.IsEnabled = false;
        }

        private void ResourceTableSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if ((Application.Current.MainWindow as AdminWindow).GetSelectedRows(BuildResourcesTable).Count != 0)
            {
                editBuildResourceButton.IsEnabled = true;
                removeBuildResourceButton.IsEnabled = true;
            }
        }

        public void AddResourceToTable(ResourceType resourceType, int quantity)
        {
            DataRow dr = activeTable.NewRow();
            dr[0] = resourceType;
            dr[1] = quantity;
            activeTable.Rows.Add(dr);
            activeGrid.ItemsSource = activeTable.DefaultView;
        }

        private void OpenResourceWindowClick(object sender, RoutedEventArgs e)
        {

            AddResourceWindow addResourceWindow = new AddResourceWindow(BuildResourcesTable, buildTable);
            addResourceWindow.Show();
        }

        private void RemoveResourceClick(object sender, RoutedEventArgs e)
        {
            
            var rows = BuildResourcesTable.SelectedItems.Cast<DataRowView>().ToList();
            foreach (var dr in rows)
            {
                //DeleteUser((Guid)dr.Row[0]);
                buildTable.Rows.Remove(dr.Row);
            }
            if (BuildResourcesTable.SelectedItems.Count > 0)
            {
                BuildResourcesTable.SelectedItems.Remove(BuildResourcesTable.SelectedItems[0]);
            }
            if (BuildResourcesTable.SelectedItems.Count == 0)
            {
                removeBuildResourceButton.IsEnabled = false;
                editBuildResourceButton.IsEnabled = false;
            }
            BuildResourcesTable.ItemsSource = buildTable.DefaultView;
        }


        private void EditItemClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as AdminWindow).EditAddable(new Item());
            this.Hide();
        }
    }
}
