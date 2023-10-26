using ColdrunLogistics.Api.Models.Trucks;
using ColdrunLogistics.Data.Models.Trucks;

namespace ColdrunLogistics.Api.Mappers
{
    internal class TruckMapper
    {
        internal static TruckResponse MapToTruckResponse(Truck truck)
        {
            return new TruckResponse(
                truck.Id,
                truck.Name,
                truck.Code,
                truck.Description,
                truck.Status,
                truck.LastModifiedTime);
        }

        internal static TrucksResponse MapToTrucksResponse(List<Truck> trucks)
        {
            return new TrucksResponse(trucks.Select(t => MapToTruckResponse(t)).ToList());
        }
    }
}