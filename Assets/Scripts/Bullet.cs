using UnityEngine;

public class Bullet : MonoBehaviour,IDesctructeble
{
    public float Speed=20f;
    public float BulletLife=7f;
    public float Timer;
    private Rigidbody2D rb;
   public  Vector3 lastDirection;
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
            Destroy(gameObject);
            Shooting.instance.BulletCount--;
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
            Shooting.instance.BulletCount--;
            
        }
        else
            return;
    }
    public void Destroying()
    {
        Destroy(this.gameObject);
    }

}
