namespace ColdrunLogistics.Api.Models.Trucks
{
    public record CreateTruckRequest(
        string Name, 
        string Code, 
        string Description);
}