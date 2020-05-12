using Data.ControlCenter;
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
using Microsoft.Web.Infrastructure.DynamicValidationHelper;

namespace webapiapp.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class CreditAccountController : ApiController
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
        private Data.ControlCenter.Repository.CreditAccountRepositorio uR = new Data.ControlCenter.Repository.CreditAccountRepositorio();

        [HttpPost]
        [Route("api/CreditAccount/Create")]
        public ResponseDTO CrearRegistro([FromBody]CreditAccountDTO value)
        {
            var Response = new ResponseDTO();

            Data.ControlCenter.Model.CreditAccount u = new Data.ControlCenter.Model.CreditAccount();
            u = AutoMapper.Mapper.Map<CreditAccountDTO, Data.ControlCenter.Model.CreditAccount>(value);

            FuncionLogActualizaciones L = new FuncionLogActualizaciones();
            L.LogCreacion(u.Log);
            uR.SaveOrUpdate(u);
            if (u.EntityID > 0)
            {
                Response.Success = true;
                Response.Message = "Creado";

                AccountController ApiCuenta = new AccountController();
                var Reg = ApiCuenta.GetByAccountNum(value.AccountNum);
                Reg.PaidToDate += value.DocTotal;
                Reg.Log.UserUpdate = u.Log.UserCreation;
                ApiCuenta.UpdateRegistro(Reg);

                CustomerController ApiCustomer = new CustomerController();
                var Cliente = ApiCustomer.GetByCardCode(Reg.CardCode);
                Cliente.Balance -= value.DocTotal;
                Cliente.Log.UserUpdate = u.Log.UserCreation;
                ApiCustomer.UpdateBalance(Cliente);
            }
            return Response;
        }

        [HttpPost]
        [Route("api/CreditAccount/Update")]
        public ResponseDTO UpdateRegistro([FromBody]CreditAccountDTO value)
        {
            var Response = new ResponseDTO();
            Data.ControlCenter.Model.CreditAccount u;

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
        [Route("api/CreditAccount/GetCardCode")]
        public CreditAccountDTO GetCardCode([FromBody]CreditAccountDTO value)
        {
            Data.ControlCenter.Model.CreditAccount u;
            CreditAccountDTO uDTO = new CreditAccountDTO();

            u = uR.GetById(value.EntityID, false);

            uDTO = AutoMapper.Mapper.Map<Data.ControlCenter.Model.CreditAccount, CreditAccountDTO>(u);

            return uDTO;
        }
        [HttpPost]
        [Route("api/CreditAccount/GetAllCardCode")]
        public IList<CreditAccountDTO> GetAllCardCode()
        {
            IList<Data.ControlCenter.Model.CreditAccount> u;

            u = uR.GetAll().ToList();

            IList<CreditAccountDTO> uDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.CreditAccount>, IList<CreditAccountDTO>>(u);

            return uDTO;
        }
    }
}
