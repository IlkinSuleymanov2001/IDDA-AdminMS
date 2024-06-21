using Core.Pipelines.Logger;


namespace Core.Exceptions
{
    public  class RollBackException:Exception,INonLogException
    {
        public RollBackException()
        {     
        }
        public RollBackException(string message):base(message)
        {
        
        }
    }
}
