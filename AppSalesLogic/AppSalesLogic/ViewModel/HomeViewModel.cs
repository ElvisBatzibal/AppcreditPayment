using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppSalesLogic.ViewModel
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {

        }
        public ICommand CustomerPageCommand => new RelayCommand(OpenCustomerPage);
        public async void OpenCustomerPage()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Customer = new CustomerViewModel();
            await App.Navigator.PushAsync(new Views.CustomerPage());
            return;
        }
    }
}
