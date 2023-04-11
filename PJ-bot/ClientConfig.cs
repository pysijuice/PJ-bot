using Newtonsoft.Json;

namespace PJ_bot;

public struct ClientConfig {
    [JsonProperty("token")] 
    public string Token;
}
