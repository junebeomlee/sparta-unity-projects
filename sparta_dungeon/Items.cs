using System.Text.Json.Serialization;

namespace sparta_dungeon;
public struct Item
{
    // 대문자일 경우 인식이 안됨
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; }

    [JsonPropertyName("price")]
    public int Price { get; set; }
}
