﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using App1.Pages.Pilots;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages.Stewardesses
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StewardessPage : Page
    {
        private Stewardess StewardessData { get; set; }
        public StewardessPage()
        {
            this.InitializeComponent();
        }
        private void BackToList(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StewardessesPage));
        }

        private async void Update(object sender, RoutedEventArgs e)
        {
            var p = new Stewardess
            {
                Id = StewardessData.Id,
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


                string url = App.BaseUrl + "stewardesses/" + StewardessData.Id;

                var result = client.PutAsync(new Uri(url), byteContent).ConfigureAwait(false).GetAwaiter().GetResult();

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


        private async void Delete(object sender, RoutedEventArgs e)
        {

            using (var client = new HttpClient())

            {
                string b = App.BaseUrl + "stewardesses/" + StewardessData.Id;
                var result = client.DeleteAsync(new Uri(b)).ConfigureAwait(false).GetAwaiter().GetResult();
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            StewardessData = (Stewardess)e.Parameter;

            // parameters.Name
            // parameters.Text
            // ...
        }
    }
}
