using Newtonsoft.Json;

namespace auth0documentdb.Models.Db
{
    public class NotificationPreferences : Entity
    {
        public NotificationPreferences():base("notificationpreferences")
        {
            Email = false;
            Push = false;
            SMS = false;
        }
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("email")]
        public bool Email { get; set; }
        [JsonProperty("push")]
        public bool Push { get; set; }
        [JsonProperty("sms")]
        public bool SMS { get; set; }
    }
}
