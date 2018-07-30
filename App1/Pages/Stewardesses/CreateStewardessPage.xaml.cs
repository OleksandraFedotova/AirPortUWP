using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using App1.Pages.Pilots;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages.Stewardesses
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateStewardessPage : Page
    {
        public CreateStewardessPage()
        {
            this.InitializeComponent();
        }

        private void BackToList(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StewardessesPage));
        }

        private async void CreateStewardess(object sender, RoutedEventArgs e)
        {
            var p = new PilotModel
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                DateOfBirth = Convert.ToDateTime(DateOfBirth.Text)
            };

            using (var client = new HttpClient())

            {
                var myContent = JsonConvert.SerializeObject(p);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                string url = App.BaseUrl + "stewardesses/";

                var result = client.PostAsync(new Uri(url), byteContent).ConfigureAwait(false).GetAwaiter().GetResult();

                if (!result.IsSuccessStatusCode)
                {
                    MessageDialog showDialog = new MessageDialog("Something wrong with posting data!!!");
                    await showDialog.ShowAsync();

                }
                else
                {
                    this.Frame.Navigate(typeof(StewardessesPage));
                }


            }
        }
    }

    public class StewardessModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
