using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Mono5.Model;
using Mono5.Service.Common;

namespace Mono5.WebApi.Controllers
{
    public class DriverController : ApiController
    {
        private readonly IDriverService DriverService;

        public DriverController(IDriverService driverService)
        {
            DriverService = driverService;
        }

        // GET api/driver/{id}
        public async Task<HttpResponseMessage> Get(int id)
        {
            try
            {
                var driver = await DriverService.GetDriverById(id);
                if (driver == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Driver was not found!");
                }
                return Request.CreateResponse(HttpStatusCode.OK, driver);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/driver
        public async Task<HttpResponseMessage> Post([FromBody] Driver newDriver)
        {
            try
            {
                await DriverService.AddDriver(newDriver);
                return Request.CreateResponse(HttpStatusCode.Created, newDriver);
            }
            catch (ArgumentException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/driver/{id}
        public async Task<HttpResponseMessage> Put(int id, [FromBody] DriverUpdate editedDriver)
        {
            try
            {
                await DriverService.UpdateDriver(id, editedDriver);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ArgumentException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/driver/{id}
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                await DriverService.DeleteDriver(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Driver deleted!");
            }
            catch (KeyNotFoundException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
