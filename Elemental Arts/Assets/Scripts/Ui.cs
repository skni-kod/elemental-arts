using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ui : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI expText;

    public int hp = 100;
    public int exp = 0;

    private void Start()
    {
        UpdateHp();
        UpdateExp();
    }

    public void UpdateHp()
    {
        hpText.text = hp.ToString();
    }

    public void UpdateExp()
    {
        expText.text = exp.ToString();
    }
}
