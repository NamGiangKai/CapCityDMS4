using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pref
{
    public const string CUR_PLAYER_ID = "cur_player_id";
    public const string PLAYER_PREFIX = "player_";
    private const string CHAR_SKIN = "charSkin";
    

    public static int CurPlayerId
    {
        set => PlayerPrefs.SetInt(Pref.CUR_PLAYER_ID, value);
        get => PlayerPrefs.GetInt(Pref.CUR_PLAYER_ID);
    }
    
    public static void SetBool(string key, bool isOn)
    {
        if(isOn)
        {
            PlayerPrefs.SetInt(key, 1);
        }else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }

    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1 ? true : false;
    }
}
