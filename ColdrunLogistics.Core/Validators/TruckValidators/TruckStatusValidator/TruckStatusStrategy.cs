using ColdrunLogistics.Api.Models.Enums;

namespace ColdrunLogistics.Models.Trucks
{
    internal abstract class TruckStatusStrategy
    {
        internal abstract bool CanChangeTo(TruckStatusType status);
    }
}