﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;



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
            var p = new PilotModel
            {
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


                string url = App.BaseUrl + "pilots/";

                var result = client.PostAsync(new Uri(url), byteContent).ConfigureAwait(false).GetAwaiter().GetResult();

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
    }

    public class PilotModel
    {
        [JsonProperty("firstName")] public string FirstName { get; set; }

        [JsonProperty("lastName")] public string LastName { get; set; }

        [JsonProperty("dateOfBirth")] public DateTime DateOfBirth { get; set; }

        [JsonProperty("experience")] public int Experience { get; set; }
    }
}
