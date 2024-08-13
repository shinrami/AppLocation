using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

public class PointConverter : JsonConverter<Point>
{
    private readonly WKTReader _reader = new WKTReader();

    public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var wkt = reader.GetString();
        if (string.IsNullOrEmpty(wkt))
        {
            return null;
        }

        try
        {
            return (Point)_reader.Read(wkt);
        }
        catch (Exception ex)
        {
            throw new JsonException("Failed to deserialize Point.", ex);
        }
    }

    public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStringValue(value.AsText());
    }
}
