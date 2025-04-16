using System.Collections;
using UnityEngine;


public class AccountAPI
{
    private readonly string baseUrl = "https://localhost:7113/account";

    [System.Serializable]
    public class Req_CreateAccount
    {
        public string MemberId;

        // token
        public string UnityDeviceNumber;
        public string Nickname;
    }

    [System.Serializable]
    public class Req_Login
    {
        public int MemberId;

        // token
        public string UnityDeviceNumber;
    }


    public IEnumerator CreateAccount(string memberId, string unityDeviceNumber, string nickname, System.Action<string> onResult)
    {
        var data = new Req_CreateAccount
        {
            MemberId = memberId,
            UnityDeviceNumber = unityDeviceNumber,
            Nickname = nickname
        };

        string json = JsonUtility.ToJson(data);
        yield return APIHelper.Post($"{baseUrl}/Create-Account", json, onResult);
    }
    
    public IEnumerator Login(int memberId, string unityDeviceNumber, System.Action<string> onResult)
    {
        var data = new Req_Login
        {
            MemberId = memberId,
            UnityDeviceNumber = unityDeviceNumber
        };

        string json = JsonUtility.ToJson(data);
        yield return APIHelper.Post($"{baseUrl}/Login", json, onResult);
    }
}
