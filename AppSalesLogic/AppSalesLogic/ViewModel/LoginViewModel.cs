using AppSalesLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using AppSalesLogic.Services;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using AppSalesLogic.Views;

namespace AppSalesLogic.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        private bool isEnabledLogin;
        private bool isEnabledCompany;
        private bool isEnabledRol;

        public bool IsRemember { get; set; }
        public int IdUsuario { get; set; }
        public int IdCompania { get; set; }
        public int IdRol { get; set; }
        public int IdRelacion { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand => new RelayCommand(Login);
        public ICommand RegisterCommand => new RelayCommand(Register);
        public ICommand LoginStaticCommand => new RelayCommand(LoginStatic);


        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public bool IsEnabledLogin
        {
            get => this.isEnabledLogin;
            set => this.SetValue(ref this.isEnabledLogin, value);
        }

        public bool IsEnabledCompany
        {
            get => this.isEnabledCompany;
            set => this.SetValue(ref this.isEnabledCompany, value);
        }

        public bool IsEnabledRol
        {
            get => this.isEnabledRol;
            set => this.SetValue(ref this.isEnabledRol, value);
        }

        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.IsEnabledLogin = true;
            this.IsEnabledCompany = false;
            this.IsRemember = true;
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.User))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Por favor ingrese un usuario.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Por favor ingrese una su contraseña.",
                    "Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "",
                    connection.Message,
                    "Accept");

                if (!connection.IsReachable)
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;
                    return;
                }
            }

            var usuario = new DomainModels.Models.AccountUserDTO
            {
                UserName = User,
                Password = Password
            };

            var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await this.apiService.PostAsync(
                url,
                "/api",
                "/AccountUser/Login",
                usuario);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error!",
                    response.Message,
                    "Accept");
                return;
            }

            var mobileResponse = (DomainModels.Models.AccountUserDTO)response.Result;

            if (mobileResponse.EntityID==0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error!",
                    "Usuario invalido",
                    "Accept");
                return;
            }

            this.IdUsuario = mobileResponse.EntityID;

            Settings.IsRemember = this.IsRemember;
            Settings.User = this.User;
            Settings.Password = this.Password;
            Settings.IdUsuario = this.IdUsuario;

            GoToMainPage();

            return;
        }

        private void Register()
        {
            return;
        }
        private void LoginStatic()
        {
            string KeyApp = Guid.NewGuid().ToString();
            //GenerateKey
            var KeyCreated = new Model.KeyAplicationDTO { KeyApp = KeyApp };
            var CreateInTable = new Services.DataService().AddItem(KeyCreated);
            GoToMainPage();
            //return;
        }

        public void GoToMainPage()
        {
            MainViewModel.GetInstance().Menu = new MenuViewModel();
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
    }
}
