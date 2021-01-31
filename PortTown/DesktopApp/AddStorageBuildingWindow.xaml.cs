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
    
    public partial class AddStorageBuildingWindow : Window
    {
        public AddStorageBuildingWindow()
        {
            InitializeComponent();
        }

        public DataTable buildTable { get; set; }
        public DataTable upgradeTable { get; set; }
        public DataTable activeTable = null;
        public DataGrid activeGrid = null;

        private void AddStorageBuildingWindowWindow_Loaded(object sender, RoutedEventArgs e)
        {

            DataColumn dc1, dc2;
            buildTable = new DataTable("buildResources");
            //dc1 = new DataColumn("Id", typeof(Guid));
            dc1 = new DataColumn("Type", typeof(string));
            dc2 = new DataColumn("Quantity", typeof(int));
            buildTable.Columns.Add(dc1);
            buildTable.Columns.Add(dc2);
            BuildResourcesTable.ItemsSource = buildTable.DefaultView;

            upgradeTable = new DataTable("upgradeResources");
            //dc1 = new DataColumn("Id", typeof(Guid));
            dc1 = new DataColumn("Type", typeof(string));
            dc2 = new DataColumn("Quantity", typeof(int));
            upgradeTable.Columns.Add(dc1);
            upgradeTable.Columns.Add(dc2);
            UpgradeResourcesTable.ItemsSource = upgradeTable.DefaultView;

            removeBuildResourceButton.IsEnabled = false;
            editBuildResourceButton.IsEnabled = false;
            removeUpgradeResourceButton.IsEnabled = false;
            editUpgradeResourceButton.IsEnabled = false;
        }

        private void ResourceTableSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.DataGrid activeGridToChange= new System.Windows.Controls.DataGrid();
            System.Windows.Controls.Button activeRemoveButton = new System.Windows.Controls.Button();
            System.Windows.Controls.Button activeEditButton = new System.Windows.Controls.Button();
            switch (((System.Windows.Controls.DataGrid)sender).Name)
            {
                case "BuildResourcesTable":
                    //Console.WriteLine("addUserButton");
                    activeGridToChange = BuildResourcesTable;
                    activeEditButton = editBuildResourceButton;
                    activeRemoveButton = removeBuildResourceButton;
                    break;
                case "UpgradeResourcesTable":
                    //Console.WriteLine("addUserButton");
                    activeGridToChange = UpgradeResourcesTable;
                    activeEditButton = editUpgradeResourceButton;
                    activeRemoveButton = removeUpgradeResourceButton;
                    break;
            }
            if ((Application.Current.MainWindow as AdminWindow).GetSelectedRows(activeGridToChange).Count != 0)
            {
                activeEditButton.IsEnabled = true;
                activeRemoveButton.IsEnabled = true;
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
            AddResourceWindow addResourceWindow;
            switch (((System.Windows.Controls.Button)sender).Name)
            {
                case "addBuildResourceButton":
                    activeTable = buildTable;
                    activeGrid = BuildResourcesTable;
                    addResourceWindow = new AddResourceWindow(BuildResourcesTable, buildTable);
                    addResourceWindow.Show();
                    break;
                case "addUpgradeResourceButton":
                    activeTable = upgradeTable;
                    activeGrid = UpgradeResourcesTable;
                    addResourceWindow = new AddResourceWindow(UpgradeResourcesTable, upgradeTable);
                    addResourceWindow.Show();
                    break;
            }
            
        }

        private void RemoveResourceClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.DataGrid activeGridToChange = new System.Windows.Controls.DataGrid();
            DataTable activeTableToChange = new DataTable();
            System.Windows.Controls.Button activeRemoveButton = new System.Windows.Controls.Button();
            System.Windows.Controls.Button activeEditButton = new System.Windows.Controls.Button();
            switch (((System.Windows.Controls.Button)sender).Name)
            {
                case "removeBuildResourceButton":
                    //Console.WriteLine("removeUserButton");
                    activeGridToChange = BuildResourcesTable;
                    activeTableToChange = buildTable;
                    activeRemoveButton = removeBuildResourceButton;
                    activeEditButton = editBuildResourceButton;
                    break;
                case "removeUpgradeResourceButton":
                    //Console.WriteLine("removeUserButton");
                    activeGridToChange = UpgradeResourcesTable;
                    activeTableToChange = upgradeTable;
                    activeRemoveButton = removeUpgradeResourceButton;
                    activeEditButton = editUpgradeResourceButton;
                    break;
            }
            var rows = activeGridToChange.SelectedItems.Cast<DataRowView>().ToList();
            foreach (var dr in rows)
            {
                //DeleteUser((Guid)dr.Row[0]);
                activeTableToChange.Rows.Remove(dr.Row);

            }
            if (activeGridToChange.SelectedItems.Count > 0)
            {
                activeGridToChange.SelectedItems.Remove(activeGridToChange.SelectedItems[0]);
            }
            if (activeGridToChange.SelectedItems.Count == 0)
            {
                activeRemoveButton.IsEnabled = false;
                activeEditButton.IsEnabled = false;
            }
            activeGridToChange.ItemsSource = activeTableToChange.DefaultView;
        }


        private void AddProductionBuildingClick(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as AdminWindow).AddBuilding(new Building());
            this.Hide();
        }
    }
}
