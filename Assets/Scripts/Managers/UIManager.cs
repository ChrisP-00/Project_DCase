using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] public Animator testAnimator;

    public async void OnClickGameStart()
    {
        bool result = await LoginManager.Instance.Login();

        if (result)
        {
            // scene 이동     
            SceneManager.LoadSceneAsync("MainScene");
        }
        else
        {
            SceneManager.LoadSceneAsync("MainScene");
        }
    }
    
    public void OnClickTestAngry()
    {
        testAnimator.SetTrigger("IsAngry");
    }


    public void OnClickTestHappy()
    {
        testAnimator.SetTrigger("IsHappy");
    }
    
    public void OnClickTestHello()
    {
        testAnimator.SetTrigger("IsHello");
    }
}