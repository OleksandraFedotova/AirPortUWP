using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages.Pilots
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PilotsPage : Page
    {

        public IEnumerable<Pilot> Pilots { get; set; }

        public PilotsPage()
        {
            this.InitializeComponent();
            var client = new HttpClient();
            try
            {

                var response = "";

                client.MaxResponseContentBufferSize = 266000;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = client.GetStringAsync(App.BaseUrl + "pilots").ConfigureAwait(false).GetAwaiter().GetResult();

                var pilots = JsonConvert.DeserializeObject<PilotsList>(response);
                Pilots = pilots.Pilots;


            }
            catch (Exception e)
            {

                MessageDialog showDialog = new MessageDialog("Something wrong with getting data!!! Maybe there is no conection to server");
                showDialog.ShowAsync();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Create_Pilot(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreatePilotPage));
        }

        void SelectedPilot(object sender, RoutedEventArgs routedEventArgs)
        {
            var pilot = ((sender as ListView).SelectedItem);
            Pilot pilotforView = (Pilot)pilot;

            Frame.Navigate(typeof(PilotPage), pilotforView);
        }
    }
    public class Pilot
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("experience")]
        public int Experience { get; set; }
    }

    public class PilotsList
    {
        public virtual IEnumerable<Pilot> Pilots { get; set; }
    }
}
