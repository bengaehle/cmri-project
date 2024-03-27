using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CMRI_CandidateProject.Models;

public class CandidateInterview
{
    public required string Name { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly DateOfInterview { get; set; }
}

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // TODO - probably a better way to do this conversion, but this at least simplifies logic to be DateOnly
        var dateTimeVar = DateTime.Parse(reader.GetString() ?? "", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
        return DateOnly.FromDateTime(dateTimeVar);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}
