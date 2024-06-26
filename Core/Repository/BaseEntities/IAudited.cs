
namespace Core.Repository.BaseEntities
{
    public interface IAudited
    {
    }


    public interface IHasCreationTime : IAudited
    {
        public DateTime CreatedDate { get; set; }
    }

    public interface ICreationAudited : IHasCreationTime
    {
        public int? CreatedUserId { get; set; }
    }


    public interface IHasModificationTime : IAudited
    {
        public DateTime? LastModifiedDate { get; set; }
    }

    public interface IModificationAudited : IHasModificationTime
    {
        public int? LastModifiedUserId { get; set; }
    }


    public interface IHasDeletionTime : IAudited
    {
        DateTime? DeletedDate { get; set; }
    }

    public interface IDeletionAudited : IHasDeletionTime
    {
        int? DeletedUserId { get; set; }
    }
}
