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
        public async Task<HttpResponseMessage> AddCarDriver(int carId, int driverId)
        {
            try
            {
                await CarDriverService.AddCarDriver(carId, driverId);
                return Request.CreateResponse(HttpStatusCode.Created, "Car driver added successfully");
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

        // DELETE api/cardriver/{carId}/driver/{driverId}
        [HttpDelete]
        [Route("api/cardriver/{carId}/driver/{driverId}")]
        public async Task<HttpResponseMessage> DeleteCarDriver(int carId, int driverId)
        {
            try
            {
                await CarDriverService.DeleteCarDriver(driverId, carId);
                return Request.CreateResponse(HttpStatusCode.OK, "Car driver deleted successfully");
            }
            catch (KeyNotFoundException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Car driver not found");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/cardriver/{carId}/driver/{driverId}
        [HttpPut]
        [Route("api/cardriver/{carId}/driver/{driverId}")]
        public async Task<HttpResponseMessage> UpdateCarDriver(int carId, int driverId, [FromBody] CarDriverUpdate carDriverUpdate)
        {
            try
            {
                await CarDriverService.UpdateCarDriver(carId, driverId, carDriverUpdate.DriverId);
                return Request.CreateResponse(HttpStatusCode.OK, "Car driver updated successfully");
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
    }
}
