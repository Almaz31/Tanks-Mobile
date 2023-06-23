using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour, IDesctructeble
{
    [Header("Movement")]
    public float Speed;
    private Rigidbody2D rb;
    public FloatingJoystick joystick;

    [Header("Rotation")]
    public float speedRotation = 5f;
    public float toleranceAngle;

    [Header("Fire")]
    public Transform firePoint;

    [Header("Animation")]
    public Animator anim;
    private bool isAnimating;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (IsMobile.instance.isMobile)
        {
            if (joystick == null)
                joystick = FindObjectOfType<FloatingJoystick>().GetComponent<FloatingJoystick>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
            Fire();
    }

    public void Move()
    {
        
        Vector2 direction;
        if (IsMobile.instance.isMobile)
        {
            direction = Vector2.up * joystick.Vertical + Vector2.right * joystick.Horizontal;
        }
        else
        {
            direction = Vector2.up * Input.GetAxis("Vertical") + Vector2.right * Input.GetAxis("Horizontal");
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        Quaternion currentRotation = transform.rotation;
        if (direction != Vector2.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
        else
        {
            transform.rotation = transform.rotation;
        }

        if (Quaternion.Angle(currentRotation, targetRotation) <= toleranceAngle)
        {
            rb.velocity = direction * Speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Fire()
    {
        Shooting.instance.Shoot(firePoint);
    }

    public void Destroying()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        float maxClipLength = 0f;
        foreach (AnimationClip clip in clips)
        {
            if (clip.length > maxClipLength)
                maxClipLength = clip.length;
        }

        anim.Play("TankExplose");
        isAnimating = true;

        StartCoroutine(WaitAndDestroy(maxClipLength));
    }

    private IEnumerator WaitAndDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
