using ColdrunLogistics.Data.Models.Trucks;

namespace ColdrunLogistics.Models.Trucks
{
    public class TruckSortSettings
    {
        public static Dictionary<string, Func<Truck, object>> keySelectorsByColumn = new Dictionary<string, Func<Truck, object>>
        {
            { TruckSortColumnNames.Name, t => t.Name },
            { TruckSortColumnNames.Code, t => t.Code },
            { TruckSortColumnNames.Description, t => t.Description },
            { TruckSortColumnNames.Status, t => t.Status }
        };

        private static class TruckSortColumnNames
        {
            public static string Name = "Name";
            public static string Code = "Code";
            public static string Description = "Description";
            public static string Status = "Status";
        }
    }
}
