public class EndPoint
{
    private static readonly string Account = "Account/";
    private static readonly string Character = "Character/";
    private static readonly string Reward = "Reward/";

    public static readonly string CreateAccount = Account + "CreateAccount";
    public static readonly string Login = Account + "Login";
    public static readonly string LoginOrCreateAccount = Account + "LoginOrCreateAccount";
    public static readonly string Logout = Account + "Logout";

    public static readonly string EquipCharacter = Character + "EquipCharacter";
    public static readonly string EquipItem = Character + "EquipItem";
    public static readonly string UnequipItem = Character + "UnequipItem";
    public static readonly string PlayStatus = Character + "PlayStatus";

    public static readonly string ReceiveMission = Reward + "ReceiveMission";
}