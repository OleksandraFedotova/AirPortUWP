using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App1.Pages;
using App1.Pages.AirCrafts;
using App1.Pages.AirCraftTypes;
using App1.Pages.Pilots;
using App1.Pages.Stewardesses;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PilotsPage));
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StewardessesPage));
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TicketsPage));
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AirCraftTypesPage));
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AirCraftsPage));
        }

        private void Button_Click6(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TicketsPage));
        }

        private void Button_Click7(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PilotsPage));
        }

        private void Button_Click8(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PilotsPage));
        }
    }
}
