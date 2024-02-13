using CommonLibraryCoreMaui.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.Helper
{
    //internal sealed class CustomEnumConverter : JsonConverter
    //{
    //    public override Role ReadJson(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        switch (reader.TokenType)
    //        {
    //            case JsonTokenType.String:
    //                var isNullable = IsNullableType(typeToConvert);
    //                var enumType = isNullable ? Nullable.GetUnderlyingType(typeToConvert) : typeToConvert;
    //                var names = Enum.GetNames(enumType ?? throw new InvalidOperationException());
    //                if (reader.TokenType != JsonTokenType.String) return Role.Unknown;
    //                var enumText = System.Text.Encoding.UTF8.GetString(reader.ValueSpan);
    //                if (string.IsNullOrEmpty(enumText)) return Role.Unknown;
    //                var match = names.FirstOrDefault(e => string.Equals(e, enumText, StringComparison.OrdinalIgnoreCase));
    //                return (Role)(match != null ? Enum.Parse(enumType, match) : Role.Unknown);
    //            default:
    //                throw new ArgumentOutOfRangeException();
    //        }
    //    }
    //    public override void Write(Utf8JsonWriter writer, Role value, JsonSerializerOptions options)
    //    {
    //        writer.WriteStringValue(value.ToString());
    //    }
    //    private static bool IsNullableType(Type t)
    //    {
    //        return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
    //    }

    //    public override void WriteJson(JsonWriter writer, object? value, Newtonsoft.Json.JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override bool CanConvert(Type objectType)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
