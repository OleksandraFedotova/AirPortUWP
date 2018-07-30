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
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AirportUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class P : Page
    {
        public P()
        {
            this.InitializeComponent();
            using (var client = new HttpClient())

            {

                var response = "";

                client.MaxResponseContentBufferSize = 266000;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Task task = Task.Run(async () =>

                {

                    response = await client.GetStringAsync(App.BaseUri + "pilots/");

                });

                task.Wait(); // Wait  

                var pilots = JsonConvert.DeserializeObject<PilotsList>(response);

                var a = 6;
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
            public virtual IEnumerable<Pilots> Pilots { get; set; }
        }
    }

}
