using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public static Shooting instance;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int numberOfBullets = 5;
    public int BulletCount;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }
    public void Shoot()
    {
        if (BulletCount < numberOfBullets)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            Bullet bulletCode = rbBullet.GetComponent<Bullet>();
            float bulletSpeed = bulletCode.Speed;
            rbBullet.AddForce(firePoint.up * 20f, ForceMode2D.Impulse);
            float bulletLife = bulletCode.BulletLife;
            BulletCount++;

        }
       
    }
}
