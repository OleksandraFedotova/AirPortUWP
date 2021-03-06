﻿using App1.Pages.Tickets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TicketsPage : Page
    {
        public IEnumerable<Ticket> Tickets { get; set; }

        public TicketsPage()
        {
            this.InitializeComponent();
            var client = new HttpClient();
            try
            {

                var response = "";

                client.MaxResponseContentBufferSize = 266000;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = client.GetStringAsync(App.BaseUrl + "Tickets").ConfigureAwait(false).GetAwaiter().GetResult();

                var tickets = JsonConvert.DeserializeObject<TicketsList>(response);
                Tickets = tickets.Tickets;
            }
            catch (Exception e)
            {
                MessageDialog showDialog = new MessageDialog("Something wrong with getting data!!! Maybe there is no conection to server");
                showDialog.ShowAsync();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Create_Ticket(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateTicketPage));
        }

        void SelectedTicket(object sender, RoutedEventArgs routedEventArgs)
        {
            var Ticket = ((sender as ListView).SelectedItem);
            Ticket TicketforView = (Ticket)Ticket;

            Frame.Navigate(typeof(TicketPage), TicketforView);
        }
    }
    public class Ticket
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }
    }

    public class TicketsList
    {
        public virtual IEnumerable<Ticket> Tickets { get; set; }
    }
}
