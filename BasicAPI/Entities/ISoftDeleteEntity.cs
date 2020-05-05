namespace BasicAPI.Entities
{
    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; set; }
    }
}