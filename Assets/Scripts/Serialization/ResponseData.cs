using System.Collections.Generic;

[System.Serializable]
public class ResponseData
{
    public ClientItem[] clients;
    public Dictionary<int, DataItem> data;
    public string label;
}