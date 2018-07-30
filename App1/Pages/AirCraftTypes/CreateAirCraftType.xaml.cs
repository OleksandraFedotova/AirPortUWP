using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages.AirCraftTypes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateAirCraftType : Page
    {
        public CreateAirCraftType()
        {
            this.InitializeComponent();
        }

        private void BackToList(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AirCraftTypesPage));
        }

        private async void CreateAirCraftTypeItem(object sender, RoutedEventArgs e)
        {
            var p = new AirCraftTypeModel
            {
                 Model= Model.Text,
                 LoadCapacity = Convert.ToInt32(LoadCapacity.Text),
                 Places = Convert.ToInt32(Places.Text)
            };

            using (var client = new HttpClient())

            {
                var myContent = JsonConvert.SerializeObject(p);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                string url = App.BaseUrl + "AirCraftTypes/";

                var result = client.PostAsync(new Uri(url), byteContent).ConfigureAwait(false).GetAwaiter().GetResult();

                if (!result.IsSuccessStatusCode)
                {
                    MessageDialog showDialog = new MessageDialog("Something wrong with posting data!!!");
                    await showDialog.ShowAsync();

                }
                else
                {
                    this.Frame.Navigate(typeof(AirCraftTypesPage));
                }


            }



        }
    }

    public class AirCraftTypeModel
    {
        [JsonProperty("model")] public string Model { get; set; }

        [JsonProperty("Seats")] public int Places { get; set; }

        [JsonProperty("loadcAPACITY")] public int LoadCapacity { get; set; }
    }
}
