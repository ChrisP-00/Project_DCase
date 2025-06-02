using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class DriveImageLoader : MonoBehaviour
{
    [Header("UI에 표시할 RawImage 컴포넌트")]
    public RawImage targetRawImage;

    [Header("Google Drive 폴더 ID")]
    public string driveFolderId;

    private void Start()
    {
        Debug.Log("▶ Start() 호출됨 - 이미지 로드 시작");
        LoadDriveImage();
    }

    private async void LoadDriveImage()
    {
        Debug.Log($"📂 GoogleDriveHelper에서 폴더 ID: {driveFolderId} 요청");
        string fileId = await GoogleDriveHelper.GetFirstImageFileIdAsync(driveFolderId);

        if (!string.IsNullOrEmpty(fileId))
        {
            Debug.Log($"✅ 첫 번째 이미지 파일 ID 확인됨: {fileId}");
            StartCoroutine(LoadImageFromDrive(fileId));
        }
        else
        {
            Debug.LogError("❌ 유효한 이미지 파일 ID를 찾을 수 없습니다.");
        }
    }

    IEnumerator LoadImageFromDrive(string fileId)
    {
        string imageUrl = $"https://drive.google.com/uc?export=download&id={fileId}";
        Debug.Log("최종 다운로드 URL: " + imageUrl);

        UnityWebRequest request = UnityWebRequest.Get(imageUrl);
        request.redirectLimit = 10;
        request.downloadHandler = new DownloadHandlerTexture();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("❌ 이미지 다운로드 실패: " + request.error);
            yield break;
        }

        Texture2D texture = DownloadHandlerTexture.GetContent(request);
        targetRawImage.texture = texture;

        Debug.Log("✅ 이미지 로드 및 적용 완료");
    }
}