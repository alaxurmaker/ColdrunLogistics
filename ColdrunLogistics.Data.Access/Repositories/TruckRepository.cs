using ColdrunLogistics.Api.Models.Enums;
using ColdrunLogistics.Data.Context;
using ColdrunLogistics.Data.Models.Trucks;
using ColdrunLogistics.Models;
using ColdrunLogistics.Models.Trucks;

namespace ColdrunLogistics.Data
{
    public class TruckRepository : ITruckRepository
    {
        private LogisticsMockContext _context;

        public TruckRepository()
        {
            _context = new LogisticsMockContext();
        }

        public void CreateTruck(Truck truck)
        {
            _context.Trucks.Add(truck.Id, truck);
        }

        public Truck? GetTruckById(Guid truckId)
        {
            if (_context.Trucks.TryGetValue(truckId, out var truck))
            {
                return truck;
            }

            return default;
        }

        public List<Truck> GetTrucks(string? name = null, string? code = null, TruckStatusType? status = null, SortOptions? sortOptions = null)
        {
            var queryTrucks = _context.Trucks.Values
                .Skip(PaginationOptions.Skip)
                .Take(PaginationOptions.Size);

            FilterTrucks(queryTrucks, name, code, status);
            SortTrucks(queryTrucks, sortOptions);
                   
            return queryTrucks.ToList();
        }

        public void UpdateTruck(Truck truck)
        {
            Truck? existingTruck = GetTruckById(truck.Id);
            if (existingTruck == null) 
            {
                return;
            }

            _context.Trucks[existingTruck.Id] = truck;            
        }

        public void DeleteTruck(Guid truckId)
        {
            _context.Trucks.Remove(truckId);
        }

        private void FilterTrucks(IEnumerable<Truck> trucks, string? name = null, string? code = null, TruckStatusType? status = null)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                trucks = trucks.Where(t => t.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(code))
            {
                trucks = trucks.Where(t => t.Code == code);
            }

            if (status != null && Enum.IsDefined(typeof(TruckStatusType), status))
            {
                trucks = trucks.Where(t => t.Status == status);
            }
        }

        private void SortTrucks(IEnumerable<Truck> trucks, SortOptions sortOptions)
        {
            if (sortOptions == null)
            {
                return;
            }

            string orderBy = "Name";

            if (!string.IsNullOrWhiteSpace(sortOptions.OrderBy))
            {
                if (sortOptions.OrderBy.ToLower().Contains(nameof(Truck.Name)))
                {
                    orderBy = "Name";
                }

                if (sortOptions.OrderBy.ToLower().Contains(nameof(Truck.Code)))
                {
                    orderBy = "Code";
                }

                if (sortOptions.OrderBy.ToLower().Contains(nameof(Truck.Description)))
                {
                    orderBy = "Description";
                }

                if (sortOptions.OrderBy.ToLower().Contains(nameof(Truck.Status)))
                {
                    orderBy = "Status";
                }
            }

            if (sortOptions.IsDescending)
            {
                trucks = trucks.OrderByDescending(TruckSortSettings.keySelectorsByColumn[orderBy]);
            }

            trucks = trucks.OrderBy(TruckSortSettings.keySelectorsByColumn[orderBy]);
        }
    }
}