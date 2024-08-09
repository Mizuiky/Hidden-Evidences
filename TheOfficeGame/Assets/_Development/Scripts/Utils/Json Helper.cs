using Newtonsoft.Json;

public static class JsonHelper
{
    public static string Serialize<T>(T obj)
    {
        string json = JsonConvert.SerializeObject(obj);
        return json;
    }

    public static T Deserialize<T>(string json)
    {
        var obj = JsonConvert.DeserializeObject<T>(json);
        return obj;
    }
}
