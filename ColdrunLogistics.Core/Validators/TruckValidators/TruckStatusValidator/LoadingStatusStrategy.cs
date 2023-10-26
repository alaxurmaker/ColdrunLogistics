using ColdrunLogistics.Api.Models.Enums;

namespace ColdrunLogistics.Models.Trucks.TruckStatus
{
    internal class LoadingStatusStrategy : TruckStatusStrategy
    {
        internal override bool CanChangeTo(TruckStatusType status) => status is TruckStatusType.OutOfService or TruckStatusType.ToJob;
    }
}