using Newtonsoft.Json;

namespace Carpool
{
    class Reservations
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }
        
        [JsonProperty(PropertyName = "id_user")]
        public string ID_User { get; set; }

        [JsonProperty(PropertyName = "id_route")]
        public string ID_Route { get; set; }
    }
}
