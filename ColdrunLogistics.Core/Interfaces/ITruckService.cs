using ColdrunLogistics.Api.Models.Enums;
using ColdrunLogistics.Data.Models.Trucks;
using ColdrunLogistics.Models;
using ErrorOr;

namespace ColdrunLogistics.Core.Interfaces
{
    public interface ITruckService
    {
        ErrorOr<Created> CreateTruck(Truck truck);

        ErrorOr<Truck> GetTruckById(Guid id);

        ErrorOr<List<Truck>> GetTrucks(string? name = null, string? code = null, TruckStatusType? status = null, SortOptions? sortOptions = null);

        ErrorOr<Updated> UpdateTruck(Truck truck);

        ErrorOr<Deleted> DeleteTruck(Guid id);
    }
}