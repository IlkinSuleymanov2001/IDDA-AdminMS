using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using static Core.Response.Response;

namespace Core.Response.Error
{
    public class ErrorResponse : IResponse
    {
        public string Message { get; init; } = "authorize error";

        public bool Success => false;



        public override string ToString()
        {

            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new LowercaseNamingStrategy()
                },
                Formatting = Formatting.Indented // Optional: makes the JSON output readable
            });

        }
    }
}
