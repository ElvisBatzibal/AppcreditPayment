using AppSalesLogic.Helpers;
using AppSalesLogic.Services;
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
    public class AccountViewModel : BaseViewModel
    {
        private DomainModels.Models.CustomerDTO _cliente;
        private DomainModels.Models.AccountDTO _AccountCreate;
        private DomainModels.Models.AccountDTO _AccountSelected;
        private bool _IsBusy;
        private ObservableCollection<DomainModels.Models.AccountDTO> _ListAccount;
        private bool _IsEnabled;
        private bool _ShowCreate;
        private bool _IsProcessCreate;
        private string _TextCreate;

        public DomainModels.Models.CustomerDTO Cliente
        {
            get => this._cliente;
            set => this.SetValue(ref this._cliente, value);
        }
        public DomainModels.Models.AccountDTO AccountCreate
        {
            get => this._AccountCreate;
            set => this.SetValue(ref this._AccountCreate, value);
        }
        public DomainModels.Models.AccountDTO AccountSelected
        {
            get => this._AccountSelected;
            set
            {
                this.SetValue(ref this._AccountSelected, value);
                ShowAddPayment();
            }
        }
        public ObservableCollection<DomainModels.Models.AccountDTO> ListAccount
        {
            get => this._ListAccount;
            set => this.SetValue(ref this._ListAccount, value);
        }

        public bool IsEnabled
        {
            get => this._IsEnabled;
            set => this.SetValue(ref this._IsEnabled, value);
        }
        public string TextCreate
        {
            get => this._TextCreate;
            set => this.SetValue(ref this._TextCreate, value);
        }
        public bool ShowCreate
        {
            get => this._ShowCreate;
            set => this.SetValue(ref this._ShowCreate, value);
        }
        public bool IsProcessCreate
        {
            get => this._IsProcessCreate;
            set => this.SetValue(ref this._IsProcessCreate, value);
        }
        public bool IsBusy
        {
            get => this._IsBusy;
            set => this.SetValue(ref this._IsBusy, value);
        }
        private List<Model.ListCurrency> _ListPickerCurrency;
        public List<Model.ListCurrency> ListPickerCurrency
        {
            get => this._ListPickerCurrency;
            set => SetValue(ref this._ListPickerCurrency, value);
        }

        private Model.ListCurrency _selectedCurrency;
        public Model.ListCurrency SelectedCurrency
        {
            get => this._selectedCurrency;
            set
            {
                SetValue(ref this._selectedCurrency, value);
            }
        }
        public AccountViewModel()
        {
           
            IsEnabled = true;
            IsBusy = false;
            ShowCreate = false;
            TextCreate = "Crear Cuenta";
            LoadAccount();
            ListPickerCurrency = Model.ListCurrency.GetList();
            AccountCreate = new DomainModels.Models.AccountDTO();
        }
        public ICommand UpdateClienteCommand => new RelayCommand(UpdateCliente);
        public ICommand CreateAccountCommand => new RelayCommand(ShowCreateAccount);
        public void ShowCreateAccount()
        {
            if (IsProcessCreate == true)
            {
                CrearteAccount();
            }
            else
            {
                TextCreate = "Crear";
            }
            ShowCreate = true;
            IsProcessCreate = true;
            return;
        }
        public async void CrearteAccount()
        {
            IsBusy = true;
            IsEnabled = false;
            if (String.IsNullOrEmpty(AccountCreate.AccountName))
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     "Por favor ingrese una cuenta.",
                     "Accept");
                return;
            }

            if (AccountCreate.InitialBalance == 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     "Por favor ingrese un saldo inicial.",
                     "Accept");
                return;
            }
            if (SelectedCurrency == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     "Por favor ingrese una Moneda.",
                     "Accept");
                return;
            }
            AccountCreate.Currency = SelectedCurrency.Cur;

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

            AccountCreate.CardCode = Cliente.EntityID;

            AccountCreate.Log = new DomainModels.Models.LogActualizacionDTO();
            AccountCreate.Log.UserCreation = Settings.IdUsuario;
            try
            {
                var urlBase = Application.Current.Resources["UrlApi"].ToString();
                var prefix = "/api";
                var controller = "/Account/Create";
                var request = JsonConvert.SerializeObject(AccountCreate);
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

                    await Application.Current.MainPage.DisplayAlert(
                        "Ok!",
                       mobileResponse.Message,
                        "Accept");

                }
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert(
                            "Error!",
                           ex.Message,
                            "Accept");

            }
            ShowCreate = false;
            IsProcessCreate = false;
            TextCreate = "Crear Cuenta";
            LoadAccount();
            return;
        }

        public async void UpdateCliente()
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
            Cliente.Log.UserUpdate = Settings.IdUsuario;
            try
            {
                var urlBase = Application.Current.Resources["UrlApi"].ToString();
                var prefix = "/api";
                var controller = "/Customer/Update";
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
        public async void LoadAccount()
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
                var controller = "/Account/GetCardCode";
                var AccountRequest = new DomainModels.Models.AccountDTO
                {
                    CardCode = Cliente.EntityID
                };
                var request = JsonConvert.SerializeObject(AccountRequest);
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
                var mobileResponse = JsonConvert.DeserializeObject<List<DomainModels.Models.AccountDTO>>(answer);

                this.ListAccount = new ObservableCollection<DomainModels.Models.AccountDTO>(mobileResponse);
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
        public async void ShowAddPayment()
        {
            MainViewModel.GetInstance().CreditAccount = new CreditAccountViewModel(AccountSelected);
            await App.Navigator.PushAsync(new Views.CreditAccountPage());
        }
    }
}
