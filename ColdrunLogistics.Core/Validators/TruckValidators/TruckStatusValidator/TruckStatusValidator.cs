using ColdrunLogistics.Api.Models.Enums;

namespace ColdrunLogistics.Models.Trucks.TruckStatus
{
    public class TruckStatusValidator
    {
        private TruckStatusStrategy statusStrategy;

        private Dictionary<TruckStatusType, TruckStatusStrategy> strategiesByStatus = new Dictionary<TruckStatusType, TruckStatusStrategy>
        {
            { TruckStatusType.OutOfService, new OutOfServiceStatusStrategy() },
            { TruckStatusType.Loading, new LoadingStatusStrategy() },
            { TruckStatusType.ToJob, new ToJobStatusStrategy() },
            { TruckStatusType.AtJob, new AtJobStatusStrategy() },
            { TruckStatusType.Returning, new ReturningStatusStrategy() }
        };

        public TruckStatusValidator(TruckStatusType currentStatus)
        {
            statusStrategy = strategiesByStatus.TryGetValue(currentStatus, out var strategy)
                ? strategy
                : new OutOfServiceStatusStrategy();       
        }

        public bool IsStatusAllowedToChange(TruckStatusType status) => statusStrategy.CanChangeTo(status);
    }
}