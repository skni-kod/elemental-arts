using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private Player player;
    public void SaveProgress()
    {
        player = GetComponent<Player>();
        PlayerPrefs.SetInt("hp", player.hp);
        PlayerPrefs.SetInt("exp", player.exp);
    }
}
