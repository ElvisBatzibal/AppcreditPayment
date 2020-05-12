using FluentNHibernate.Automapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DomainModels.Models;
namespace webapiapp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            MapConfig();
        }
        void MapConfig()
        {
            AutoMapper.Mapper.CreateMap<Data.ControlCenter.Model.Account, AccountDTO>();
            AutoMapper.Mapper.CreateMap<AccountDTO, Data.ControlCenter.Model.Account>();

            AutoMapper.Mapper.CreateMap<Data.ControlCenter.Model.LogActualizacion, LogActualizacionDTO>();
            AutoMapper.Mapper.CreateMap<LogActualizacionDTO, Data.ControlCenter.Model.LogActualizacion>();

            AutoMapper.Mapper.CreateMap<Data.ControlCenter.Model.AccountUser, AccountUserDTO>();
            AutoMapper.Mapper.CreateMap<AccountUserDTO, Data.ControlCenter.Model.AccountUser>();

            AutoMapper.Mapper.CreateMap<Data.ControlCenter.Model.CreditAccount, CreditAccountDTO>();
            AutoMapper.Mapper.CreateMap<CreditAccountDTO, Data.ControlCenter.Model.CreditAccount>();

            AutoMapper.Mapper.CreateMap<Data.ControlCenter.Model.Customer, CustomerDTO>();
            AutoMapper.Mapper.CreateMap<CustomerDTO, Data.ControlCenter.Model.Customer>();
        }
    }
}