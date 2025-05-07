using System.Collections.Generic;
[System.Serializable]
public class infoGoods
{
	public int goods_index;
	public int goods_type;
	public string goods_name;

	public static Dictionary<int, infoGoods> tableDic = new Dictionary<int, infoGoods>();
}
