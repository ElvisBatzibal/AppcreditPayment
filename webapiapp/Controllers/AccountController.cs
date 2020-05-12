using Data.ControlCenter;
using Newtonsoft.Json.Schema;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using webapiapp.Models;
using DomainModels.Models;
using FluentNHibernate.Utils;

namespace webapiapp.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class AccountController : ApiController
    {
        #region Conection Data
        //Crear la sesion para trabajar los QUERIES
        public void NHibernateSessionClose() { SessionManager.Instance.CloseSession(); }
        private ISession NHibernateSession
        {
            get
            {
                return SessionManager.Instance.GetSession();
            }
        }
        #endregion
        private Data.ControlCenter.Repository.AccountRepositorio uR = new Data.ControlCenter.Repository.AccountRepositorio();
        [HttpPost]
        [Route("api/Account/Create")]
        public ResponseDTO CrearRegistro([FromBody]AccountDTO value)
        {
            var Response = new ResponseDTO();
            if (String.IsNullOrEmpty(value.AccountName))
            {
                Response.Message = "AccountName es requerido";
                return Response;
            }
            if (value.CardCode == 0)
            {
                Response.Message = "CardCode es requerido";
                return Response;
            }
            if (value.InitialBalance == 0 || value.InitialBalance < 0)
            {
                Response.Message = "InitialBalance es requerido";
                return Response;
            }
            value.AccountRef = "SALES-" + value.CardCode.ToString();
            value.Status = "Abierto";

            Data.ControlCenter.Model.Account u = new Data.ControlCenter.Model.Account();
            u = AutoMapper.Mapper.Map<AccountDTO, Data.ControlCenter.Model.Account>(value);

            FuncionLogActualizaciones L = new FuncionLogActualizaciones();
            L.LogCreacion(u.Log);
            uR.SaveOrUpdate(u);
            if (u.EntityID > 0)
            {
                Response.Success = true;
                Response.Message = "Creado";
                Response.InternalKey = u.EntityID;

                u = uR.GetById(u.EntityID, false);
                u.AccountRef = "SALES-" + value.CardCode + "-" + Response.InternalKey;
                uR.SaveOrUpdate(u);
                uR.CommitChanges();

                CustomerController ApiCustomer = new CustomerController();
                var Cliente = ApiCustomer.GetByCardCode(u.CardCode);
                Cliente.Balance += value.InitialBalance;
                Cliente.Log.UserUpdate = u.Log.UserCreation;
                ApiCustomer.UpdateBalance(Cliente);
            }
           
           

            return Response;
        }

        [HttpPost]
        [Route("api/Account/Update")]
        public ResponseDTO UpdateRegistro([FromBody]AccountDTO value)
        {
            var Response = new ResponseDTO();
            Data.ControlCenter.Model.Account u;

            u = uR.GetById(value.EntityID, false);
            u = AutoMapper.Mapper.Map(value, u);

            FuncionLogActualizaciones L = new FuncionLogActualizaciones();
            L.LogModificacion(u.Log);

            uR.SaveOrUpdate(u);
            uR.CommitChanges();

            Response.Success = true;
            Response.Message = "Actualizado";

            return Response;
        }
        [HttpPost]
        [Route("api/Account/GetByAccountNum")]
        public AccountDTO GetByAccountNum(int EntityID)
        {
            Data.ControlCenter.Model.Account u;
            AccountDTO uDTO = new AccountDTO();

            u = uR.GetById(EntityID, false);

            uDTO = AutoMapper.Mapper.Map<Data.ControlCenter.Model.Account, AccountDTO>(u);

            return uDTO;
        }
        [HttpPost]
        [Route("api/Account/GetById")]
        public AccountDTO GetById([FromBody]AccountDTO value)
        {
            Data.ControlCenter.Model.Account u;
            AccountDTO uDTO = new AccountDTO();

            u = uR.GetById(value.EntityID, false);

            uDTO = AutoMapper.Mapper.Map<Data.ControlCenter.Model.Account, AccountDTO>(u);

            return uDTO;
        }
        [HttpPost]
        [Route("api/Account/GetAllCardCode")]
        public IList<AccountDTO> GetAllCardCode()
        {
            IList<Data.ControlCenter.Model.Account> u;

            u = uR.GetAll().ToList();

            IList<AccountDTO> uDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.Account>, IList<AccountDTO>>(u);

            return uDTO;
        }
        [HttpPost]
        [Route("api/Account/GetCardCode")]
        public List<AccountDTO> GetCardCode([FromBody]AccountDTO value)
        {
            var query = NHibernateSession.QueryOver<Data.ControlCenter.Model.Account>()
            .Where(de => de.CardCode == value.CardCode)
            .List();
            IList<AccountDTO> cDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.Account>, IList<AccountDTO>>(query);
            foreach (var item in cDTO)
            {
                item.PaidOpenToDate = item.InitialBalance - item.PaidToDate;
            }
            NHibernateSessionClose();
            return cDTO.ToList();
        }
    }
}
