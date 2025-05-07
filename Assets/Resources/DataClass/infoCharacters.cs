using System.Collections.Generic;
[System.Serializable]
public class infoCharacters
{
	public int character_index;
	public string character_name;

	public static Dictionary<string, infoCharacters> tableDic = new Dictionary<string, infoCharacters>();
}
