namespace Zephyr.Data.Models
{
    public class SortOptions
    {
        public string SortProperty { get; set; }
        public SortDirection SortDirection { get; set; }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}