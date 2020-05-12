using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using AppSalesLogic.Services;
using AppSalesLogic.Helpers;

namespace AppSalesLogic.Services
{
    public class DataService
    {
        private static DataService _instance;
        public static DataService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataService();
                return _instance;
            }
        }

        //public SQLiteAsyncConnection AccessDB = new DataAccess().connection;
        public SQLiteConnection AccessDBNotAsync = new DataAccess().connectionNotAsync;

        //public   DataService()
        //{
        //    AccessDB =new DataAccess().connection;

        //}
        public int AddItem<T>(T item)
        {
            return AccessDBNotAsync.Insert(item);
        }

        public int DeleteItem<T>(T item)
        {
            return AccessDBNotAsync.Delete(item);
        }

        public Model.KeyAplicationDTO GetKeyApp()
        {
            return AccessDBNotAsync.Table<Model.KeyAplicationDTO>().FirstOrDefault();
        }


        //public List<T> FindItems<T>(Expression<Func<T, bool>> predicate)
        //{

        //    return AccessDB.Table<T>().Where(predicate).ToList();
        //}

        //public List<T> GetAllItems<T>() where T : class
        //{
        //    return AccessDBNotAsync.Table<T>().ToList();
        //}

        //public T GetItem<T>(string pk)
        //{
        //    return AccessDBNotAsync.Get<T>(pk);
        //}

        //public async Task<T> GetItem<T>(Expression<Func<T, bool>> predicate)
        //{
        //    return await AccessDB.GetAsync<T>(predicate);
        //}

        public int InsertItem<T>(T item)
        {
            return AccessDBNotAsync.Insert(item);
        }

        public bool InsertItems<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                AccessDBNotAsync.Insert(item);
            }
            return true;
        }

        public int UpdateItem<T>(T item)
        {

            return AccessDBNotAsync.InsertOrReplace(item);
        }
        public void DeleteLocalKeyApp()
        {
            try
            {
                var dd = AccessDBNotAsync.Table<Model.KeyAplicationDTO>().ToList();

                foreach (var oldRecord in dd)
                {
                    AccessDBNotAsync.Delete(oldRecord);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private bool isSucces(int rowsAffected)
        {
            if (rowsAffected > 0)
                return true;
            return false;
        }

    }



}