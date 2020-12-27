using Domain;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Hosted web API REST Service base url
        const string Baseurl = "https://localhost:44366/";

        public MainWindow()
        {
            InitializeComponent();

            var town = GetTown();
            if (town != null)
            {
                TownName.Content = town.Name;
                TownLevel.Content = town.Level;
            }
        }

        private Town GetTown()
        {
            Town town = null;

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
                town = JsonConvert.DeserializeObject<Town>(responseResult);
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

            return town;
        }
    }
}
