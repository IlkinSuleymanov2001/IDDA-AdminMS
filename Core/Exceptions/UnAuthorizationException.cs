

using Core.Pipelines.Logger;

namespace Core.Exceptions
{
    public  class UnAuthorizationException : Exception,IException
    {
        public UnAuthorizationException()
        {
            
        }

        public UnAuthorizationException(string message):base(message)
        {
            
        }
    }
}
