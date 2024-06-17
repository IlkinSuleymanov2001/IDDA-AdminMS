using MediatR;
using Serilog.Sinks.File;
using Serilog;

namespace Application.Common.Pipelines.Logger;



public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IBaseRequest
{



    public async  Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        try
        {
            return await next(); 
        }
        catch (Exception ex)
        {
            if(ex is not IException)
                LogErrorToFile(ex);
            throw;
        }
        //return await next();

    }
   private  void  LogErrorToFile(Exception exception)
    {
     
        
        string logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + "/errorlog/" , ".txt");
        Log.Logger = new LoggerConfiguration()
                 .WriteTo.File(
                     logFilePath,
                     rollingInterval: RollingInterval.Day,
                     retainedFileCountLimit: null,
                     fileSizeLimitBytes: 5000000,
                     outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                 .CreateLogger();
        Log.Error(exception, exception.Message);
        Log.CloseAndFlush();
       
    
    }


    }





