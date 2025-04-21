using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private Dictionary<string, object> loadedData = new Dictionary<string, object>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadAllJsonDataFromDataClass();
    }

    private void LoadAllJsonDataFromDataClass()
    {
        // Assembly 안에서 Serializable 클래스 중 DataClass 내 추정되는 클래스만 필터링
        var types = Assembly.GetExecutingAssembly()
                            .GetTypes()
                            .Where(t => t.IsClass && t.Namespace == null && t.IsDefined(typeof(SerializableAttribute), false))
                            .Where(t => t.Name.StartsWith("info") || t.Name == "define") // infoXXX 또는 define
                            .ToList();

        foreach (var type in types)
        {
            LoadJsonData(type.Name, type);
        }
    }

    private void LoadJsonData(string sheetName, Type dataType)
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>($"JsonFiles/{sheetName}");
        if (jsonAsset == null)
        {
            Debug.LogWarning($"[DataManager] JSON not found: {sheetName}");
            return;
        }

        var listType = typeof(List<>).MakeGenericType(dataType);
        var data = JsonConvert.DeserializeObject(jsonAsset.text, listType);
        loadedData[sheetName] = data;

        Debug.Log($"[DataManager] Loaded: {sheetName} ({((IList)data).Count} entries)");
    }

    public List<T> GetData<T>(string sheetName)
    {
        if (loadedData.TryGetValue(sheetName, out object rawList))
        {
            return rawList as List<T>;
        }

        Debug.LogWarning($"[DataManager] Sheet not loaded: {sheetName}");
        return new List<T>();
    }
}
