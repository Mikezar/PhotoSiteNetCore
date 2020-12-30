namespace PhotoSite.Data.Base
{
    public interface IEntityBase<TKey>
    {
        TKey Id { get; set; }
    }
}