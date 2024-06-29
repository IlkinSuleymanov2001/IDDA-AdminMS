using Core.Pipelines.Logger;

namespace Core.Exceptions
{
    public class BadRequestException:Exception,INonLogException
    {
        public BadRequestException(string messsage):base(messsage)
        {
            
        }

    }
}
