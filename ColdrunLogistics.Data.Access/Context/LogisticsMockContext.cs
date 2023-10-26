using ColdrunLogistics.Api.Models.Enums;
using ColdrunLogistics.Data.Models.Trucks;

namespace ColdrunLogistics.Data.Context
{
    internal class LogisticsMockContext
    {
        public Dictionary<Guid, Truck> Trucks { get; set; }

        public LogisticsMockContext()
        {
            Trucks = _trucks;
        }

        private Dictionary<Guid, Truck> _trucks = new Dictionary<Guid, Truck>
        {
            { new Guid("12345678-1234-1234-1234-1234567890AB"), Truck.CreateTruck("Ciężarówka 1", "ABC123", "Opis 1", new Guid("12345678-1234-1234-1234-1234567890AB"), TruckStatusType.OutOfService) },
            { new Guid("12345678-1234-1234-1234-1234567890AB"), Truck.CreateTruck("Ciężarówka 2", "CBAC321", "Opis 2", new Guid("46445678-1234-1234-1234-1234567890AB"), TruckStatusType.Loading) },
            { new Guid("12345678-1234-1234-1234-1234567890AB"), Truck.CreateTruck("Ciężarówka 3", "DEF456", "Opis 3", new Guid("89045678-1234-1234-1234-1234567890AB"), TruckStatusType.ToJob) },
            { new Guid("12345678-1234-1234-1234-1234567890AB"), Truck.CreateTruck("Ciężarówka 4", "FED654", "Opis 4", new Guid("16145678-1234-1234-1234-1234567890AB"), TruckStatusType.AtJob) },
            { new Guid("12345678-1234-1234-1234-1234567890AB"), Truck.CreateTruck("Ciężarówka 5", "GHI789", "Opis 5", new Guid("55545678-1234-1234-1234-1234567890AB"), TruckStatusType.Returning) }
        };
    }
}