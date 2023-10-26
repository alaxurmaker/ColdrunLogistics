using ColdrunLogistics.Api.Models.Enums;
using ColdrunLogistics.Core.Interfaces;
using ColdrunLogistics.Data;
using ColdrunLogistics.Data.Models.Trucks;
using ColdrunLogistics.Models;
using ColdrunLogistics.Models.Trucks.TruckStatus;
using ColdrunLogistics.Models.Utilities.Errors;
using ErrorOr;

namespace ColdrunLogistics.Core.Services
{
    public class TruckService : ITruckService
    {
        private readonly ITruckRepository _truckRepository;

        public TruckService(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }

        public ErrorOr<Created> CreateTruck(Truck truck)
        {
            _truckRepository.CreateTruck(truck);

            return Result.Created;
        }

        public ErrorOr<Truck> GetTruckById(Guid id)
        {
            Truck? truck = _truckRepository.GetTruckById(id);

            if (truck == default)
            {
                return TruckErrors.NotFound;
            }

            return truck;
        }

        public ErrorOr<List<Truck>> GetTrucks(string? name = null, string? code = null, TruckStatusType? status = null, SortOptions? sortOptions = null)
        {
            List<Truck> trucks = _truckRepository.GetTrucks(name, code, status, sortOptions);

            if (trucks == default)
            {
                return TruckErrors.NotFound;
            }

            return trucks;
        }

        public ErrorOr<Updated> UpdateTruck(Truck truck)
        {
            Truck? existingTruck = _truckRepository.GetTruckById(truck.Id);
            if (existingTruck == null)
            {
                return TruckErrors.NotFound;
            }

            TruckStatusValidator statusValidator = new TruckStatusValidator(currentStatus: existingTruck.Status);
            if (!statusValidator.IsStatusAllowedToChange(truck.Status))
            {
                return TruckErrors.InvalidStatus;
            }

            _truckRepository.UpdateTruck(truck);

            return Result.Updated;
        }

        public ErrorOr<Deleted> DeleteTruck(Guid id)
        {
            _truckRepository.DeleteTruck(id);   

            return Result.Deleted;
        }
    }
}