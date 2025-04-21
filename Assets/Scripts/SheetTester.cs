using System.Collections.Generic;
using UnityEngine;

public class SheetTester : MonoBehaviour
{
    void Start()
    {
        // infoGoods 타입의 데이터 리스트를 불러옵니다.
        var goodsList = SheetDataLoader.Instance.GetData<infoGoods>("infoGoods");

        Debug.Log($"[테스트] 불러온 infoGoods 항목 수: {goodsList.Count}");

        foreach (var item in goodsList)
        {
            // infoGoods 클래스의 필드에 맞춰 출력
            Debug.Log($"ID: {item.goods_index}, 이름: {item.goods_name}, 타 {item.goods_type}");
        }
    }
}