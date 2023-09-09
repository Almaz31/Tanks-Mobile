using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour,IDesctructeble
{
    public float Speed=20f;
    public float BulletLife=7f;
    public float Timer;
    private Rigidbody2D rb;
    public  Vector3 lastDirection;
    public Shooting parent;
    public int bulletId;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        lastDirection = rb.velocity;
        Timer += Time.deltaTime;
        if (Timer > BulletLife)
        {
            Destroying();

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            
           var speed = lastDirection.magnitude;
           var newDirection = Vector3.Reflect(lastDirection.normalized, collision.contacts[0].normal);
            rb.velocity= newDirection*Speed;
        }
        else if (collision.transform.CompareTag("Player"))
        {
            IDesctructeble destructible = collision.collider.GetComponent<IDesctructeble>();
            destructible.Destroying();
            Destroying();
            parent.GetComponent<Shooting>().GetKill();

        }
        else if (collision.collider.CompareTag("Bullet")){

            Destroying();
        }
        else
            return;
    }
    public void Destroying()
    {
        if(!IsOwner) return;
        parent.GetComponent<Shooting>().DestroyServerRpc(bulletId);
    }

}
