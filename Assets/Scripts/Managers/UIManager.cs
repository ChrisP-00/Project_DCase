using System;
using System.Collections.Generic;
using System.Linq;
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
            mainScreenUI.SetActive(true);
            currentScreenUI.SetActive(false);
            currentScreenUI = null;
        }
        else
        {
            #if UNITY_WEBGL
                GoToCafe24Homepage();
            #endif
        }
    }



    private void GoToCafe24Homepage()
    {
        Application.ExternalEval("window.goToCafe24Homepage()");
    }
}