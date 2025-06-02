using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
using static Response;


public class LoginManager : MonoSingleton<LoginManager>
{
    [Header("Login info for test")]
    [SerializeField] public bool hostDebug = true;
    [SerializeField] public bool isServerON;
    [SerializeField] public string memberId;
    [SerializeField] public string nickName;


    private async void Start()
    {
        Debug.Log("LoginManager start ");
        try
        {
            await HostHandler.CheckHostConnection();
        }
        catch (Exception e)
        {
            Debug.LogError($"❌ Host check failed: {e.Message}");
        }
    }

    public async void OnClickGameStart()
    {
        try
        {
            bool isSuccess = await Login();

            if (isSuccess)
            {
                // WebGL에서는 반드시 화면 전환!
                SceneManager.LoadScene("MainScene");
            }
            else
            {
                // WebGL에서는 반드시 화면 전환!
                Console.WriteLine($"[Login] Login failed");
                SceneManager.LoadScene("MainScene");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Network] Connection failed : {e.Message}");
            SceneManager.LoadScene("MainScene");
        }
    }

    private async UniTask<bool> Login()
    {
        bool isLoginSuccess = false;
        
        Request.Req_Login login = new Request.Req_Login();
        
        login.MemberId = memberId;
        login.UnityDeviceNumber = SystemInfo.deviceUniqueIdentifier;
        login.Nickname = nickName;
        
        var result = await ServerManager.Instance.LoginAsync(login);

        if (result.ResultCodes != ResultCodes.Ok)
        {
            DebugUtility.Log($"{result.ResultCodes} : Result / {result.ResultMessage}");
            
        }
        else
        {
            userData serverUser = new userData
            {
                Token = result.Data.Token,
                User_Index = result.Data.UserAccount.User_Index,
                Nickname = result.Data.UserAccount.Nickname,
                Is_Banned = result.Data.UserAccount.Is_Banned,
                Is_Deleted = result.Data.UserAccount.Is_Deleted,
                Last_Login_At = result.Data.UserAccount.Last_Login_At,
                UserCharacters = result.Data.UserCharacters,
                UserInventories = result.Data.UserInventories,
                UserEquips = result.Data.UserEquips,
                UserGoods = result.Data.UserGoods,
                UserDailyMission = result.Data.UserDailyMission
            };

            if (serverUser.Is_Banned || serverUser.Is_Deleted)
            {
                Debug.Log("밴 유저 또는 삭제 유저");
                UIManager.Instance.AccountErrorOn();
            }

            DataManager.Instance.SetUserData(serverUser);
            isLoginSuccess = true;
        }
        
        if(isServerON == false)
        {
            userData serverUser = null;
            DataManager.Instance.SetUserData(serverUser);
            if(DataManager.Instance.CurrentUser.Is_Banned == true || DataManager.Instance.CurrentUser.Is_Deleted)
            {
                UIManager.Instance.AccountErrorOn();
                
            }
            else
            {
                isLoginSuccess = true;
            }
        }
        return isLoginSuccess;
    }
    
    private async UniTask<bool> Login1()
    {
        bool isLoginSuccess = false;
        
        Request.Req_Login login = new Request.Req_Login();
        
        login.MemberId = memberId;
        login.UnityDeviceNumber = SystemInfo.deviceUniqueIdentifier;
        
        var result = await ServerManager.Instance.LoginAsync(login);

        DebugUtility.Log($"{result.ResultCodes} : Result");
        
        if (result.ResultCodes != ResultCodes.Ok)
        {
            Request.Req_CreateAccount createAccount = new Request.Req_CreateAccount();
            createAccount.MemberId = memberId;
            createAccount.UnityDeviceNumber = SystemInfo.deviceUniqueIdentifier;
            createAccount.Nickname = nickName;
            
            var createResult = await ServerManager.Instance.CreateAccountAsync(createAccount);

            DebugUtility.Log($"{createResult.ResultCodes} : Result");

            
            if (createResult.ResultCodes == ResultCodes.Ok)
            {
                DebugUtility.Log("Create Account Success");
                
                result = await ServerManager.Instance.LoginAsync(login);
                if (result.ResultCodes == ResultCodes.Ok)
                {
                      DebugUtility.Log("2 Login success");
                      isLoginSuccess = true;
                }
                else
                {
                    DebugUtility.Log("Login Failed");
                }
            }
            else
            {
                DebugUtility.Log("Create Account Failed");  
            }
        }
        else
        {
            DebugUtility.Log("1 Login success");
            userData serverUser = new userData
            {
                User_Index = result.Data.UserAccount.User_Index,
                Nickname = result.Data.UserAccount.Nickname,
                Is_Banned = result.Data.UserAccount.Is_Banned,
                Is_Deleted = result.Data.UserAccount.Is_Deleted,
                Last_Login_At = result.Data.UserAccount.Last_Login_At,
                UserCharacters = result.Data.UserCharacters,
                UserInventories = result.Data.UserInventories,
                UserEquips = result.Data.UserEquips,
                UserGoods = result.Data.UserGoods,
                UserDailyMission = result.Data.UserDailyMission
            };

            if (serverUser.Is_Banned || serverUser.Is_Deleted)
            {
                Debug.Log("밴 유저 또는 삭제 유저");
                UIManager.Instance.AccountErrorOn();
            }

            DataManager.Instance.SetUserData(serverUser);
            isLoginSuccess = true;
        }

        if(isServerON == false)
        {
            userData serverUser = null;
            DataManager.Instance.SetUserData(serverUser);
            if(DataManager.Instance.CurrentUser.Is_Banned == true || DataManager.Instance.CurrentUser.Is_Deleted)
            {
                UIManager.Instance.AccountErrorOn();
                
            }
            else
            {
                isLoginSuccess = true;
            }
        }

        return isLoginSuccess;
    }
}