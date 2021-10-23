using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NetworkSpeed
{
    public partial class MainPage : ContentPage
    { 

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            SpeedResultLabel.Text = "Not run yet ...";
        }

      

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    SpeedResultLabel.Text = "Not connected..";
                    return;
                }
                DateTime dateTimeStartRequest = DateTime.Now;
                var tmpClient = new HttpClient();
                byte[] data = await tmpClient.GetByteArrayAsync("https://www.google.com");
                DateTime dateTimeEndRequest = DateTime.Now;
                SpeedResultLabel.Text = $"Connection speed is {Math.Round((data.Length / 1024) / (dateTimeEndRequest - dateTimeStartRequest).TotalSeconds, 2)} (kb/s) ";
            }
            catch (Exception exception)
            {
                SpeedResultLabel.Text = "failed ...";
                Debug.WriteLine($"Error: {exception.Message}");
            }
        }
    }
}
