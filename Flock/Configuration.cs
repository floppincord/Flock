using Newtonsoft.Json;

namespace Flock
{
    [JsonObject]
    public class Configuration
    {
        [JsonProperty("queries")] public string[] Query { get; set; }
    }
}