using ColdrunLogistics.Api.Models.Enums;

namespace ColdrunLogistics.Api.Models.Trucks
{
    public record TruckResponse(
        Guid Id, 
        string Name, 
        string Code, 
        string Description, 
        TruckStatusType Status, 
        DateTime LastModifiedDate);
}