using System.Collections;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static bool[] unlockedWeapons = new bool[4];

    private void Awake()
    {
        SetGunUnlocks();
    }

    public static void SetGunUnlocks()
    {
        SaveData.unlockedWeapons[0] = PlayerPrefs.GetInt("HasSword") > 0 ? true : false;
        SaveData.unlockedWeapons[1] = PlayerPrefs.GetInt("HasProtractor") > 0 ? true : false;
        SaveData.unlockedWeapons[2] = PlayerPrefs.GetInt("HasSineCosine") > 0 ? true : false;
        SaveData.unlockedWeapons[3] = PlayerPrefs.GetInt("HasBoomerang") > 0 ? true : false; 
    }


}
