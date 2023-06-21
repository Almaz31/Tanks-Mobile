using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed=20f;
    public float BulletLife=7f;
    public float Timer;
    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > BulletLife)
        {
            Destroy(gameObject);
            Shooting.instance.BulletCount--;
        }
    }

}
