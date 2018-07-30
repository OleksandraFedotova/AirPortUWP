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
    public sealed partial class AirCraftTypePage : Page
    {
        private AirCraftType AirCraftTypeData { get; set; }

        public AirCraftTypePage()
        {
            this.InitializeComponent();
        }

        private void BackToList(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AirCraftTypesPage));
        }

        private async void Update(object sender, RoutedEventArgs e)
        {
            var p = new AirCraftType
            {
                Id = AirCraftTypeData.Id,
                Model = Model.Text,
                Places = Convert.ToInt32(Seats.Text),
                LoadCapacity = Convert.ToInt32(LoadCapacity.Text),
            };

            using (var client = new HttpClient())
            {
                var myContent = JsonConvert.SerializeObject(p);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                string url = App.BaseUrl + "AirCraftTypes/" + AirCraftTypeData.Id;

                var result = client.PutAsync(new Uri(url), byteContent).ConfigureAwait(false).GetAwaiter().GetResult();

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


        private async void Delete(object sender, RoutedEventArgs e)
        {

            using (var client = new HttpClient())

            {
                string b = App.BaseUrl + "AirCraftTypes/" + AirCraftTypeData.Id;
                var result = client.DeleteAsync(new Uri(b)).ConfigureAwait(false).GetAwaiter().GetResult();
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


            this.Frame.Navigate(typeof(AirCraftTypesPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            AirCraftTypeData = (AirCraftType)e.Parameter;

            // parameters.Name
            // parameters.Text
            // ...
        }
    }
}
