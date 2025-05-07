using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("UIs")]
    [SerializeField] private GameObject mainScreenUI;
    [SerializeField] private GameObject storageScreenUI;
    [SerializeField] private GameObject missionScreenUI;
    
    private GameObject currentScreenUI;
    
    [SerializeField] private GameObject accountError;

    public void OpenMissionScreen()
    {
        if (currentScreenUI != null)
        {
            currentScreenUI.SetActive(false);
        }
        
        mainScreenUI.SetActive(false);
        missionScreenUI.SetActive(true);
        currentScreenUI = missionScreenUI;
    }

    public void OpenStorageScreen()
    {
        if (currentScreenUI != null)
        {
            currentScreenUI.SetActive(false);
        }

        mainScreenUI.SetActive(false);
        storageScreenUI.SetActive(true);
        currentScreenUI = storageScreenUI;
    }

    public void OnClickBack()
    {
        if (currentScreenUI != null)
        {
            mainScreenUI.SetActive(true);
            currentScreenUI.SetActive(false);
            currentScreenUI = null;
            return;
        }
        
        Console.WriteLine("카페24로 이동");
    }
    
    public void AccountErrorOn()
    {
        accountError.SetActive(true);
    }
}