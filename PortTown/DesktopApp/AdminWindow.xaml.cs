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
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using DesktopApp.Models;
using System.Reflection;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            
        }

        const string Baseurl = "https://localhost:44366/";

        public DataTable userTable { get; set; }
        public DataTable buildingTable { get; set; }
        public DataTable itemTable { get; set; }
        public DataTable resourceTable { get; set; }
        public DataTable marketTable { get; set; }

        private void AdminWindow_Loaded(object sender, RoutedEventArgs e)
        {
            userTable = new DataTable("users");
            DataColumn dc1 = new DataColumn("Id", typeof(Guid));
            DataColumn dc2 = new DataColumn("Username", typeof(string));
            DataColumn dc3 = new DataColumn("Email", typeof(string));
            DataColumn dc4 = new DataColumn("Password", typeof(string));
            DataColumn dc5 = new DataColumn("Town", typeof(string));
            userTable.Columns.Add(dc1);
            userTable.Columns.Add(dc2);
            userTable.Columns.Add(dc3);
            userTable.Columns.Add(dc4);
            userTable.Columns.Add(dc5);
            UserTable.ItemsSource = userTable.DefaultView;

            buildingTable = new DataTable("buildings");
            dc1 = new DataColumn("Id", typeof(Guid));
            dc2 = new DataColumn("Username", typeof(string));
            dc3 = new DataColumn("Email", typeof(string));
            dc4 = new DataColumn("Password", typeof(string));
            buildingTable.Columns.Add(dc1);
            buildingTable.Columns.Add(dc2);
            buildingTable.Columns.Add(dc3);
            buildingTable.Columns.Add(dc4);
            BuildingTable.ItemsSource = userTable.DefaultView;

            itemTable = new DataTable("items");
            dc1 = new DataColumn("Id", typeof(Guid));
            dc2 = new DataColumn("Username", typeof(string));
            dc3 = new DataColumn("Email", typeof(string));
            dc4 = new DataColumn("Password", typeof(string));
            itemTable.Columns.Add(dc1);
            itemTable.Columns.Add(dc2);
            itemTable.Columns.Add(dc3);
            itemTable.Columns.Add(dc4);
            ItemTable.ItemsSource = itemTable.DefaultView;

            resourceTable = new DataTable("resources");
            dc1 = new DataColumn("Id", typeof(Guid));
            dc2 = new DataColumn("Username", typeof(string));
            dc3 = new DataColumn("Email", typeof(string));
            dc4 = new DataColumn("Password", typeof(string));
            resourceTable.Columns.Add(dc1);
            resourceTable.Columns.Add(dc2);
            resourceTable.Columns.Add(dc3);
            resourceTable.Columns.Add(dc4);
            ResourceTable.ItemsSource = resourceTable.DefaultView;

            marketTable = new DataTable("marketItems");
            dc1 = new DataColumn("Id", typeof(Guid));
            dc2 = new DataColumn("Username", typeof(string));
            dc3 = new DataColumn("Email", typeof(string));
            dc4 = new DataColumn("Password", typeof(string));
            marketTable.Columns.Add(dc1);
            marketTable.Columns.Add(dc2);
            marketTable.Columns.Add(dc3);
            marketTable.Columns.Add(dc4);
            MarketTable.ItemsSource = marketTable.DefaultView;

            removeUserButton.IsEnabled = false;
            editUserButton.IsEnabled = false;
            removeBuildingButton.IsEnabled = false;
            editBuildingButton.IsEnabled = false;
            removeItemButton.IsEnabled = false;
            editItemButton.IsEnabled = false;
            removeResourceButton.IsEnabled = false;
            editResourceButton.IsEnabled = false;
            removeMarketButton.IsEnabled = false;
            editMarketButton.IsEnabled = false;

            var users = GetUsers();
            if (users != null)
            {
            AddListToTable(users);
            }

        }

        private void DataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.DataGrid activeGrid = new System.Windows.Controls.DataGrid();
            System.Windows.Controls.Button activeRemoveButton = new System.Windows.Controls.Button();
            System.Windows.Controls.Button activeEditButton = new System.Windows.Controls.Button();
            switch (((System.Windows.Controls.DataGrid)sender).Name)
            {
                case "UserTable":
                    //Console.WriteLine("addUserButton");
                    activeGrid = UserTable;
                    activeEditButton = editUserButton;
                    activeRemoveButton = removeUserButton;
                    break;
                case "BuildingTable":
                    //Console.WriteLine("addBuildingButton");
                    activeGrid = BuildingTable;
                    activeEditButton = editBuildingButton;
                    activeRemoveButton = removeBuildingButton;
                    break;
                case "ItemTable":
                    //Console.WriteLine("addItemButton");
                    activeGrid = ItemTable;
                    activeEditButton = editItemButton;
                    activeRemoveButton = removeItemButton;
                    break;
                case "ResourceTable":
                    //Console.WriteLine("addResourceButton");
                    activeGrid = ResourceTable;
                    activeEditButton = editResourceButton;
                    activeRemoveButton = removeResourceButton;
                    break;
                case "MarketTable":
                    //Console.WriteLine("addResourceButton");
                    activeGrid = MarketTable;
                    activeEditButton = editMarketButton;
                    activeRemoveButton = removeMarketButton;
                    break;
            }

            if (GetSelectedRows(activeGrid).Count != 0)
            {
                activeEditButton.IsEnabled = true;
                activeRemoveButton.IsEnabled = true;
            }
        }

        private void OpenWindowClick(object sender, RoutedEventArgs e)
        {

            switch (((System.Windows.Controls.Button)sender).Name)
            {
                case "addUserButton":
                    Console.WriteLine("addUserButton");
                    AddUserWindow addUserWindow = new AddUserWindow();
                    addUserWindow.Show();
                    break;
                case "addBuildingButton":
                    Console.WriteLine("addBuildingButton");
                    AddBuildingWindow addBuildingWindow = new AddBuildingWindow();
                    addBuildingWindow.Show();
                    break;
                case "addItemButton":
                    Console.WriteLine("addItemButton");
                    AddItemWindow addItemWindow = new AddItemWindow();
                    addItemWindow.Show();
                    break;
                case "addResourceButton":
                    Console.WriteLine("editResourceButton");
                    AddResourceWindow addResourceWindow = new AddResourceWindow();
                    addResourceWindow.Show();
                    break;
                case "addMarketButton":
                    Console.WriteLine("editResourceButton");
                    AddMarketItemWindow addMarketItemWindow = new AddMarketItemWindow();
                    addMarketItemWindow.Show();
                    break;
                case "editUserButton":
                    Console.WriteLine("editUserButton");
                    EditUserWindow editUserWindow = new EditUserWindow();
                    editUserWindow.Show();
                    break;
                case "editBuildingButton":
                    Console.WriteLine("editBuildingButton");
                    EditBuildingWindow editBuildingWindow = new EditBuildingWindow();
                    editBuildingWindow.Show();
                    break;
                case "editItemButton":
                    Console.WriteLine("editItemButton");
                    EditItemWindow editItemWindow = new EditItemWindow();
                    editItemWindow.Show();
                    break;
                case "editResourceButton":
                    Console.WriteLine("editResourceButton");
                    EditResourceWindow editResourceWindow = new EditResourceWindow();
                    editResourceWindow.Show();
                    break;
                case "editMarketButton":
                    Console.WriteLine("editMarketButton");
                    EditMarketItemWindow editMarketItemWindow = new EditMarketItemWindow();
                    editMarketItemWindow.Show();
                    break;
            }
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.DataGrid activeGrid = new System.Windows.Controls.DataGrid();
            DataTable activeTable = new DataTable();
            System.Windows.Controls.Button activeRemoveButton = new System.Windows.Controls.Button();
            System.Windows.Controls.Button activeEditButton = new System.Windows.Controls.Button();
            switch (((System.Windows.Controls.Button)sender).Name)
            {
                case "removeUserButton":
                    Console.WriteLine("removeUserButton");
                    activeGrid = UserTable;
                    activeTable = userTable;
                    activeRemoveButton = removeUserButton;
                    activeEditButton = editUserButton;
                    break;
                case "removeBuildingButton":
                    Console.WriteLine("removeBuildingButton");
                    activeGrid = BuildingTable;
                    activeTable = buildingTable;
                    activeRemoveButton = removeBuildingButton;
                    activeEditButton = editBuildingButton;
                    break;
                case "removeItemButton":
                    activeGrid = ItemTable;
                    activeTable = itemTable;
                    activeRemoveButton = removeItemButton;
                    activeEditButton = editItemButton;
                    Console.WriteLine("removeItemButton");
                    break;
                case "removeResourceButton":
                    activeGrid = ResourceTable;
                    activeTable = resourceTable;
                    activeRemoveButton = removeResourceButton;
                    activeEditButton = editResourceButton;
                    Console.WriteLine("removeResourceButton");
                    break;
                case "removeMarketButton":
                    activeGrid = MarketTable;
                    activeTable = marketTable;
                    activeRemoveButton = removeMarketButton;
                    activeEditButton = editMarketButton;
                    Console.WriteLine("removeMarketButton");
                    break;
            }
            var rows = activeGrid.SelectedItems.Cast<DataRowView>().ToList();
            foreach (var dr in rows)
            {
                activeTable.Rows.Remove(dr.Row);
            }
            if (activeGrid.SelectedItems.Count > 0)
            {
                activeGrid.SelectedItems.Remove(activeGrid.SelectedItems[0]);
            }
            if (activeGrid.SelectedItems.Count == 0)
            {
                activeRemoveButton.IsEnabled = false;
                activeEditButton.IsEnabled = false;
            }
            activeGrid.ItemsSource = activeTable.DefaultView;
        }

        public void AddAddable(TableAddable addableObject, DataTable dataTable, System.Windows.Controls.DataGrid dataGrid)
        {
            AddUserToDB((User)addableObject);
            DataRow dr = dataTable.NewRow();
            PropertyInfo[] properties = addableObject.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                dr[i] = properties[i].GetValue(addableObject);
            }
            dataTable.Rows.Add(dr);
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        public void EditAddable(TableAddable addableObject, DataTable dataTable, System.Windows.Controls.DataGrid dataGrid)
        {
            DataRow row = GetSelectedRows(dataGrid)[0].Row;
            PropertyInfo[] properties = addableObject.GetType().GetProperties();
            for(int i = 0; i < properties.Length; i++)
            {
                row[i] = properties[i].GetValue(addableObject);
            }
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        public List<DataRowView> GetSelectedRows(System.Windows.Controls.DataGrid grid)
        {
            return grid.SelectedItems.Cast<DataRowView>().ToList();
        }

        private void AddUserToDB(User user)
        {

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonConvert.SerializeObject(user);
            var userStringContent = new StringContent(userJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("api/user/register/" + user.TownName, userStringContent).Result;

            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var responseResult = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                System.Windows.MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private List<User> GetUsers()
        {
            List<User> users = null;

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/town").Result;

            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var responseResult = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Town  
                users = JsonConvert.DeserializeObject<List<User>>(responseResult);
            }
            else
            {
                System.Windows.MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

            return users;
        }

        private void AddListToTable(List<User> itemList)
        {
            foreach (var item in itemList)
            {
                AddAddable(item, userTable, UserTable);
            }
        }
    }
}
