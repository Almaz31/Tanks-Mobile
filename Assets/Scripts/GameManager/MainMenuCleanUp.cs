using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MainMenuCleanUp : MonoBehaviour
{
    private void Awake()
    {
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
        if(TanksMobileMultiplayer.Instance != null)
        {
            Destroy(TanksMobileMultiplayer.Instance.gameObject);
        }
        if(MobileTanksLobby.Instance != null)
        {
            Destroy(MobileTanksLobby.Instance.gameObject);
        }
    }
}