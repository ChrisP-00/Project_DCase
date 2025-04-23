using UnityEngine;
using TMPro;


public class UIDataManager : MonoBehaviour
{
    public static UIDataManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI FoodCount;
    [SerializeField] TextMeshProUGUI PlayCount;
    [SerializeField] TextMeshProUGUI Exp;
    [SerializeField] TextMeshProUGUI TogetherDay;
    [SerializeField] TextMeshProUGUI NickName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SetUIData();
    }

    public void SetUIData()
    {
        var user = DataManager.Instance.CurrentUser;

        NickName.text = user.Nickname;
        FoodCount.text = $"보유 갯수 : {user.FoodCount}";
        PlayCount.text = $"보유 갯수 : {user.PlayCount}";
        Exp.text = $"{user.Exp} %";
        TogetherDay.text = $"함께한 날짜 : {user.TogetherDay}";
    }

    public void DownValue(int statsNum)
    {
        var user = DataManager.Instance.CurrentUser;

        switch (statsNum)
        {
            case 1:
                user.FoodCount = Mathf.Max(0, user.FoodCount - 1);
                FoodCount.text = $"보유 갯수 : {user.FoodCount}";
                break;
            case 2:
                user.PlayCount = Mathf.Max(0, user.PlayCount - 1);
                PlayCount.text = $"보유 갯수 : {user.PlayCount}";
                break;
            case 3:
                user.Exp = Mathf.Max(0, user.Exp - 1);
                Exp.text = $"{user.Exp} %";
                break;
            case 4:
                user.TogetherDay = Mathf.Max(0, user.TogetherDay - 1);
                TogetherDay.text = $"함께한 날짜 : {user.TogetherDay}";
                break;
        }
    }

    public void UpValue(int statsNum)
    {
        var user = DataManager.Instance.CurrentUser;

        switch (statsNum)
        {
            case 1:
                user.FoodCount++;
                FoodCount.text = $"보유 갯수 : {user.FoodCount}";
                break;
            case 2:
                user.PlayCount++;
                PlayCount.text = $"보유 갯수 : {user.PlayCount}";
                break;
            case 3:
                user.Exp++;
                Exp.text = $"{user.Exp} %";
                break;
            case 4:
                user.TogetherDay++;
                TogetherDay.text = $"함께한 날짜 : {user.TogetherDay}";
                break;
        }
    }
}