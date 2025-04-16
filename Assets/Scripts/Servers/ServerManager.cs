using System.Net;
using Cysharp.Threading.Tasks;


public class ServerManager : MonoSingleton<ServerManager>
{
    public async UniTask<Result<string>> CreateAccountAsync(Request.Req_CreateAccount requestBody)
    {
        Result<string> result = await NetworkManager.Instance.PostResultMessageAsync(EndPoint.CreateAccount , requestBody);
        return result;
    }
    
    public async UniTask<Result<Response.Res_Login>> LoginAsync(Request.Req_Login requestBody)
    {
        Result<Response.Res_Login> result = await NetworkManager.Instance.PostResultAsync<Response.Res_Login>(EndPoint.Login , requestBody);
        return result;
    }
    
    public async UniTask<Result<string>> EquipCharacterAsync(Request.Req_EquipCharacter requestBody)
    {
        Result<string> result = await NetworkManager.Instance.PostResultMessageAsync(EndPoint.EquipCharacter , requestBody);
        return result;
    }
    
    public async UniTask<Result<string>> EquipItemAsync(Request.Req_EquipItem requestBody)
    {
        Result<string> result = await NetworkManager.Instance.PostResultMessageAsync(EndPoint.EquipItem , requestBody);
        return result;
    }
    
    public async UniTask<Result<string>> UnequipItemByTypeAsnyc(Request.Req_EquipItem requestBody)
    {
        Result<string> result = await NetworkManager.Instance.PostResultMessageAsync(EndPoint.UnequipItem , requestBody);
        return result;
    }
    
    public async UniTask<Result<string>> PlayStatusAsync(Request.Req_PlayStatus requestBody)
    {
        Result<string> result = await NetworkManager.Instance.PostResultMessageAsync(EndPoint.PlayStatus , requestBody);
        return result;
    }
    
    public async UniTask<Result<string>> ReceiveMissionAsync(Request.Req_ReceiveMission requestBody)
    {
        Result<string> result = await NetworkManager.Instance.PostResultMessageAsync(EndPoint.ReceiveMission , requestBody);
        return result;
    }
}