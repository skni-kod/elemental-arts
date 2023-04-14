using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Ui ui;
    public int hp = 100;
    public int exp = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey("hp"))
            hp = PlayerPrefs.GetInt("hp");
        if (PlayerPrefs.HasKey("exp"))
            exp = PlayerPrefs.GetInt("exp");
    }

    private void OnTriggerEnter(Collider other)
    {
        hp -= 10;
        ui.UpdateHp(hp);
    }
}
