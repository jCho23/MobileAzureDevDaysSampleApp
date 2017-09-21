using Xamarin.Forms;

using MobileAzureDevDaysSampleApp.Pages;

namespace MobileAzureDevDaysSampleApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HomePage();
        }
    }
}
