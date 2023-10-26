namespace ColdrunLogistics.Models
{
    public class SortOptions
    {
        public SortOptions() { }

        public SortOptions(string orderBy, bool isDescending) 
        {
            OrderBy = orderBy;
            IsDescending = isDescending;
        }

        public string? OrderBy { get; set; }

        public bool IsDescending { get; set; }
    }
}
