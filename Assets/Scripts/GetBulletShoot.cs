using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBulletShoot : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            Shooting.instance.BulletCount--;
        }
    }
}
