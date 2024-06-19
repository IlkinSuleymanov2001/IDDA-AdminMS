
namespace Core.BaseEntities
{
    public class BaseEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
}
