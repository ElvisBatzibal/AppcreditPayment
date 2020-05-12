using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBusinessConfig.Class
{

    public class ConectionDB
    {
        public string NombreDB { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string UserDB { get; set; }

        public ConectionDB()
        {
            Server = "GBPSOLUTIONS";
            NombreDB = "SBO_PRUEBA_LS";
            UserDB = "DevLogicaStudio";
            Password = "*Dev123";
        }
    }
}

