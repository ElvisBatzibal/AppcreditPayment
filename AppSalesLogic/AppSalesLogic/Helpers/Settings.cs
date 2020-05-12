using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSalesLogic.Helpers
{
    public static class Settings
    {
        private const string user = "user";
        private const string password = "password";
        private const string idUsuario = "idUsuario";
        private const string isRemember = "isRemember";
        private const string config = "config";
        private static readonly string stringDefault = string.Empty;
        private static readonly bool boolDefault = false;
        private static readonly int intDefault = 0;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string User
        {
            get => AppSettings.GetValueOrDefault(user, stringDefault);
            set => AppSettings.AddOrUpdateValue(user, value);
        }

        public static string Password
        {
            get => AppSettings.GetValueOrDefault(password, stringDefault);
            set => AppSettings.AddOrUpdateValue(password, value);
        }

        public static int IdUsuario
        {
            get => AppSettings.GetValueOrDefault(idUsuario, intDefault);
            set => AppSettings.AddOrUpdateValue(idUsuario, value);
        }
        public static bool IsRemember
        {
            get => AppSettings.GetValueOrDefault(isRemember, boolDefault);
            set => AppSettings.AddOrUpdateValue(isRemember, value);
        }

        public static string Config
        {
            get => AppSettings.GetValueOrDefault(config, stringDefault);
            set => AppSettings.AddOrUpdateValue(config, value);
        }
    }
}


