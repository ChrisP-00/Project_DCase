using UnityEngine;

public class UIScreenOpener : MonoBehaviour
{
    [SerializeField] private ScreenType screenType;

    public void Open()
    {
        UIManager.Instance.OpenScreen(screenType);
    }
}
