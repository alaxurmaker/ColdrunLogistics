using ColdrunLogistics.Api.Models.Enums;

namespace ColdrunLogistics.Api.Models.Trucks
{
    public record UpdateTruckRequest(
        string Name,
        string Code,
        string Description,
        TruckStatusType Status);
}