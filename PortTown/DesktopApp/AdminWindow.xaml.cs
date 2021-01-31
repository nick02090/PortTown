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
        public DataTable productionBuildingTable { get; set; }
        public DataTable storageBuildingTable { get; set; }
        public DataTable itemTable { get; set; }
        public DataTable resourceTable { get; set; }
        public DataTable marketTable { get; set; }
        public List<User> users = null;
        public List<Building> buildings = null;
        public List<Item> items = null;
        public List<ResourceBatch> marketItems = null;
        private async void AdminWindow_Loaded(object sender, RoutedEventArgs e)
        {

            DataColumn dc1, dc2, dc3, dc4, dc5, dc6, dc7, dc8;
            userTable = new DataTable("users");
            dc1 = new DataColumn("Id", typeof(Guid));
            dc2 = new DataColumn("Username", typeof(string));
            dc3 = new DataColumn("Email", typeof(string));
            dc4 = new DataColumn("Password", typeof(string));
            dc5 = new DataColumn("Town", typeof(string));
            dc6 = new DataColumn("Town Level", typeof(int));
            userTable.Columns.Add(dc1);
            userTable.Columns.Add(dc2);
            userTable.Columns.Add(dc3);
            userTable.Columns.Add(dc4);
            userTable.Columns.Add(dc5);
            userTable.Columns.Add(dc6);
            UserTable.ItemsSource = userTable.DefaultView;

            productionBuildingTable = new DataTable("productionBuildings");
            dc1 = new DataColumn("Id", typeof(Guid));
            dc2 = new DataColumn("Name", typeof(string));
            dc3 = new DataColumn("Capacity", typeof(int));
            dc4 = new DataColumn("Time To Build", typeof(string));
            dc5 = new DataColumn("Resources To Build", typeof(string));
            dc6 = new DataColumn("Resources To Upgrade", typeof(string));
            dc7 = new DataColumn("Production Rate", typeof(float));
            dc8 = new DataColumn("Produces", typeof(string));
            productionBuildingTable.Columns.Add(dc1);
            productionBuildingTable.Columns.Add(dc2);
            productionBuildingTable.Columns.Add(dc3);
            productionBuildingTable.Columns.Add(dc4);
            productionBuildingTable.Columns.Add(dc5);
            productionBuildingTable.Columns.Add(dc6);
            productionBuildingTable.Columns.Add(dc7);
            productionBuildingTable.Columns.Add(dc8);
            ProductionBuildingTable.ItemsSource = productionBuildingTable.DefaultView;

            storageBuildingTable = new DataTable("storageBuildings");
            dc1 = new DataColumn("Id", typeof(Guid));
            dc2 = new DataColumn("Name", typeof(string));
            dc3 = new DataColumn("Capacity", typeof(int));
            dc4 = new DataColumn("Time To Build", typeof(string));
            dc5 = new DataColumn("Resources To Build", typeof(string));
            dc6 = new DataColumn("Resources To Upgrade", typeof(string));
            dc7 = new DataColumn("Stores", typeof(string));
            storageBuildingTable.Columns.Add(dc1);
            storageBuildingTable.Columns.Add(dc2);
            storageBuildingTable.Columns.Add(dc3);
            storageBuildingTable.Columns.Add(dc4);
            storageBuildingTable.Columns.Add(dc5);
            storageBuildingTable.Columns.Add(dc6);
            storageBuildingTable.Columns.Add(dc7);
            StorageBuildingTable.ItemsSource = storageBuildingTable.DefaultView;

            itemTable = new DataTable("items");
            dc1 = new DataColumn("Id", typeof(Guid));
            dc2 = new DataColumn("Name", typeof(string));
            dc3 = new DataColumn("Quality", typeof(string));
            dc4 = new DataColumn("Price", typeof(int));
            dc5 = new DataColumn("Required Resources", typeof(string));
            itemTable.Columns.Add(dc1);
            itemTable.Columns.Add(dc2);
            itemTable.Columns.Add(dc3);
            itemTable.Columns.Add(dc4);
            itemTable.Columns.Add(dc5);
            ItemTable.ItemsSource = itemTable.DefaultView;

            marketTable = new DataTable("marketItems");
            dc1 = new DataColumn("Id", typeof(Guid));
            dc2 = new DataColumn("Resource Type", typeof(string));
            dc3 = new DataColumn("Quantity", typeof(int));
            dc4 = new DataColumn("Price", typeof(int));
            marketTable.Columns.Add(dc1);
            marketTable.Columns.Add(dc2);
            marketTable.Columns.Add(dc3);
            marketTable.Columns.Add(dc4);
            MarketTable.ItemsSource = marketTable.DefaultView;

            removeUserButton.IsEnabled = false;
            editUserButton.IsEnabled = false;
            removeProductionBuildingButton.IsEnabled = false;
            editProductionBuildingButton.IsEnabled = false;
            removeStorageBuildingButton.IsEnabled = false;
            editStorageBuildingButton.IsEnabled = false;
            removeItemButton.IsEnabled = false;
            editItemButton.IsEnabled = false;
            editMarketButton.IsEnabled = false;

            users = await GetUsers();
            //buildings = await GetBuildingTemplates();
            //items = await GetItemTemplates();
        }

        private async void UserTabItemClick(object sender, RoutedEventArgs e)
        {
            if (users is null)
                users = await GetUsers();
        }

        private async void BuildingTabItemClick(object sender, RoutedEventArgs e)
        {
            if (buildings is null)
                buildings = await GetBuildingTemplates();
        }

        private async void ItemTabItemClick(object sender, RoutedEventArgs e)
        {
            if (items is null)
                items = await GetItemTemplates();
        }

        private async void MarketItemTabItemClick(object sender, RoutedEventArgs e)
        {
            if (marketItems is null)
                marketItems = await GetMarketItemTemplates();
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
                case "ProductionBuildingTable":
                    //Console.WriteLine("addBuildingButton");
                    activeGrid = ProductionBuildingTable;
                    activeEditButton = editProductionBuildingButton;
                    activeRemoveButton = removeProductionBuildingButton;
                    break;
                case "StorageBuildingTable":
                    //Console.WriteLine("addBuildingButton");
                    activeGrid = StorageBuildingTable;
                    activeEditButton = editStorageBuildingButton;
                    activeRemoveButton = removeStorageBuildingButton;
                    break;
                case "ItemTable":
                    //Console.WriteLine("addItemButton");
                    activeGrid = ItemTable;
                    activeEditButton = editItemButton;
                    activeRemoveButton = removeItemButton;
                    break;
                case "MarketTable":
                    //Console.WriteLine("addResourceButton");
                    activeGrid = MarketTable;
                    activeEditButton = editMarketButton;
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
                    AddUserWindow addUserWindow = new AddUserWindow();
                    addUserWindow.Show();
                    break;
                case "addBuildingButton":
                    AddBuildingWindow addBuildingWindow = new AddBuildingWindow();
                    addBuildingWindow.Show();
                    break;
                case "addProductionBuildingButton":
                    AddProductionBuildingWindow addProductionBuildingWindow = new AddProductionBuildingWindow();
                    addProductionBuildingWindow.Show();
                    break;
                case "addStorageBuildingButton":
                    AddStorageBuildingWindow addStorageBuildingWindow = new AddStorageBuildingWindow();
                    addStorageBuildingWindow.Show();
                    break;
                case "addItemButton":
                    AddItemWindow addItemWindow = new AddItemWindow();
                    addItemWindow.Show();
                    break;
                case "addMarketButton":
                    AddMarketItemWindow addMarketItemWindow = new AddMarketItemWindow();
                    addMarketItemWindow.Show();
                    break;
                case "editUserButton":
                    var selectedRow = GetSelectedRows(UserTable)[0].Row;
                    EditUserWindow editUserWindow = new EditUserWindow(FindUserById((Guid)selectedRow[0]));
                    editUserWindow.Show();
                    break;
                case "editBuildingButton":
                    EditBuildingWindow editBuildingWindow = new EditBuildingWindow();
                    editBuildingWindow.Show();
                    break;
                case "editItemButton":
                    EditItemWindow editItemWindow = new EditItemWindow();
                    editItemWindow.Show();
                    break;
                case "editMarketButton":
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
                    activeGrid = UserTable;
                    activeTable = userTable;
                    activeRemoveButton = removeUserButton;
                    activeEditButton = editUserButton;
                    break;
                case "removeProductionBuildingButton":
                    activeGrid = ProductionBuildingTable;
                    activeTable = productionBuildingTable;
                    activeRemoveButton = removeProductionBuildingButton;
                    activeEditButton = editProductionBuildingButton;
                    break;
                case "removeStorageBuildingButton":
                    activeGrid = StorageBuildingTable;
                    activeTable = storageBuildingTable;
                    activeRemoveButton = removeStorageBuildingButton;
                    activeEditButton = editStorageBuildingButton;
                    break;
                case "removeItemButton":
                    activeGrid = ItemTable;
                    activeTable = itemTable;
                    activeRemoveButton = removeItemButton;
                    activeEditButton = editItemButton;
                    break;
                case "removeMarketButton":
                    activeGrid = MarketTable;
                    activeTable = marketTable;
                    activeEditButton = editMarketButton;
                    break;
            }
            var rows = activeGrid.SelectedItems.Cast<DataRowView>().ToList();
            foreach (var dr in rows)
            {
                DeleteUser((Guid)dr.Row[0]);
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

        public void AddUserToTable(User user)
        {
            DataRow dr = userTable.NewRow();
            dr[0] = user.Id;
            dr[1] = user.Username;
            dr[2] = user.Email;
            dr[3] = "*********";
            dr[4] = user.Town.Name;
            dr[5] = user.Town.Level;
            userTable.Rows.Add(dr);
            UserTable.ItemsSource = userTable.DefaultView;
        }

        public void AddBuildingToTable(Building building)
        {
            DataRow dr;
            DataTable activeTable;
            System.Windows.Controls.DataGrid activeGrid;
            if (building.BuildingType == 0)
            {
                activeTable = productionBuildingTable;
                activeGrid = ProductionBuildingTable;
            }
            else
            {
                activeTable = storageBuildingTable;
                activeGrid = StorageBuildingTable;
            }
            dr = activeTable.NewRow();
            dr[0] = building.Id;
            dr[1] = building.Name;
            dr[2] = building.Capacity;
            dr[3] = building.ParentCraftable.TimeToBuild.ToString("HH:mm:ss");
            string resources = "";
            var resourceList = building.ParentCraftable.RequiredResources.ToList<ResourceBatch>();
            for (int i = 0; i < resourceList.Count; i++)
            {
                resources += resourceList[i].ResourceType + " (" + resourceList[i].Quantity + ")" + ", ";
            }
            if(resources.Length > 2)
                resources = resources.Remove(resources.Length - 2, 2);
            dr[4] = resources;

            resources = "";
            resourceList = building.Upgradeable.RequiredResources.ToList<ResourceBatch>();
            for (int i = 0; i < resourceList.Count; i++)
            {
                resources += resourceList[i].ResourceType + " (" + resourceList[i].Quantity + ")" + ", ";
            }
            if (resources.Length > 2)
                resources = resources.Remove(resources.Length - 2, 2);
            dr[5] = resources;

            if (building.BuildingType == 0)
            {
                dr[6] = building.ChildProductionBuilding.ProductionRate;
                dr[7] = building.ChildProductionBuilding.ResourceProduced;
            }
            else
            {
                resources = "";
                resourceList = building.ChildStorage.StoredResources.ToList<ResourceBatch>();
                for (int i = 0; i < resourceList.Count; i++)
                {
                    resources += resourceList[i].ResourceType + ", ";
                }
                if (resources.Length > 2)
                    resources = resources.Remove(resources.Length - 2, 2);
                dr[6] = resources;
            }

            activeTable.Rows.Add(dr);
            activeGrid.ItemsSource = activeTable.DefaultView;
        }

        public void AddItemToTable(Item item)
        {
            
            DataRow dr = itemTable.NewRow();
            dr[0] = item.Id;
            dr[1] = item.Name;
            dr[2] = item.Quality;
            string resources = "";
            dr[3] = item.Sellable.Price;
            var resourceList = item.ParentCraftable.RequiredResources.ToList<ResourceBatch>();
            for (int i = 0; i < item.ParentCraftable.RequiredResources.Count; i++)
            {
                resources += resourceList[i].ResourceType + " (" + resourceList[i].Quantity + ")" + ", ";
            }
            dr[4] = resources.Remove(resources.Length - 2, 2);
            itemTable.Rows.Add(dr);
            ItemTable.ItemsSource = itemTable.DefaultView;
        }

        public void AddMarketItemToTable(ResourceBatch marketBatch)
        {

            DataRow dr = marketTable.NewRow();
            dr[0] = marketBatch.Id;
            dr[1] = marketBatch.ResourceType;
            dr[2] = marketBatch.Quantity;
            dr[3] = marketBatch.Sellable.Price;
            marketTable.Rows.Add(dr);
            MarketTable.ItemsSource = marketTable.DefaultView;
        }

        public void EditAddable(TableAddable ta)
        {
            return;
        }

        public async Task<User> EditUser(User oldUser, User newUser)
        {

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonConvert.SerializeObject(newUser);
            var userStringContent = new StringContent(userJson, Encoding.UTF8, "application/json");

            var townJson = JsonConvert.SerializeObject(oldUser.Town);
            var townStringContent = new StringContent(townJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response1 = await client.PutAsync($"api/town/{oldUser.Town.Id}", townStringContent);
            HttpResponseMessage response2 = await client.PutAsync($"api/user/{oldUser.Id}", userStringContent);

            if (response2.IsSuccessStatusCode && response1.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var responseResult = response1.Content.ReadAsStringAsync().Result;
                await GetUsers();
            }
            else
            {
                System.Windows.MessageBox.Show("Error Code" + response1.StatusCode + " : Message - " + response1.ReasonPhrase);
            }
            return FindUserById(oldUser.Id);
        }

        public List<DataRowView> GetSelectedRows(System.Windows.Controls.DataGrid grid)
        {
            return grid.SelectedItems.Cast<DataRowView>().ToList();
        }

        public async void AddUser(User user, string townName)
        {

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userJson = JsonConvert.SerializeObject(user);
            var userStringContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            Console.WriteLine(userJson);

            HttpResponseMessage response = await client.PostAsync("api/user/register/" + townName, userStringContent);

            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var responseResult = response.Content.ReadAsStringAsync().Result;
                await GetUsers();
            }
            else
            {
                System.Windows.MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        public async void DeleteUser(Guid id)
        {

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.DeleteAsync("api/user/" + id);

            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var responseResult = response.Content.ReadAsStringAsync().Result;
                await GetUsers();
            }
            else
            {
                System.Windows.MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        public async void AddBuilding(Building building)
        {

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var buildingJson = JsonConvert.SerializeObject(building);
            var buildingStringContent = new StringContent(buildingJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/building/template", buildingStringContent);

            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var responseResult = response.Content.ReadAsStringAsync().Result;
                await GetBuildingTemplates();
            }
            else
            {
                System.Windows.MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        public async void AddMarketItem(MarketItem marketItem)
        {

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var marketItemJson = JsonConvert.SerializeObject(marketItem);
            var marketItemStringContent = new StringContent(marketItemJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/marketplace/template", marketItemStringContent);

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

        public async void AddItem(Item item)
        {

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var itemJson = JsonConvert.SerializeObject(item);
            var itemStringContent = new StringContent(itemJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/item/template", itemStringContent);

            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var responseResult = response.Content.ReadAsStringAsync().Result;
                //GetItems();
            }
            else
            {
                System.Windows.MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private async Task<List<User>> GetUsers()
        {
            List<User> users = null;

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/user");

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
            userTable.Clear();
            UserTable.ItemsSource = userTable.DefaultView;
            foreach (var user in users)
            {
                user.Town = await GetTownAsync(user.Town.Id);
                AddUserToTable(user);
            }
            return users;
        }

        private async Task<Town> GetTownAsync(Guid id)
        {
            Town town = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                // Add an Accept header for JSON format.

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/town/{id}");

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var responseResult = response.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Town  
                    town = JsonConvert.DeserializeObject<Town>(responseResult);
                }
                else
                {
                    System.Windows.MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
                return town;
            }

            
        }

        private async Task<List<Building>> GetBuildingTemplates()
        {
            List<Building> buildings = null;

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/building/template");

            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var responseResult = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Town  
                buildings = JsonConvert.DeserializeObject<List<Building>>(responseResult);
                
            }
            else
            {
                System.Windows.MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
            productionBuildingTable.Clear();
            ProductionBuildingTable.ItemsSource = productionBuildingTable.DefaultView;
            storageBuildingTable.Clear();
            StorageBuildingTable.ItemsSource = storageBuildingTable.DefaultView;
            foreach (var building in buildings)
            {
                AddBuildingToTable(building);
            }
            return buildings;
        }

        private async Task<List<Item>> GetItemTemplates()
        {
            List<Item> items = null;

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/item/template");

            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var responseResult = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Town  
                items = JsonConvert.DeserializeObject<List<Item>>(responseResult);

            }
            else
            {
                System.Windows.MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
            itemTable.Clear();
            ItemTable.ItemsSource = itemTable.DefaultView;
            foreach (var item in items)
            {
                AddItemToTable(item);
            }
            return items;
        }

        private async Task<List<ResourceBatch>> GetMarketItemTemplates()
        {
            List<ResourceBatch> marketBatches = null;

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Baseurl)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/marketplace/template");

            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var responseResult = response.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Town  
                marketBatches = JsonConvert.DeserializeObject<List<ResourceBatch>>(responseResult);

            }
            else
            {
                System.Windows.MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
            marketTable.Clear();
            MarketTable.ItemsSource = marketTable.DefaultView;
            foreach (var marketBatch in marketBatches)
            {
                AddMarketItemToTable(marketBatch);
            }
            return marketBatches;
        }

        private User FindUserById(Guid id)
        {
            foreach (var user in users)
            {
                if (user.Id.Equals(id))
                    return user;
            }
            return null;
        }
    }
}
