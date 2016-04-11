using Newtonsoft.Json;

namespace Carpool
{
    class Car
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "id_user")]
        public string ID_User { get; set; }

        [JsonProperty(PropertyName = "model")]
        public string Model { get; set; }

    }
}
