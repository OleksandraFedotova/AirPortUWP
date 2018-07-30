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
using App1.Pages.AirCrafts;
using App1.Pages.AirCraftTypes;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages.AirCrafts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AirCraftPage : Page
    {
        private AirCraft AirCraftData { get; set; }
        private IEnumerable<AirCraftType> CraftTypes { get; set; }

        public AirCraftPage()
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
                CraftTypes = airCraftTypes.airCraftTypes;

                foreach (var t in CraftTypes)
                {
                    string text = Types.Text + "\n" + "Id -" + t.Id + "      Model - " + t.Model + "           LoadCapacity -  " +
                                  t.LoadCapacity + "        Places -    " + t.Places;
                    Types.Text = text;
                }
            }
        }

        private void BackToList(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AirCraftsPage));
        }

        private async void Update(object sender, RoutedEventArgs e)
        {
            var p = new AirCraft
            {
                Id = AirCraftData.Id,
                Name = Name.Text,
                TypeId = TypeId.Text,
                TimeSpan = Convert.ToDateTime(TimeSpan.Text),
                ReleaseDate = Convert.ToDateTime(ReleaseDate.Text)
            };

            using (var client = new HttpClient())
            {
                var myContent = JsonConvert.SerializeObject(p);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                string url = App.BaseUrl + "AirCrafts/" + AirCraftData.Id;

                var result = client.PutAsync(new Uri(url), byteContent).ConfigureAwait(false).GetAwaiter().GetResult();

                if (!result.IsSuccessStatusCode)
                {
                    MessageDialog showDialog = new MessageDialog("Something wrong with posting data!!!");
                    await showDialog.ShowAsync();

                }
                else
                {
                    this.Frame.Navigate(typeof(AirCraftsPage));
                }
            }
        }


        private async void Delete(object sender, RoutedEventArgs e)
        {

            using (var client = new HttpClient())

            {
                string b = App.BaseUrl + "AirCrafts/" + AirCraftData.Id;
                var result = client.DeleteAsync(new Uri(b)).ConfigureAwait(false).GetAwaiter().GetResult();
                if (!result.IsSuccessStatusCode)
                {
                    MessageDialog showDialog = new MessageDialog("Something wrong with posting data!!!");
                    await showDialog.ShowAsync();

                }
                else
                {
                    this.Frame.Navigate(typeof(AirCraftsPage));
                }

            }


            this.Frame.Navigate(typeof(AirCraftsPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            AirCraftData = (AirCraft)e.Parameter;

            // parameters.Name
            // parameters.Text
            // ...
        }
    }
}
