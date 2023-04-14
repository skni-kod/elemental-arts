using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ui : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI expText;

    private void Start()
    {
        UpdateHp(PlayerPrefs.HasKey("hp") ? PlayerPrefs.GetInt("hp") : 100);
        UpdateExp(PlayerPrefs.HasKey("exp") ? PlayerPrefs.GetInt("exp") : 0);
    }

    public void UpdateHp(int hp)
    {
        hpText.text = hp.ToString();
    }

    public void UpdateExp(int exp)
    {
        expText.text = exp.ToString();
    }
}
