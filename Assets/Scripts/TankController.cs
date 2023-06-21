using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [Range(0f, 10f)]
    public float Speed;
    private Rigidbody2D rb;
    public FloatingJoystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    public void Move()
    {
        Vector2 direction=Vector2.up*joystick.Vertical+Vector2.right*joystick.Horizontal;
        rb.velocity = direction*Speed;
        float angle = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg-90;
        if(angle!=-90)
            rb.rotation = angle;
    }
    public void Fire()
    {

    }
}
