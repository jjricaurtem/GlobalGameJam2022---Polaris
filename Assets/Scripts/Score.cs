using System;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
[Serializable]
public class Value
{
    public string stringValue;
    public int integerValue;
    public string valueType;
}

[Serializable]
public class Score
{
    public Value score;
    public Value name;
}

[Serializable]
public class Root
{
    public Score[] answer;
}