using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlertView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TextMeshProUGUI confirmButtonText;
    [SerializeField] private TMP_FontAsset koreanFont;
    
    private void Awake()
    {
        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(OnConfirmButtonClick);
        }

        if (koreanFont != null)
        {
            if (messageText != null)
                messageText.font = koreanFont;
            if (confirmButtonText != null)
                confirmButtonText.font = koreanFont;
        }
    }

    public void Show(string message)
    {
        gameObject.SetActive(true);
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnConfirmButtonClick()
    {
        Hide();
    }

    private void OnDestroy()
    {
        if (confirmButton != null)
        {
            confirmButton.onClick.RemoveListener(OnConfirmButtonClick);
        }
    }
} 