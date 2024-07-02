using Core.BaseEntities;
using Core.Pipelines.Logger;

namespace Core.Exceptions
{
    public  class DublicatedEntityException:Exception,INonLogException
    {
        public DublicatedEntityException(string message) : base(message)
        {

        }
        public DublicatedEntityException(Type entity) : base($"{entity.Name} cannot be   dublicated")
        {

        }

        public DublicatedEntityException() : base("Entity cannot be  dublicated")
        {
        }
    }
}
