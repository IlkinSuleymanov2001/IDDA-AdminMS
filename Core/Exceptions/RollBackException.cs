using Core.Pipelines.Logger;


namespace Core.Exceptions
{
    public  class RollBackException:Exception,IException
    {
        public RollBackException()
        {     
        }
        public RollBackException(string message):base(message)
        {
        
        }
    }
}
