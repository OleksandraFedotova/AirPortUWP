using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
using Windows.Web.Http.Headers;
using Newtonsoft.Json;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages.Pilots
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreatePilotPage : Page
    {
        public CreatePilotPage()
        {
            this.InitializeComponent();
        }

        private void BackToList(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PilotsPage));
        }

        private async void CreatePilot(object sender, RoutedEventArgs e)
        {
            var p = new Pilot
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Experience = Convert.ToInt32(Experience.Text),
                DateOfBirth = Convert.ToDateTime(DateOfBirth.Text)
            };

            using (var client = new HttpClient())

            {
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                var c = JsonConvert.SerializeObject(p);

                // Send a POST  

                    string url = App.BaseUrl + "pilots/";
                    var pairs =new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("firstName", p.FirstName),
                        new KeyValuePair<string, string>("lastName", p.LastName),
                        new KeyValuePair<string, string>("dateOfBirth", p.DateOfBirth.ToString()),
                        new KeyValuePair<string, string>("experience", p.Experience.ToString()),
                    };
                    var content = new FormUrlEncodedContent(pairs);

                    var result =  client.PostAsync(new Uri(url), content).ConfigureAwait(false).GetAwaiter().GetResult();
                    if (result.IsSuccessStatusCode)
                    {
                        // Get the URI of the created resource.
                        Uri gizmoUrl = result.Headers.Location;
                    }

              

            }


            this.Frame.Navigate(typeof(PilotsPage));
        }
    }
}
