using ColdrunLogistics.Api.Models.Enums;

namespace ColdrunLogistics.Models.Trucks.TruckStatus
{
    internal class OutOfServiceStatusStrategy : TruckStatusStrategy
    {
        internal override bool CanChangeTo(TruckStatusType status) => true;
    }
}