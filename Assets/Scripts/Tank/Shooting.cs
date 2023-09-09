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
    public int freeId;
    [SerializeField] private List<GameObject> spawnedBullets=new List<GameObject>();


    private void Start()
    {
        freeId = 0;
    }
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
        rbBullet.GetComponent<Bullet>().bulletId=freeId;
        freeId++;
        rbBullet.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        BulletCount++;

    }
    [ServerRpc(RequireOwnership =false)]
    public void DestroyServerRpc(int bulletId)
    {

        for (int i =0; i < spawnedBullets.Count; i++)
        {
            GameObject bullet = spawnedBullets[i];
            if (bulletId == bullet.GetComponent<Bullet>().bulletId)
            {
                bullet.GetComponent<NetworkObject>().Despawn();
                spawnedBullets.RemoveAt(i);
                BulletDestroyed();
                break;
            }
        }
    }
private void BulletDestroyed()
    {
        BulletCount--;
    }
    public void GetKill()
    {
        GameSettings.instance.PlayerGetKill(OwnerClientId);
    }
}
