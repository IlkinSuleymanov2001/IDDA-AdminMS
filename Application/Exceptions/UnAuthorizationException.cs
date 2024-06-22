

using Core.Pipelines.Logger;

namespace Core.Exceptions
{
    public  class UnAuthorizationException : Exception,INonLogException
    {
        public UnAuthorizationException()
        {
            
        }

        public UnAuthorizationException(string message):base(message)
        {
            
        }
    }
}
