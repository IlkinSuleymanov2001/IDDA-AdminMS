using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Response
{
    public class Response : IResponse
    {
        public string Message { get;  init; } = "Successfully operations";
        public bool  Success =>true;

        public Response(string message)
        {
            Message = message;
        }

        public Response()
        {
        }


        public static IResponse Ok()
        {
            return new Response();
        }
        public static IResponse Ok(string message)
        {
            return new Response(message);
        }
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


        public class LowercaseNamingStrategy : NamingStrategy
        {
            protected override string ResolvePropertyName(string name)
            {
                return name.ToLower();
            }
        }
    }
}
