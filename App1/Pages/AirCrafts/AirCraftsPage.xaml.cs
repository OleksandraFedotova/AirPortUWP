using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App1.Pages.AirCrafts;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages.AirCrafts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AirCraftsPage : Page
    {

        public IEnumerable<AirCraft> AirCrafts { get; set; }

        public AirCraftsPage()
        {
            this.InitializeComponent();
            using (var client = new HttpClient())

            {

                var response = "";

                client.MaxResponseContentBufferSize = 266000;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Task task = Task.Run(async () =>

                {

                    response = await client.GetStringAsync(App.BaseUrl + "AirCrafts");

                });

                task.Wait(); // Wait  

                var airCrafts = JsonConvert.DeserializeObject<AirCraftsList>(response);
                AirCrafts = airCrafts.AirCrafts;


            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Create_AirCraft(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateAirCraftPage));
        }

        void SelectedAirCraft(object sender, RoutedEventArgs routedEventArgs)
        {
            var AirCraft = ((sender as ListView).SelectedItem);
            AirCraft AirCraftforView = (AirCraft)AirCraft;

            Frame.Navigate(typeof(AirCraftPage), AirCraftforView);
        }
    }
    public class AirCraft
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("typeId")]
        public string TypeId { get; set; }

        [JsonProperty("timeSpan")]
        public DateTime TimeSpan { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("releaseDate")]
        public DateTime ReleaseDate { get; set; }
    }


    public class AirCraftsList
    {
        public virtual IEnumerable<AirCraft> AirCrafts { get; set; }
    }
}
