using Newtonsoft.Json;
using static UserDto;

public class ProfileApiDto
{
    public class SetupProflieRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }
    }

}