using ColdrunLogistics.Api.Mappers;
using ColdrunLogistics.Api.Models.Trucks;
using ColdrunLogistics.Api.Models.Trucks.Requests;
using ColdrunLogistics.Core.Interfaces;
using ColdrunLogistics.Data.Models.Trucks;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace ColdrunLogistics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrucksController : ApiController
    {
        private readonly ITruckService _truckService;

        private readonly ILogger<TrucksController> _logger;
    
        public TrucksController(ITruckService truckService, ILogger<TrucksController> logger)
        {
            _truckService = truckService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CreateTruck(CreateTruckRequest request)
        {
            ErrorOr<Truck> requestToTruckResult = Truck.From(request);

            if (requestToTruckResult.IsError)
            {
                return Problem(requestToTruckResult.Errors);
            }

            Truck truck = requestToTruckResult.Value;
            ErrorOr<Created> createTruckResult = _truckService.CreateTruck(truck);

            return createTruckResult.Match(
                created => CreatedAtGetTruck(truck),
                errors => Problem(errors));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetTruck(Guid id)
        {
            ErrorOr<Truck> getTruckResult = _truckService.GetTruckById(id);

            return getTruckResult.Match(
                truck => Ok(TruckMapper.MapToTruckResponse(truck)),
                errors => Problem(errors));
        }

        [HttpGet("search")]
        public IActionResult SearchTrucks([FromQuery] GetTrucksRequest request)
        {
            ErrorOr<List<Truck>> getTrucksResult = _truckService.GetTrucks(request.Name, request.Code, request.Status, new Models.SortOptions(request.OrderBy, request.IsDescending));

            return getTrucksResult.Match(
                trucks => Ok(TruckMapper.MapToTrucksResponse(trucks)),
                errors => Problem(errors));
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateTruck(Guid id, UpdateTruckRequest request)
        {
            ErrorOr<Truck> requestToTruckResult = Truck.From(id, request);

            if (requestToTruckResult.IsError)
            {
                return Problem(requestToTruckResult.Errors);
            }

            var truck = requestToTruckResult.Value;
            ErrorOr<Updated> updateTruckResult = _truckService.UpdateTruck(truck);

            return updateTruckResult.Match(
                updated => NoContent(),
                errors => Problem(errors));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteTruck(Guid id)
        {
            ErrorOr<Deleted> deleteTruckResult = _truckService.DeleteTruck(id);

            return deleteTruckResult.Match(
                deleted => NoContent(),
                errors => Problem(errors));
        }

        private CreatedAtActionResult CreatedAtGetTruck(Truck truck)
        {
            return CreatedAtAction(
                actionName: nameof(GetTruck),
                routeValues: new { id = truck.Id },
                value: TruckMapper.MapToTruckResponse(truck));
        }
    }
}