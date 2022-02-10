using System;
using System.Collections.Generic;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
[Serializable]
public class Score
{
    public string stringValue { get; set; }
    public string integerValue { get; set; }
}

[Serializable]
public class Name
{
    public string stringValue { get; set; }
}

[Serializable]
public class Fields
{
    public Score score { get; set; }
    public Name name { get; set; }
}

[Serializable]
public class Document
{
    public string name { get; set; }
    public Fields fields { get; set; }
    public DateTime createTime { get; set; }
    public DateTime updateTime { get; set; }
}

[Serializable]
public class Root
{
    public Document document { get; set; }
    public DateTime readTime { get; set; }
}

