using AppSalesLogic.Helpers;
using AppSalesLogic.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppSalesLogic.ViewModel
{
    public class MenuItemViewModel : Model.Menu
    {
        public ICommand SelectMenuCommand => new RelayCommand(this.SelectMenu);

        private async void SelectMenu()
        {
            App.Master.IsPresented = false;
            var mainViewModel = MainViewModel.GetInstance();

            switch (this.PageName)
            {
                case "Customer":
                    mainViewModel.Customer = new CustomerViewModel();
                    await App.Navigator.PushAsync(new CustomerPage());
                    break;          
              
        
                default:
                    Settings.IsRemember = false;
                    Settings.User = string.Empty;
                    Settings.Password = string.Empty;
                    Settings.IdUsuario = 0;
                    Settings.Config = string.Empty;

                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }
        }
    }
}
