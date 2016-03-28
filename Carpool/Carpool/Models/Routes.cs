
using System;
using Newtonsoft.Json;

namespace Carpool
{
     public class Routes
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

        public override string ToString()
        {
            return string.Format("[Routes: ID={0}, " +
                                 "ID_User={1}, " +
                                 "ID_Car={2}, " +
                                 "From={3}, " +
                                 "From_Longitude={4}," +
                                 "From_Latitude={5}, " +
                                 "To={6}," +
                                 "To_Latitude={7}," +
                                 "To_Longitude={8}," +
                                 "Capacity={9}," +
                                 "Comments={10}," +
                                 "depart_time={11}]", 
                                 ID,ID_User,ID_Car,From,From_Longitude,From_Latitude,To,To_Latitude,To_Longitude,Capacity,Comments,Depart_Time);
        }
    }
}
