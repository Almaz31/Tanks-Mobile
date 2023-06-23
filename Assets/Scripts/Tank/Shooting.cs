using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public static Shooting instance;
    public GameObject bulletPrefab;
    public int numberOfBullets = 5;
    public int BulletCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Shoot(Transform firePoint)
    {
        
        if (BulletCount < numberOfBullets)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            Bullet bulletCode = rbBullet.GetComponent<Bullet>();
            float bulletSpeed = bulletCode.Speed;
            rbBullet.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
            BulletCount++;

        }
       
    }
}
