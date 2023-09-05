using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Shooting : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public int numberOfBullets = 5;
    public int BulletCount;
    public Transform firePoint;
    [SerializeField] private List<GameObject> spawnedBullets=new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }
    public void Shoot()
    {
        if (!IsOwner) return;
        if (BulletCount < numberOfBullets)
        {
            ShootServerRpc();

        }
        
       
    }
    [ServerRpc]
    private void ShootServerRpc()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        spawnedBullets.Add(bullet);
        bullet.GetComponent<Bullet>().parent = this;
        bullet.GetComponent<NetworkObject>().Spawn();
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        float bulletSpeed = rbBullet.GetComponent<Bullet>().Speed;
        rbBullet.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        BulletCount++;
    }
    [ServerRpc(RequireOwnership =false)]
    public void DestroyServerRpc()
    {
        GameObject toDestroy = spawnedBullets[0];
        toDestroy.GetComponent<NetworkObject>().Despawn();
        spawnedBullets.Remove(toDestroy);
        Destroy(toDestroy);
    }
    public void BulletDestroyed()
    {
        BulletCount--;
    }
    public void GetKill()
    {
        GameSettings.instance.PlayerGetKill(OwnerClientId);
    }
}
