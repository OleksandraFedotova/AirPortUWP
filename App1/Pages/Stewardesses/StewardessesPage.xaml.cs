﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages.Stewardesses
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StewardessesPage : Page
    {
        public IEnumerable<Stewardess> Stewardesses { get; set; }

        public StewardessesPage()
        {
            this.InitializeComponent();
            using (var client = new HttpClient())

            {

                var response = "";

                client.MaxResponseContentBufferSize = 266000;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Task task = Task.Run(async () =>

                {

                    response = await client.GetStringAsync(App.BaseUrl+"stewardesses");

                });

                task.Wait(); // Wait  

                var stewardesses = JsonConvert.DeserializeObject<StewardessList>(response);
                Stewardesses = stewardesses.Stewardesses;


            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Create_Stewardess(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateStewardessPage));
        }

        void SelectedStewardess(object sender, RoutedEventArgs routedEventArgs)
        {
            var st = ((sender as ListView).SelectedItem);
            Frame.Navigate(typeof(StewardessPage), st);
        }
    }
    public class Stewardess 
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class StewardessList
    {
        public IEnumerable<Stewardess> Stewardesses { get; set; }
    }
}
