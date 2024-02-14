using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Mono5.Common;
using Mono5.Model;
using Mono5.Service;
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




        public async Task<HttpResponseMessage> Get(
            [FromUri] int pageNumber=1,
            [FromUri] int pageSize=10,
            [FromUri] string sortBy="",
            [FromUri] bool isAsc=true,
            [FromUri] string searchQuery=null,
            [FromUri] string model= null,
            [FromUri] string brand=null,
            [FromUri] int? manufacturYear=null)

        {
            try
            {
                Paging paging = new Paging { PageNumber=pageNumber,PageSize = pageSize };
                Sorting sorting = new Sorting { SortBy=sortBy, IsAsc=isAsc};
                CarFiltering carFiltering = new CarFiltering { SearchQuery = searchQuery, Brand = brand, Model = model, ManufacturYear = manufacturYear.GetValueOrDefault() };
                var cars = await CarService.GetCars(paging, sorting, carFiltering);
                if (cars == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Car was not found!");
                }
                return Request.CreateResponse(HttpStatusCode.OK, cars);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<HttpResponseMessage> Post([FromBody] Car newCar)
        {
            try
            {
                await CarService.AddCar(newCar);
                var car = await CarService.GetCarById(newCar.Id);
                return Request.CreateResponse(HttpStatusCode.Created, car);
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

        public async Task<HttpResponseMessage> Put(int id, [FromBody] CarUpdate editedCar)
        {
            try
            {
                await CarService.UpdateCar(id, editedCar);
                var car = await CarService.GetCarById(id);
                return Request.CreateResponse(HttpStatusCode.OK,car);
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

        public async Task<HttpResponseMessage> Delete(int id)
        {
            try
            {
                var car = await CarService.GetCarById(id);

                await CarService.DeleteCar(id);
                return Request.CreateResponse(HttpStatusCode.OK, car);
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
