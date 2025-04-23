using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class userData
{
    public int User_Index;
    public string Nickname;
    public bool Is_Banned;
    public bool Is_Deleted;
    public int TogetherDay;
    public int FoodCount;
    public int PlayCount;
    public int Exp;
    public DateTime Last_Login_At;
    public List<Response.User_Character> UserCharacters;
    public List<Response.User_Inventory> UserInventories;
    public List<Response.User_Equip> UserEquips;
    public List<Response.User_Goods> UserGoods;
    public List<Response.User_Daily_Missions> UserDailyMission;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private Dictionary<string, object> loadedData = new Dictionary<string, object>();

    public userData CurrentUser { get; private set; }

    [SerializeField] userData debugUserData;

    private async void Awake()
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

    public void SetUserData(userData data)
    {
        if (data == null)
        {
            Debug.Log("[DataManager] 서버에서 유저 데이터를 받지 못했습니다. 디버그 데이터로 대체합니다.");
            if (debugUserData.TogetherDay == 0)
                debugUserData.TogetherDay = 1;

            CurrentUser = debugUserData;
            if(debugUserData.FoodCount == 0)
            {
                if (define.tableDic.TryGetValue(1, out var foodDefine))
                {
                    CurrentUser.FoodCount = foodDefine.value;
                }
            }

            if(debugUserData.PlayCount == 0)
            {
                if (define.tableDic.TryGetValue(1, out var playDefine))
                {
                    CurrentUser.PlayCount = playDefine.value;
                }
            }
        }
        else
        {
            CurrentUser = data;
        }
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

        // define 타입이면 별도로 Dictionary 초기화
        if (dataType == typeof(define))
        {
            define.tableDic.Clear(); // 기존 데이터 제거

            foreach (var item in (IList)data)
            {
                var d = item as define;
                if (d != null)
                {
                    define.tableDic[d.define_index] = d;
                }
            }

            Debug.Log($"[DataManager] define.tableDic initialized with {define.tableDic.Count} entries");
        }
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