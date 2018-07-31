using App1.Pages.Pilots;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
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

            if (ValidatePilot() == false)
            {
                MessageDialog showDialog = new MessageDialog("Please check your inputs");
                await showDialog.ShowAsync();

            }
            else
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

                    var result = client.PutAsync(new Uri(url), byteContent).ConfigureAwait(false).GetAwaiter()
                        .GetResult();

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

        public bool ValidatePilot()
        {
            var DateRegex = new Regex(@"(0{0,1}[1-9])|(1/d)|(2/d)|(3[0-1])/(0{0,1}[1-9])|(1[0-2])/([1-9]/d):(0{0,1}/d)|(1/d)|(2[0-4]):(0{0,1}/d)|([1-5]/d)");


            /*@"[({]?[a-zA-Z0-9]{8}[-]?([a-zA-Z0-9]{4}[-]?){3}[a-zA-Z0-9]{12}[})]?"*/

            if (string.IsNullOrWhiteSpace(FirstName.Text) || string.IsNullOrWhiteSpace(LastName.Text) ||
                string.IsNullOrWhiteSpace(DateOfBirth.Text) || string.IsNullOrWhiteSpace(Experience.Text))
            {
                return false;
            }

            if (!DateRegex.IsMatch(DateOfBirth.Text))
            {
                return false;
            }

            return true;
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
