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
using FluentNHibernate.Utils;

namespace webapiapp.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class CustomerController : ApiController
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
        private Data.ControlCenter.Repository.CustomerRepositorio uR = new Data.ControlCenter.Repository.CustomerRepositorio();


        // POST: api/Customer
        [HttpPost]
        [Route("api/Customer/Create")]
        public ResponseDTO CrearRegistro([FromBody]CustomerDTO value)
        {
            var Response = new ResponseDTO();
            try
            {
                Data.ControlCenter.Model.Customer u = new Data.ControlCenter.Model.Customer();
                u = AutoMapper.Mapper.Map<CustomerDTO, Data.ControlCenter.Model.Customer>(value);

                FuncionLogActualizaciones L = new FuncionLogActualizaciones();
                L.LogCreacion(u.Log);
                uR.SaveOrUpdate(u);
                if (u.EntityID > 0)
                {
                    Response.Success = true;
                    Response.Message = "Creado";
                    Response.InternalKey = u.EntityID;
                }
                else
                {
                    Response.Message = "No Creado";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Response;
        }

        [HttpPost]
        [Route("api/Customer/Update")]
        public ResponseDTO UpdateRegistro([FromBody]CustomerDTO value)
        {

            var Response = new ResponseDTO();
            if (value.EntityID == 0)
            {
                Response.Message = "EntityID es requerido";
            }
            try
            {
                Data.ControlCenter.Model.Customer u;

                u = uR.GetById(value.EntityID, false);
                u.Address = value.Address;
                u.CustomerName = value.CustomerName;
                u.Tel = value.Tel;
                u.Log.UserUpdate = value.Log.UserUpdate;
                // u = AutoMapper.Mapper.Map(value, u);

                FuncionLogActualizaciones L = new FuncionLogActualizaciones();
                L.LogModificacion(u.Log);

                uR.SaveOrUpdate(u);
                uR.CommitChanges();

                Response.Success = true;
                Response.Message = "Actualizado";

            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }
            return Response;
        }

        public ResponseDTO UpdateBalance([FromBody]CustomerDTO value)
        {

            var Response = new ResponseDTO();
            if (value.EntityID == 0)
            {
                Response.Message = "EntityID es requerido";
            }
            try
            {
                Data.ControlCenter.Model.Customer u;

                u = uR.GetById(value.EntityID, false);
                u.Balance = value.Balance;
                u.Log.UserUpdate = value.Log.UserUpdate;
                FuncionLogActualizaciones L = new FuncionLogActualizaciones();
                L.LogModificacion(u.Log);

                uR.SaveOrUpdate(u);
                uR.CommitChanges();

                Response.Success = true;
                Response.Message = "Actualizado";

            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
            }
            return Response;
        }
        [HttpPost]
        [Route("api/Customer/GetByCardCode")]
        public CustomerDTO GetByCardCode(int EntityID)
        {
            Data.ControlCenter.Model.Customer u;
            CustomerDTO uDTO = new CustomerDTO();

            u = uR.GetById(EntityID, false);

            uDTO = AutoMapper.Mapper.Map<Data.ControlCenter.Model.Customer, CustomerDTO>(u);

            return uDTO;
        }
        [HttpPost]
        [Route("api/Customer/GetCardCode")]
        public CustomerDTO GetCardCode([FromBody]CustomerDTO value)
        {
            Data.ControlCenter.Model.Customer u;
            CustomerDTO uDTO = new CustomerDTO();

            u = uR.GetById(value.EntityID, false);

            uDTO = AutoMapper.Mapper.Map<Data.ControlCenter.Model.Customer, CustomerDTO>(u);

            return uDTO;
        }
        [HttpPost]
        [Route("api/Customer/GetAllCardCode")]
        public List<CustomerDTO> GetAllCardCode()
        {
            IList<Data.ControlCenter.Model.Customer> u;

            u = uR.GetAll().ToList();

            IList<CustomerDTO> uDTO = AutoMapper.Mapper.Map<IList<Data.ControlCenter.Model.Customer>, IList<CustomerDTO>>(u);

            return uDTO.ToList();
        }
    }
}
