using ColdrunLogistics.Models.Trucks;
using ErrorOr;

namespace ColdrunLogistics.Models.Utilities.Errors
{
    public static class TruckErrors
    {
        public static Error NotFound => Error.NotFound(code: "Truck.NotFound", description: "Truck not found.");

        public static Error InvalidStatus => Error.Validation(code: "Truck.InvalidStatus", description: $"Requested invalid truck status.");

        public static Error InvalidCode => Error.Validation(code: "Truck.InvalidCode", description: $"Truck code must be alphanumeric.");

        public static Error InvalidName => Error.Validation(
            code: "Truck.InvalidName", 
            description: $"Truck name must be filled and between {TruckSettings.MinNameLength} and {TruckSettings.MaxNameLength} characters long.");

        public static Error InvalidDescription => Error.Validation(
            code: "Truck.InvalidDescription",
            description: $"Truck description must be at most {TruckSettings.MaxNameLength} characters long.");
    }
}
