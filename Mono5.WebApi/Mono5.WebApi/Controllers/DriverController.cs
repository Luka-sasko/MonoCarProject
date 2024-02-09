using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mono5.WebApi.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using global::Mono5.Model;
    using global::Mono5.Service.Common;
    

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
            public HttpResponseMessage Get(int id)
            {
                var driver = DriverService.GetDriverById(id);
                if (driver == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Driver was not found!");
                }
                return Request.CreateResponse(HttpStatusCode.OK, driver);
            }

            // POST api/driver
            public HttpResponseMessage Post([FromBody] Driver newDriver)
            {
                try
                {
                    DriverService.AddDriver(newDriver);
                    return Request.CreateResponse(HttpStatusCode.Created, newDriver);
                }
                catch (ArgumentException ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }

            // PUT api/driver/{id}
            public HttpResponseMessage Put(int id, [FromBody] DriverUpdate editedDriver)
            {
                try
                {
                    DriverService.UpdateDriver(id, editedDriver);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (ArgumentException ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }

            // DELETE api/driver/{id}
            public HttpResponseMessage Delete(int id)
            {
                try
                {
                    DriverService.DeleteDriver(id);
                    return Request.CreateResponse(HttpStatusCode.OK, "Driver deleted!");
                }
                catch (KeyNotFoundException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
        }
    }

}