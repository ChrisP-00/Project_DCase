using System;
using System.Text;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;


public class NetworkManager : MonoSingleton<NetworkManager>
{
    public async UniTask<Result<string>> PostResultMessageAsync(string endPoint, object requestBody)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return new Result<string>
            {
                ResultCodes = ResultCodes.NetworkError,
                ResultMessage = "Network Reachability Not Reachable"
            };
        }

        byte[] bodyRow = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestBody));

        DebugUtility.Log(DebugColor.Yellow, endPoint, $"Request : {JsonConvert.SerializeObject(requestBody)}");

        using (var request = UnityWebRequest.PostWwwForm(HostHandler.HostCall() + endPoint, "POST"))
        {
            //
            request.timeout = 30;
            request.uploadHandler = new UploadHandlerRaw(bodyRow);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            try
            {
                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.DataProcessingError)
                {
                    return new Result<string>
                    {
                        ResultCodes = ResultCodes.NetworkError,
                        ResultMessage = ResultCodes.NetworkError.ToString()
                    };
                }

                if (request.result == UnityWebRequest.Result.ProtocolError)
                {
                    return new Result<string>
                    {
                        ResultCodes = ResultCodes.NetworkError,
                        ResultMessage = ResultCodes.NetworkError.ToString()
                    };
                }

                if (request.responseCode == 503 || request.responseCode == 504 || request.responseCode == 404)
                {
                    return new Result<string>
                    {
                        ResultCodes = ResultCodes.NetworkError,
                        ResultMessage = ResultCodes.NetworkError.ToString()
                    };
                }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    var jsonResponse = JObject.Parse(request.downloadHandler.text);
                    int resultCode = (int)jsonResponse["resultCode"];
                    string resultMessage = jsonResponse["resultMessage"].ToString();

                    DebugUtility.Log(DebugColor.Yellow, endPoint, $"Response : {resultMessage}");

                    return new Result<string>
                    {
                        ResultCodes = (ResultCodes)resultCode,
                        ResultMessage = resultMessage
                    };
                }

                return new Result<string>
                {
                    ResultCodes = ResultCodes.NetworkError,
                    ResultMessage = ResultCodes.NetworkError.ToString()
                };
            }
            catch (Exception e)
            {
                return new Result<string>
                {
                    ResultCodes = ResultCodes.NetworkError,
                    ResultMessage = e.ToString()
                };
            }
        }
    }

    public async UniTask<Result<T>> PostResultAsync<T>(string endPoint, object requestBody)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return new Result<T>
            {
                ResultCodes = ResultCodes.NetworkError,
                ResultMessage = "Network Reachability Not Reachable",
                Data = default(T)
            };
        }

        byte[] bodyRow = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(requestBody));

        Debug.Log($"{bodyRow}");
        DebugUtility.Log(DebugColor.Yellow, endPoint, $"Request : {JsonConvert.SerializeObject(requestBody)}");

        using (var request = UnityWebRequest.PostWwwForm(HostHandler.HostCall() + endPoint, "POST"))
        {
            //
            request.timeout = 30;
            request.uploadHandler = new UploadHandlerRaw(bodyRow);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            try
            {
                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.DataProcessingError)
                {
                    return new Result<T>
                    {
                        ResultCodes = ResultCodes.NetworkError,
                        ResultMessage = ResultCodes.NetworkError.ToString(),
                        Data = default(T)
                    };
                }

                if (request.result == UnityWebRequest.Result.ProtocolError)
                {
                    return new Result<T>
                    {
                        ResultCodes = ResultCodes.NetworkError,
                        ResultMessage = ResultCodes.NetworkError.ToString(),
                        Data = default(T)
                    };
                }

                if (request.responseCode == 503 || request.responseCode == 504 || request.responseCode == 404)
                {
                    return new Result<T>
                    {
                        ResultCodes = ResultCodes.NetworkError,
                        ResultMessage = ResultCodes.NetworkError.ToString(),
                        Data = default(T)
                    };
                }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    var jsonResponse = JObject.Parse(request.downloadHandler.text);
                    int resultCode = (int)jsonResponse["resultCodes"];
                    string resultMessage = jsonResponse["resultMessage"].ToString();

                    DebugUtility.Log(DebugColor.Yellow, endPoint,
                        $"Response : {resultMessage}, Data : {JsonConvert.SerializeObject(jsonResponse)}");

                    T data = jsonResponse["data"].ToObject<T>();

                    return new Result<T>
                    {
                        ResultCodes = (ResultCodes)resultCode,
                        ResultMessage = resultMessage,
                        Data = data
                    };
                }

                return new Result<T>
                {
                    ResultCodes = ResultCodes.NetworkError,
                    ResultMessage = ResultCodes.NetworkError.ToString(),
                    Data = default(T)
                };
            }
            catch (Exception e)
            {
                return new Result<T>
                {
                    ResultCodes = ResultCodes.NetworkError,
                    ResultMessage = e.ToString(),
                    Data = default(T)
                };
            }
        }
    }
}