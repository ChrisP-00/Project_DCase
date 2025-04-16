using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


public class APIHelper
{
    public static IEnumerator Post(string url, string json, Action<string> callback)
    {
        using UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            callback?.Invoke(request.downloadHandler.text);
        else
            callback?.Invoke($"ERROR: {request.error}");
    }
}
