using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace CommonLibraryCoreMaui.Models
{
    public class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public Role role { get; set; }
        public int userid { get; set; }
        public string loginid { get; set; }
        public string issued { get; set; }
        public string expires { get; set; }
        public string error { get; set; }


        //[JsonPropertyName("access_token")]
        //public string AccessToken { get; set; }

        //[JsonPropertyName("token_type")]
        //public string TokenType { get; set; }

        //[JsonPropertyName("expires_in")]
        //public string ExpiresIn { get; set; }

        //[JsonPropertyName("refresh_token")]
        //public string RefreshToken { get; set; }

        //// [JsonPropertyName("role")]           
        ////public string Role { get; set; }

        //[JsonPropertyName("userid")]
        //public int UserId { get; set; }

        //[JsonPropertyName("loginid")]
        //public int LoginId { get; set; }

        //[JsonPropertyName(".issued")]
        //public string Issued { get; set; }

        //[JsonPropertyName(".expires")]
        //public string Expires { get; set; }

        //public string error { get; set; }
    }

    //public class TypeEnumConverter : StringEnumConverter
    //{
    //    public Type DefaultValue { get; set; }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        try
    //        {
    //            return base.ReadJson(reader, objectType, existingValue, serializer);
    //        }
    //        catch (JsonSerializationException)
    //        {
    //            return DefaultValue;
    //        }
    //    }
    //}
}
