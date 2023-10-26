using ColdrunLogistics.Api.Models.Enums;
using ColdrunLogistics.Api.Models.Trucks;
using ColdrunLogistics.Models.Extensions.StringExtensions;
using ColdrunLogistics.Models.Trucks;
using ColdrunLogistics.Models.Utilities.Errors;
using ErrorOr;

namespace ColdrunLogistics.Data.Models.Trucks
{
    public class Truck
    {
        public Guid Id { get; }

        public string Name { get; }

        public string Code { get; }

        public string Description { get; }

        public TruckStatusType Status { get; }

        public DateTime LastModifiedTime { get; }

        private Truck(Guid id, string name, string code, string description, TruckStatusType status, DateTime lastModifiedDate)
        {
            Id = id;
            Name = name;
            Code = code;
            Description = description;
            Status = status;
            LastModifiedTime = lastModifiedDate;
        }

        public static ErrorOr<Truck> Create(string name, string code, string description, Guid? id = null, TruckStatusType? status = null)
        {
            List<Error> errors = new();

            if (string.IsNullOrWhiteSpace(name) || name.Length is < TruckSettings.MinNameLength or > TruckSettings.MaxNameLength)
            {
                errors.Add(TruckErrors.InvalidName);
            }

            if (!code.IsAlphanumeric())
            {
                errors.Add(TruckErrors.InvalidCode);
            }

            if (description.Length > TruckSettings.MaxDescriptionLength)
            {
                errors.Add(TruckErrors.InvalidDescription);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return CreateTruck(name, code, description, id ?? Guid.NewGuid(), status ?? TruckStatusType.OutOfService);
        }

        public static Truck CreateTruck(string name, string code, string description, Guid? id = null, TruckStatusType? status = null)
        {
            return new Truck(id ?? Guid.NewGuid(), name, code, description, status ?? TruckStatusType.OutOfService, lastModifiedDate: DateTime.UtcNow);
        }

        public static ErrorOr<Truck> From(CreateTruckRequest request)
        {
            return Create(
                request.Name,
                request.Code,
                request.Description);
        }

        public static ErrorOr<Truck> From(Guid id, UpdateTruckRequest request)
        {
            return Create(               
                request.Name,
                request.Code,
                request.Description,
                id,
                request.Status);
        }
    }
}