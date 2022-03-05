using System.Text.Json.Serialization;

namespace PreferenceCenterAPI.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumConsent
    {
        email_notifications = 1,
        sms_notifications = 2
    }
}