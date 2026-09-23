using System;

public class JsonSerializationException : Newtonsoft.Json.JsonSerializationException
{
    public JsonSerializationException(string message) : base(message)
    {
    }
}