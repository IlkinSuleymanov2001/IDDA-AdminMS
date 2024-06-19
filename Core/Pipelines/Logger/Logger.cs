using Microsoft.Extensions.Configuration;
using NpgsqlTypes;
using Serilog.Sinks.PostgreSQL;
using Serilog;

namespace Core.Pipelines.Logger
{
    public  class Logger
    {
        private readonly IConfiguration _configuration;
        public Logger(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public  void LogErrorToFile(Exception exception)
        {


            string logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + "/errorlog/", ".txt");
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

        public  void LogErrorToPostgreDatabase(Exception exception)
        {

            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
{
    {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
    {"raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
    {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text) }
};
            Log.Logger = new LoggerConfiguration()
            .WriteTo.PostgreSQL
                                (_configuration.GetConnectionString("ADMINMS"), "ErrorLog", columnWriters,
                                needAutoCreateTable: true,
                                respectCase: true,
                                useCopy: false)
                                .CreateLogger();

            Log.Error(exception, exception.Message);
            Log.CloseAndFlush();
        }
    }
}
