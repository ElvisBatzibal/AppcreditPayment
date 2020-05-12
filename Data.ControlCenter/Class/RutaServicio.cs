using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ControlCenter.Class
{


    public class RutaServicio
    {
        /// <summary>
        /// Configuracion de conection de base de datos
        /// </summary>
        /// <returns></returns>
        public string BdConexionDBAzureManagementApp()
        {                        
            //return BdConexionDBAzure("SERVER", "DATABASE", "USER", "PASSWORD");
        }
        /// <summary>
        /// Conection String
        /// </summary>
        /// <param name="Server">Server</param>
        /// <param name="Databases">Databases</param>
        /// <param name="user">user</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        private String BdConexionDBAzure(string Server, string Databases, string user, string password)
        {
            string connectionString = "";
            try
            {
                connectionString = String.Format("Server={0};Database={1};User Id={2};Password={3}"
                    , Server
                    , Databases
                    , user
                    , password);

                return connectionString;

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
