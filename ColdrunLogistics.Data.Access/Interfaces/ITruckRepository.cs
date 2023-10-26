using ColdrunLogistics.Api.Models.Enums;
using ColdrunLogistics.Data.Models.Trucks;
using ColdrunLogistics.Models;

namespace ColdrunLogistics.Data
{
    public interface ITruckRepository
    {
        void CreateTruck(Truck truck);

        Truck? GetTruckById(Guid truckId);

        List<Truck> GetTrucks(string? name = null, string? code = null, TruckStatusType? status = null, SortOptions? sortOptions = null);

        void UpdateTruck(Truck truck);

        void DeleteTruck(Guid truckId);
    }
}