using AppSalesLogic.Model;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppSalesLogic.Services
{
    public class ApiService
    {
        private static ApiService _instance;
        public static ApiService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ApiService();
                return _instance;
            }
        }

        /// <summary>
        /// Valida la conexión a internet al momento de consumir un api. 
        /// </summary>
        /// <returns></returns>
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsReachable = false,
                    IsSuccess = false,
                    Message = "Por favor verifique su conexión a internet."
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsReachable = true,
                    IsSuccess = false,
                    Message = "La conexión a internet es lenta, por favor espere..."
                };
            }

            return new Response
            {
                IsSuccess = true
            };
        }

        /// <summary>
        /// Método genérico para obtener listas de un web service. 
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="urlBase">UrlBase</param>
        /// <param name="prefix">Prefijo</param>
        /// <param name="controller">Controlador</param>
        /// <returns></returns>
        public async Task<Response> GetList<T>(
            string urlBase,
            string prefix,
            string controller,
            string action,
            string bd,
            string id)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                var url = $"{prefix}{controller}{action}{bd}{id}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Método POST genérico para peticiones a la base de datos. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlBase">UrlBase</param>
        /// <param name="prefix">Prefijo</param>
        /// <param name="controller">Controlador</param>
        /// <param name="model">Modelo</param>
        /// <returns></returns>
        public async Task<Response> PostAsync<T>(
            string urlBase,
            string prefix,
            string controller,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
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
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Método genérico para obtener un objeto de un web service. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlBase">UrlBase</param>
        /// <param name="prefix">Prefijo</param>
        /// <param name="controller">Controlador</param>
        /// <param name="action"></param>
        /// <param name="bd"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Response> GetObject<T>(
            string urlBase,
            string prefix,
            string controller,
            string action,
            string bd,
            string id)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                var url = $"{prefix}{controller}{action}{bd}{id}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
