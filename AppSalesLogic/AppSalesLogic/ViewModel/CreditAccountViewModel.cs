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
    public class CreditAccountViewModel : BaseViewModel
    {
        private DomainModels.Models.AccountDTO _Cuenta;
        private DomainModels.Models.CreditAccountDTO _PayAccountCreate;
        private bool _IsBusy;
        private bool _IsEnabled;


        public DomainModels.Models.AccountDTO Cuenta
        {
            get => this._Cuenta;
            set => this.SetValue(ref this._Cuenta, value);
        }
        public DomainModels.Models.CreditAccountDTO PayAccountCreate
        {
            get => this._PayAccountCreate;
            set => this.SetValue(ref this._PayAccountCreate, value);
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
            if (PayAccountCreate.DocTotal==0)
            {
                await Application.Current.MainPage.DisplayAlert(
                     "Error",
                     "Por favor ingrese un valor.",
                     "Accept");
                return;
            }

            PayAccountCreate.AccountNum = Cuenta.EntityID;            
          
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

            PayAccountCreate.Log = new DomainModels.Models.LogActualizacionDTO();
            PayAccountCreate.Log.UserCreation = Settings.IdUsuario;
            PayAccountCreate.Log.UserUpdate = Settings.IdUsuario;
            try
            {
                var urlBase = Application.Current.Resources["UrlApi"].ToString();
                var prefix = "/api";
                var controller = "/CreditAccount/Create";
                var request = JsonConvert.SerializeObject(PayAccountCreate);
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
                    PayAccountCreate = new DomainModels.Models.CreditAccountDTO();
                    //MainViewModel.GetInstance().AccountClients =new AccountViewModel();
                    await App.Navigator.PopAsync();
                    await App.Navigator.PopAsync();
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
        public CreditAccountViewModel(DomainModels.Models.AccountDTO CuentaPay)
        {
            Cuenta = CuentaPay;
            IsEnabled = true;
            IsBusy = false;
            PayAccountCreate = new DomainModels.Models.CreditAccountDTO();
        }
    }
}
