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
    public class CarController : ApiController
    {
        private readonly ICarService CarService;

        public CarController(ICarService carService)
        {
            CarService = carService;
        }

        // GET api/car/{id}
        public async Task<HttpResponseMessage> Get(int id)
        {
            try
            {
                var car = await CarService.GetCarById(id);
                if (car == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Car was not found!");
                }
                return Request.CreateResponse(HttpStatusCode.OK, car);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/car
        public async Task<HttpResponseMessage> Post([FromBody] Car newCar)
        {
            try
            {
                await CarService.AddCar(newCar);
                return Request.CreateResponse(HttpStatusCode.Created, newCar);
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

        // PUT api/car/{id}
        public async Task<HttpResponseMessage> Put(int id, [FromBody] CarUpdate editedCar)
        {
            try
            {
                await CarService.UpdateCar(id, editedCar);
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

        // DELETE api/car/{id}
        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                await CarService.DeleteCar(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Car deleted!");
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
