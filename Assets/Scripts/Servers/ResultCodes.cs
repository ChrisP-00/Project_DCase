public enum ResultCodes
{
    Ok = 0,

    // 1 ~ 9
    InputData_MissingRequiredField = 0001,
    
    // 네트워크 에러 -100 ~ 999
    NetworkError = -100,
    
    // 트렌젝션 오류 -1000 ~ -1100
    Transaction_Fail_Rollback = -1001,
    Transaction_Fail_ConnectionStringNull = -1002,


    // 계정 오류 -2000 ~ -2499
    Create_Account_Fail_Duplicate = -2001,

    Ban_Account = -2002,
    Deleted_Account = -2003,

    Create_Account_Fail = -2101,
    Create_Account_Insert_Data_Fail = -2102,
    Create_Account_Fail_Exception = -2103,

    Update_Account_NoAccount = -2201,
    Update_Account_SameNickname = -2202,
    Update_Account_InvalidNickname = -2203,
    Update_Account_Fail_Exception = -2299,


    // 로그인 오류 -2500 ~ -2999
    Login_Fail_NotUser = -2501,
    Login_Fail_Exception = -2999,

    // 재화 사용 오류 -3000 ~ -3999
    Goods_Fail_NotEnough = -3001,
    Goods_Fail_NotExist = -3002,
    Goods_Fail_NotValidType = -3003,
    Goods_Fail_LessThanZero = -3004,
    Goods_Fail_Exception = -3999,


    // 미션 오류 -5000 ~ -5999
    Mission_Fail_NotAvailable = -5001,
    Mission_Fail_AlreadyCompleted = -5002,
    Mission_Fail_Exception = -5999,

    // 장착 오류 -6000 ~ -6999
    // 캐릭터 장착 오류 -6100 ~ -6200
    Equip_Fail_CharacterAlreadyEquipped = -6101,
    Equip_Fail_NoCharacter = -6102,
    Equip_Fail_CharacterNotExist,

    // 아이템 장착 오류 -6200 ~ -6300
    Equip_Fail_NoItem = -6201,
    Equip_Fail_ItemAlreadyEquipped = -6202,
    Equip_Fail_NoEquippedItem = -6203,
    Equip_Fail_Incompatible = -6204,
    Equip_Fail_NotExist = -6291,
    Equip_Fail_Exception = -6299,


    // 공지 오류 -9000 ~ -9999
    Notice_Fail_NoNotice = -9001,
    Notice_Fail_PermissionDenied = -9002,
    Notice_Fail_InvalidContent = -9003,
    Notice_Fail_AlreadyExists = -9004,
    Notice_Fail_Duplicate = -905,
    Notice_Fail_UpdateFail = -9006,
    Notice_Fail_Exception = -9999,
}