using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankController : NetworkBehaviour, IDesctructeble
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

    [Header("Vision")]
    [SerializeField] private PlayerVisual playerVisual;

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
        PlayerData playerData = TanksMobileMultiplayer.Instance.GetPlayerDataFromClientId(OwnerClientId);
        playerVisual.SetPlayerColor(TanksMobileMultiplayer.Instance.GetPlayerColor(playerData.colorId));
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner)
        {
            return;
        }
        if (isAnimating)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        // Move();
        HandleMovementServerAuth();
    }

    private void HandleMovementServerAuth()
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
        HandleMovementServerRpc(direction);
    }
    [ServerRpc(RequireOwnership =false)]
    private void HandleMovementServerRpc(Vector2 direction)
    {

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

    public override void OnNetworkSpawn()
    {
        if(!IsOwner)return;
        UpdatePositionServerRpc();
        
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
        DestroyServerRpc();
    }
    [ServerRpc(RequireOwnership = false)]
    private void DestroyServerRpc()
    {
        GetComponent<NetworkObject>().Despawn(gameObject);
        Destroy(gameObject);
        GameSettings.instance.RemovePlayer(this.NetworkObject);
    }
    [ServerRpc(RequireOwnership =false)]
    private void UpdatePositionServerRpc()
    {
        Vector2 spawnPoint = SpawnPosition.instance.GetSpawnPoint();
        transform.position = spawnPoint;
    }

}
