using AppSalesLogic.Helpers;
using AppSalesLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSalesLogic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        Image SplashImage;
        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var Sub = new AbsoluteLayout();
            SplashImage = new Image
            {
                //Source = "wallpers_red_cerebral_1.png"
                Source = "icon_ia_8",
                //,
                WidthRequest = 150,
                HeightRequest = 150
            };

            AbsoluteLayout.SetLayoutFlags(SplashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(SplashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            Sub.Children.Add(SplashImage);
            this.BackgroundColor = Color.FromHex("#000051");
            this.Content = Sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await SplashImage.ScaleTo(1, 2000);
            await SplashImage.ScaleTo(0.9, 1500, Easing.Linear);
            // SplashImage.ScaleTo(150, 1200, Easing.Linear);

            try
            {
                if (Settings.IsRemember)
                {
                    //var config = JsonConvert.DeserializeObject<CompaniaRolUsuario>(Settings.Config);
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.User = Settings.User;
                    mainViewModel.Password = Settings.Password;
                    mainViewModel.IdUsuario = Settings.IdUsuario;
                    //mainViewModel.Config = config;
                    mainViewModel.Menu = new MenuViewModel();
                    mainViewModel.Home = new HomeViewModel();
                    Application.Current.MainPage = new MasterPage();
                    return;
                }
                else
                {
                    var KeyApp = Services.DataService.Instance.GetKeyApp();
                    if (KeyApp != null)
                    {
                        // if (KeyApp.IsFreeAccount)
                        MainViewModel.GetInstance().Menu = new MenuViewModel();
                        MainViewModel.GetInstance().Home = new HomeViewModel();
                        Application.Current.MainPage = new MasterPage();
                        return;
                    }
                }

                MainViewModel.GetInstance().Login = new LoginViewModel();
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            catch
            {
                MainViewModel.GetInstance().Login = new LoginViewModel();
                Application.Current.MainPage = new LoginPage();
            }
        }
    }
}