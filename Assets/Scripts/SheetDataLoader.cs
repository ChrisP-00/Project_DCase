using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

public class SheetDataLoader : MonoBehaviour
{
    public static SheetDataLoader Instance { get; private set; }

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
        // 현재 어셈블리에서 DataClass 네임스페이스의 클래스들 찾기
        var types = Assembly.GetExecutingAssembly()
                            .GetTypes()
                            .Where(t => t.IsClass && t.Namespace == null && t.IsDefined(typeof(SerializableAttribute), false))
                            .Where(t => t.Name.StartsWith("info") || t.Name == "define") // 네이밍 패턴에 따라 필터
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
            Debug.LogWarning($"[SheetDataLoader] JSON not found: {sheetName}");
            return;
        }

        var listType = typeof(List<>).MakeGenericType(dataType);
        var data = JsonConvert.DeserializeObject(jsonAsset.text, listType);
        loadedData[sheetName] = data;

        // 안전하게 개수 로그 출력
        if (data is System.Collections.IList list)
        {
            Debug.Log($"[DataManager] Loaded: {sheetName} ({list.Count} entries)");
        }
        else
        {
            Debug.LogWarning($"[DataManager] Loaded: {sheetName} (unknown count)");
        }
    }

    public List<T> GetData<T>(string sheetName)
    {
        if (loadedData.TryGetValue(sheetName, out object rawList))
        {
            return rawList as List<T>;
        }

        Debug.LogWarning($"[SheetDataLoader] Sheet not loaded: {sheetName}");
        return new List<T>();
    }
}