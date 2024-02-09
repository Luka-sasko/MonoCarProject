using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
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
        public HttpResponseMessage Get(int id)
        {
            var car = CarService.GetCarById(id);
            if (car == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Car was not found!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, car);
        }

        // POST api/car
        public HttpResponseMessage Post([FromBody] Car newCar)
        {
            try
            {
                CarService.AddCar(newCar);
                return Request.CreateResponse(HttpStatusCode.Created, newCar);
            }
            catch (ArgumentException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // PUT api/car/{id}
        public HttpResponseMessage Put(int id, [FromBody] CarUpdate editedCar)
        {
            try
            {
                CarService.UpdateCar(id, editedCar);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ArgumentException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE api/car/{id}
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                CarService.DeleteCar(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Car deleted!");
            }
            catch (KeyNotFoundException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}