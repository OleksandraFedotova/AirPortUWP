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
using App1.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PilotPage : Page
    {
        public PilotService _pilotService;

        private Pilot PilotData { get; set; }

        public PilotPage()
        {
            this.InitializeComponent();
            _pilotService=new PilotService();
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

               await _pilotService.Update(p);


                this.Frame.Navigate(typeof(PilotsPage));
            }
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
           await _pilotService.Delete(PilotData);
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
