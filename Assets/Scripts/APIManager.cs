using UnityEngine;

public class APIManager : MonoSingleton<APIManager>
{
    public AccountAPI Account { get; private set; }
    

    protected override void Awake()
    {
        base.Awake();
        
        Account = new AccountAPI();
    }
}