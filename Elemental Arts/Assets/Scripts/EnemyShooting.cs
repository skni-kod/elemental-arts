using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject barrelEnd;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timeToShoot = 2f;
    [SerializeField] private float timeToNextShot = 2f;
    [SerializeField] private float bulletSpeed = 2f;

    void Update()
    {
        timeToShoot -= Time.deltaTime;
        if (timeToShoot <= 0)
        {
            timeToShoot = timeToNextShot;
            SpawnBullet();
        }
    }
    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, barrelEnd.transform.position, barrelEnd.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(barrelEnd.transform.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, 5f);
    }
}
