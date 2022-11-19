using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationController : MonoBehaviour
{

    public static Dictionary<string, string> _Data;

    private void Awake()
    {
        _Data = new Dictionary<string, string>();
        var text = Resources.Load<TextAsset>("Localization/ENG").text;
        var lines = text.Split("\n");
        foreach(var line in lines)
        {
            if (line.Contains(";"))
            {
                var subData = line.Split(";");
                _Data.Add(subData[0], subData[1]);
            }
        }
    }


    public static string GetValue(string key)
    {
        if (_Data.TryGetValue(key, out var value))
        {
            return value;
        }
        return "{" + key + "}";
    }
}
