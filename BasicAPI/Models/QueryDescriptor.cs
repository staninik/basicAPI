namespace BasicAPI.Models
{
    public class QueryDescriptor
    {
        public SortDescriptor SortDescriptor { get; set; }

        public int Skip { get; set; } = 0;

        public int Take { get; set; } = 50;
    }
}