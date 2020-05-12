using AppSalesLogic.Helpers;
using AppSalesLogic.Services;
using AppSalesLogic.Views;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppSalesLogic.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {

        private DomainModels.Models.CustomerDTO _cliente;
        private DomainModels.Models.CustomerDTO _ClienteSelected;
        private bool _IsBusy;
        private ObservableCollection<DomainModels.Models.CustomerDTO> _ListClientes;
        private bool _IsEnabled;
        public DomainModels.Models.CustomerDTO Cliente
        {
            get => this._cliente;
            set => this.SetValue(ref this._cliente, value);
        }
        public DomainModels.Models.CustomerDTO ClienteSelected
        {
            get => this._ClienteSelected;
            set
            {
                this.SetValue(ref this._ClienteSelected, value);
                ShowUpdate();
            }
        }
        public ObservableCollection<DomainModels.Models.CustomerDTO> ListClientes
        {
            get => this._ListClientes;
            set => this.SetValue(ref this._ListClientes, value);
        }
        public bool IsEnabled
        {
            get => this._IsEnabled;
            set => this.SetValue(ref this._IsEnabled, value);
        }
        public bool IsBusy
        {
            get => this._IsBusy;
            set => this.SetValue(ref this._IsBusy, value);
        }
        public ICommand CreateCommand => new RelayCommand(Create);
        public async void Create()
        {
            IsBusy = true;
            IsEnabled = false;
            if (String.IsNullOrEmpty(Cliente.CustomerName))
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     "Por favor ingrese una nombre.",
                     "Accept");
                return;
            }
            if (String.IsNullOrEmpty(Cliente.Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     "Por favor ingrese una Direccion.",
                     "Accept");
                return;
            }
            if (String.IsNullOrEmpty(Cliente.Tel))
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     "Por favor ingrese una numero de celular.",
                     "Accept");
                return;
            }
            var connection = await ApiService.Instance.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "",
                    connection.Message,
                    "Accept");

                if (!connection.IsReachable)
                {
                    this.IsEnabled = true;
                    this.IsBusy = false;
                    return;
                }
            }

            Cliente.Log = new DomainModels.Models.LogActualizacionDTO();
            Cliente.Log.UserCreation = Settings.IdUsuario;
            Cliente.Log.UserUpdate = Settings.IdUsuario;
            try
            {
                var urlBase = Application.Current.Resources["UrlApi"].ToString();
                var prefix = "/api";
                var controller = "/Customer/Create";
                var request = JsonConvert.SerializeObject(Cliente);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };
                var url = $"{prefix}{controller}";
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    this.IsEnabled = true;
                    this.IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert(
                       "Error!",
                       answer,
                       "Accept");
                    return;
                }
                this.IsEnabled = true;
                this.IsBusy = false;
                var mobileResponse = JsonConvert.DeserializeObject<DomainModels.Models.ResponseDTO>(answer);

                if (mobileResponse.Success == false)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error!",
                       mobileResponse.Message,
                        "Accept");
                    return;
                }
                else
                {
                    Cliente = new DomainModels.Models.CustomerDTO();
                    LoadClientes();
                    await Application.Current.MainPage.DisplayAlert(
                        "Ok!",
                       mobileResponse.Message,
                        "Accept");
                    return;
                }
               
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                      "Error!",
                     ex.Message,
                      "Accept");
                return;
              
            }
        }

        public bool IsUpdate = false;
        public async void ShowUpdate()
        {
            MainViewModel.GetInstance().AccountClients = new AccountViewModel();
            MainViewModel.GetInstance().AccountClients.Cliente = ClienteSelected;           
           await App.Navigator.PushAsync(new AccountPage());
        }
        public async void LoadClientes()
        {
            IsBusy = true;
            IsEnabled = false;
                
            var connection = await ApiService.Instance.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "",
                    connection.Message,
                    "Accept");

                if (!connection.IsReachable)
                {
                    this.IsEnabled = true;
                    this.IsBusy = false;
                    return;
                }
            }
           
            try
            {
                var urlBase = Application.Current.Resources["UrlApi"].ToString();
                var prefix = "/api";
                var controller = "/Customer/GetAllCardCode";
                //var request = JsonConvert.SerializeObject(model);
                var content = new StringContent("", Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };
                var url = $"{prefix}{controller}";
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    this.IsEnabled = true;
                    this.IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert(
                       "Error!",
                       answer,
                       "Accept");
                    return;
                }
                this.IsEnabled = true;
                this.IsBusy = false;
                var mobileResponse = JsonConvert.DeserializeObject<List<DomainModels.Models.CustomerDTO>>(answer);

                this.ListClientes = new ObservableCollection<DomainModels.Models.CustomerDTO>(mobileResponse);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                                   "Error!",
                                   ex.Message,
                                   "Accept");
                return;
            }
        }
        public CustomerViewModel()
        {
            IsEnabled = true;
            IsBusy = false;
            Cliente = new DomainModels.Models.CustomerDTO();
            LoadClientes();
        }
    }
}
