using AppSalesLogic.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSalesLogic
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();

            this.MainPage = new NavigationPage(new SplashPage());
        }
        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
