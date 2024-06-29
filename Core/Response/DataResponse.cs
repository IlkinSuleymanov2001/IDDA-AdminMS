
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Response
{
    public class DataResponse :Response, IDataResponse
    {
        public object Data { get ;init  ; }

        public DataResponse(object data)
        {
            Data = data;
        }

        public DataResponse(object data, string message) : base(message)
        {
            Data = data;
        }

        public DataResponse()
        {

        }

        public static IDataResponse Ok(object data, string message)
        {
            return new DataResponse(data, message);
        }
        public static IDataResponse Ok(object data)
        {
            return new DataResponse(data);
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
    }
}
