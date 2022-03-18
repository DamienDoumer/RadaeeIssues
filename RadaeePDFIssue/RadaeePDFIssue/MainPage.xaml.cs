using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RadaeePDFIssue
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Instance.Subscribe<object>(this, "DownloadCompleted", sender =>
            {
                ReadBookButton.IsEnabled = true;
            });
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await DependencyService.Get<IPDFLauncher>().LaunchPDF();
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            await DependencyService.Get<IPDFLauncher>().DownloadPDF();
        }
    }
}
