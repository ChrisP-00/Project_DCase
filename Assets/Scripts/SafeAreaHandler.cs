using System.Runtime.CompilerServices;
using UnityEngine;


[RequireComponent(typeof(RectTransform))]
public class SafeAreaHandler : MonoBehaviour
{
    private RectTransform rectTransform;
    private Rect lastSafeArea;
    private ScreenOrientation lastOrientation = ScreenOrientation.AutoRotation;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;
        
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;
        
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}
