using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages.AirCraftTypes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AirCraftTypesPage : Page
    {

        public IEnumerable<AirCraftType> AirCraftTypes { get; set; }

        public AirCraftTypesPage()
        {
            this.InitializeComponent();
            using (var client = new HttpClient())

            {

                var response = "";

                client.MaxResponseContentBufferSize = 266000;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Task task = Task.Run(async () =>

                {

                    response = await client.GetStringAsync(App.BaseUrl + "AirCraftTypes");

                });

                task.Wait(); // Wait  

                var airCraftTypes = JsonConvert.DeserializeObject<AirCraftTypesList>(response);
                AirCraftTypes = airCraftTypes.airCraftTypes;


            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Create_AirCraftType(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateAirCraftType));
        }

        void SelectedAirCraftType(object sender, RoutedEventArgs routedEventArgs)
        {
            var airCraftType = ((sender as ListView).SelectedItem);

            Frame.Navigate(typeof(AirCraftTypePage), airCraftType);
        }
    }
    public class AirCraftType
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("seats")]
        public int Places { get; set; }

        [JsonProperty("loadCapacity")]
        public int LoadCapacity { get; set; }
    }

    public class AirCraftTypesList
    {
        public virtual IEnumerable<AirCraftType> airCraftTypes { get; set; }
    }
}
