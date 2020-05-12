using AppSalesLogic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
namespace AppSalesLogic.ViewModel
{
    /// <summary>
    /// Patrón Singleton
    ///     1. Crear un atributo privado y estático de la clase.
    ///     2. Crear una instancia de esa clase en el constructor con el atributo private.
    ///     3. Crear un método público y estático de la clase.
    ///     4. Si la instancia es nula, retornar una nueva instancia de la clase.
    /// </summary>
    public class MainViewModel
    {
        // Paso 1
        private static MainViewModel instance;

        public string User { get; set; }
        public string Password { get; set; }        
        public int IdUsuario { get; set; }
        public string KeyAplication { get; set; }

        /// <summary>
        /// Inicio de sesión y navegación. 
        /// </summary>
        public LoginViewModel Login { get; set; }
        public MenuViewModel Menu { get; set; }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        public HomeViewModel Home { get; set; }
        public CustomerViewModel Customer { get; set; }
        public AccountViewModel AccountClients { get; set; }
        public CreditAccountViewModel CreditAccount { get; set; }       

        public MainViewModel()
        {
            // Paso 2
            instance = this;
            this.LoadMenus();
        }

        private void LoadMenus()
        {
            var menus = new List<Menu>
            {
                new Menu
                {
                    Icon= "plus_flat",
                    PageName="Customer",
                    Title ="Cliente"
                }, 

                new Menu
                {
                    Icon = "plus_flat",
                    PageName = "",
                    Title = "LogOut"
                }
            };

            this.Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel()
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }

        // Paso 3
        public static MainViewModel GetInstance()
        {
            // Paso 4
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }


}
