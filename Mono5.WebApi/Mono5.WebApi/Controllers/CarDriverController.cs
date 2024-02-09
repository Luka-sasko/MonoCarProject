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
        public class CarDriverController : ApiController
        {
            private readonly ICarDriverService CarDriverService;

            public CarDriverController(ICarDriverService carDriverService)
            {
                CarDriverService = carDriverService;
            }

            // POST api/cardriver/{carId}/driver/{driverId}
            [HttpPost]
            [Route("api/cardriver/{carId}/driver/{driverId}")]
            public HttpResponseMessage AddCarDriver(int carId, int driverId)
            {
                try
                {
                    CarDriverService.AddCarDriver(carId, driverId);
                    return Request.CreateResponse(HttpStatusCode.Created, "Car driver added successfully");
                }
                catch (ArgumentException ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }

            // DELETE api/cardriver/{carId}/driver/{driverId}
            [HttpDelete]
            [Route("api/cardriver/{carId}/driver/{driverId}")]
            public HttpResponseMessage DeleteCarDriver(int carId, int driverId)
            {
                try
                {
                    CarDriverService.DeleteCarDriver(driverId, carId);
                    return Request.CreateResponse(HttpStatusCode.OK, "Car driver deleted successfully");
                }
                catch (KeyNotFoundException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Car driver not found");
                }
            }

            // PUT api/cardriver/{carId}/driver/{driverId}
            [HttpPut]
            [Route("api/cardriver/{carId}/driver/{driverId}")]
            public HttpResponseMessage UpdateCarDriver(int carId, int driverId, [FromBody] CarDriverUpdate carDriverUpdate)
            {
                try
                {
                    CarDriverService.UpdateCarDriver(carId, driverId, carDriverUpdate.DriverId);
                    return Request.CreateResponse(HttpStatusCode.OK, "Car driver updated successfully");
                }
                catch (ArgumentException ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
        }
    }

}