
using System;
using Newtonsoft.Json;

namespace Carpool
{
    class Routes
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "id_user")]
        public string ID_User { get; set; }

        [JsonProperty(PropertyName = "id_car")]
        public string ID_Car { get; set; }

        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }

        [JsonProperty(PropertyName = "from_longitude")]
        public string From_Longitude { get; set; }

        [JsonProperty(PropertyName = "from_latitude")]
        public string From_Latitude { get; set; }

        [JsonProperty(PropertyName = "to")]
        public string To { get; set; }

        [JsonProperty(PropertyName = "to_latitude")]
        public string To_Latitude { get; set; }

        [JsonProperty(PropertyName = "to_longitude")]
        public string To_Longitude { get; set; }

        [JsonProperty(PropertyName = "capacity")]
        public int Capacity { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; }

        [JsonProperty(PropertyName = "depart_time")]
        public string Depart_Time { get; set; }


    }
}
