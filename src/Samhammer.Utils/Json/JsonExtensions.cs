using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Samhammer.Utils.Json
{
    public static class JsonExtensions
    {
        public static string ToJson(this object contract)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy(),
                },
            };

            return JsonConvert.SerializeObject(contract, jsonSettings);
        }
    }
}
