using Core.Pipelines.Logger;


namespace Core.Exceptions
{
    public  class ForbiddenException:Exception,INonLogException
    {
        public ForbiddenException(string message):base(message) 
        {
            
        }

        public ForbiddenException()
        {
            
        }
    }
}
