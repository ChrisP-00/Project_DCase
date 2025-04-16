using System;
using System.Net.Http;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;


    public class HostHandler
    {
        public static bool HostDebug = true;
        
        public static string localHost = "https://localhost:7113/";
        public static string host = "https://chrispserver.azurewebsites.net/";

        public static HttpClient client = new HttpClient();
        
        // 실행 환경에 따라 host 반환 
        public static string HostCall()
        {
            if (HostDebug)
            {
                return localHost;
            }
            else
            {
                return host;
            }
        }

        public static async UniTask CheckHostConnection()
        {
            string url = HostCall();
            
            Debug.Log(url);
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                     DebugUtility.LogError("ConnectionFailed", $"Url : {url} => {response.StatusCode}");   
                }
            }
            catch (Exception e)
            {
                DebugUtility.LogError("ConnectionException" ,$"Url : {url} => {e.ToString()}");
            }
        }

    }
