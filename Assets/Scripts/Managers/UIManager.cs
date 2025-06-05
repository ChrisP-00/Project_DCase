using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public enum ScreenType
{
    Main,
    Mission,
    Storage,
}

[System.Serializable]
public class ScreenUISet
{
    public ScreenType screenType;
    public GameObject screenUI;
    public GameObject topBarUI;
}

public class UIManager : MonoSingleton<UIManager>
{
    [DllImport("__Internal")]
    private static extern void GoToMainPage();
    
    [Header("UIs")]
    [SerializeField] private GameObject mainScreenUI;
    [SerializeField] private ScreenType defaultScreen = ScreenType.Main;
    
    [Header("Screen Sets")]
    [SerializeField] private List<ScreenUISet> screenUISets;
    
    private Dictionary<ScreenType, ScreenUISet> configMap;
    private GameObject currentScreenUI;
    
    private void Awake()
    {
        configMap = new Dictionary<ScreenType, ScreenUISet>();

        foreach (var set in screenUISets)
        {
            if (set != null)
                configMap[set.screenType] = set;
        }
    }

    private void Start()
    {
        OpenScreen(defaultScreen);
    }

    public void OpenScreen(ScreenType type)
    {
        if (!configMap.TryGetValue(type, out var config))
        {
            Debug.LogWarning($"Screen config for {type} not found.");
            return;
        }

        if (currentScreenUI != null)
            currentScreenUI.SetActive(false);
        
        foreach (var cfg in configMap.Values)
            cfg.topBarUI?.SetActive(false);

        config.screenUI.SetActive(true);
        config.topBarUI?.SetActive(true);

        currentScreenUI = config.screenUI;
    }

    public void OnClickBack()
    {
        if (currentScreenUI != null)
        {
            // 메인 화면으로 돌아갈 때 TopBar도 함께 처리
            foreach (var cfg in configMap.Values)
            cfg.topBarUI?.SetActive(false);

            // Main 화면의 TopBar 활성화
            if (configMap.TryGetValue(ScreenType.Main, out var mainConfig))
                mainConfig.topBarUI?.SetActive(true);

            mainScreenUI.SetActive(true);
            currentScreenUI.SetActive(false);
            currentScreenUI = null;
        }
        else
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
                GoToMainPage(); // <- 여기를 실행하면 JS 함수 호출됨
            #endif
        }
    }
}