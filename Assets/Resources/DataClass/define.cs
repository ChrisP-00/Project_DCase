using System.Collections.Generic;
[System.Serializable]
public class define
{
	public int define_index;
	public string description;
	public int value;

	public static Dictionary<int, define> tableDic = new Dictionary<int, define>();
}
