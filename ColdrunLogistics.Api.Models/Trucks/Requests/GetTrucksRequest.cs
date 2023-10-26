using ColdrunLogistics.Api.Models.Enums;

namespace ColdrunLogistics.Api.Models.Trucks.Requests
{
    public record GetTrucksRequest(
        string Name,
        string Code,
        TruckStatusType Status,
        string OrderBy,
        bool IsDescending);
}
