using Core.Pipelines.Logger;
namespace Core.Exceptions
{
    public  class UnAuthorizationException: Exception,INonLogException
    {
        public UnAuthorizationException(): base("authorize error")
        {
            
        }

        public UnAuthorizationException(string message):base(message)
        {
            
        }
    }
}
