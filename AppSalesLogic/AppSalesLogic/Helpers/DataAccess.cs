using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using AppSalesLogic.Interfaces;
namespace AppSalesLogic.Helpers
{
    public class DataAccess
    {
        public SQLiteAsyncConnection connection;
        public SQLiteConnection connectionNotAsync;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            this.connection = new SQLiteAsyncConnection(
                Path.Combine(config.DirectoryDB, "AppSalesLogic.db3")
                );

            connection.CreateTableAsync<Model.KeyAplicationDTO>().Wait();

            connectionNotAsync = new SQLiteConnection(Path.Combine(config.DirectoryDB, "AppSalesLogic.db3"));
        }
    }
}
