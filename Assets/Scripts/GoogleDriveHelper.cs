using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;

public static class GoogleDriveHelper
{
    public static async Task<string> GetFirstImageFileIdAsync(string folderId)
    {
        string credentialPath = Path.Combine(Application.streamingAssetsPath, "credentials.json");
        Debug.Log("📁 credentials.json 경로: " + credentialPath);

        if (!File.Exists(credentialPath))
        {
            Debug.LogError("❌ credentials.json 파일을 찾을 수 없습니다.");
            return null;
        }

        var credential = GoogleCredential.FromFile(credentialPath)
            .CreateScoped(DriveService.Scope.DriveReadonly);

        var service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "UnityDriveImage"
        });

        var request = service.Files.List();
        request.Q = $"'{folderId}' in parents and mimeType contains 'image/'";
        request.Fields = "files(id, name)";
        var result = await request.ExecuteAsync();

        Debug.Log($"📥 응답 받은 파일 수: {result.Files?.Count ?? 0}");

        if (result.Files == null || result.Files.Count == 0)
        {
            Debug.LogWarning("📛 해당 폴더에 이미지가 없습니다.");
            return null;
        }

        Debug.Log("✅ 첫 번째 파일 이름: " + result.Files[0].Name);
        return result.Files[0].Id;
    }
}