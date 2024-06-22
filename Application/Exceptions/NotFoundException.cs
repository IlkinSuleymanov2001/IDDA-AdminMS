
using Core.Pipelines.Logger;

namespace Core.Exceptions
{
    public  class NotFoundException:Exception,INonLogException
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException() : base()
        {
        }
    }
}
