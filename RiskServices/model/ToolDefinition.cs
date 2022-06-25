using Newtonsoft.Json;

namespace FRMDesktop.model
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class ToolDefinition
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "version")]
        public byte Version { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }
    }
}
