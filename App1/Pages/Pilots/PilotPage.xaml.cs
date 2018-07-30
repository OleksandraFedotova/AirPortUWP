using App1.Pages.Pilots;
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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Net.Http;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PilotPage : Page
    {
        private Pilot PilotData { get; set; }

        public PilotPage()
        {
            this.InitializeComponent();
        }

        private void BackToList(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PilotsPage));
        }

        private async void Update(object sender, RoutedEventArgs e)
        {
            var p = new Pilot
            {
                Id = PilotData.Id,
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Experience = Convert.ToInt32(Experience.Text),
                DateOfBirth = Convert.ToDateTime(DateOfBirth.Text)
            };

            using (var client = new HttpClient())
            {
                var myContent = JsonConvert.SerializeObject(p);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                string url = App.BaseUrl + "pilots/" + PilotData.Id;

                var result = client.PutAsync(new Uri(url), byteContent).ConfigureAwait(false).GetAwaiter().GetResult();

                if (!result.IsSuccessStatusCode)
                {
                    MessageDialog showDialog = new MessageDialog("Something wrong with posting data!!!");
                    await showDialog.ShowAsync();

                }
                else
                {
                    this.Frame.Navigate(typeof(PilotsPage));
                }
            }
        }


        private async void Delete(object sender, RoutedEventArgs e)
        {

            using (var client = new HttpClient())

            {
                string b = App.BaseUrl + "pilots/" + PilotData.Id;
                var result = client.DeleteAsync(new Uri(b)).ConfigureAwait(false).GetAwaiter().GetResult();
                if (!result.IsSuccessStatusCode)
                {
                    MessageDialog showDialog = new MessageDialog("Something wrong with posting data!!!");
                    await showDialog.ShowAsync();

                }
                else
                {
                    this.Frame.Navigate(typeof(PilotsPage));
                }

            }


            this.Frame.Navigate(typeof(PilotsPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            PilotData = (Pilot)e.Parameter;

            // parameters.Name
            // parameters.Text
            // ...
        }
    }
}
