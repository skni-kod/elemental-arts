using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Ui ui;
    private int hp;

    void Start()
    {
        //ui = GetComponentInParent<Ui>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        ui.hp -= 10;
        ui.UpdateHp();
    }
}
