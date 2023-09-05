using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class GameStatsUI : MonoBehaviour
{
    
    private bool isShowing;
    [SerializeField] private Transform playerStatsContainer;
    [SerializeField] private Transform playerStatsTemplater;
    private void Awake()
    {
        
        gameObject.SetActive(false);
        isShowing = false;
        UpdatePlayerStatsServerRpc();
    }
    private void Start()
    {
        GameSettings.instance.OnGettingKill += GameSettings_OnGettingKill;
    }

    private void GameSettings_OnGettingKill(object sender, EventArgs e)
    {
        UpdatePlayerStatsServerRpc();
    }

    public void HideAndShow()
    {
        if (isShowing)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        isShowing=!isShowing;
        
        
    }
    [ServerRpc(RequireOwnership =false)]
    private void UpdatePlayerStatsServerRpc()
    {
        foreach (Transform child in playerStatsContainer)
        {
            if (child == playerStatsTemplater) continue;
            Destroy(child.gameObject);
        }
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            PlayerData playerData= TanksMobileMultiplayer.Instance.GetPlayerDataFromClientId(clientId);
            Transform playerStatsTransform = Instantiate(playerStatsTemplater, playerStatsContainer);
            playerStatsTransform.GetComponent<TextMeshProUGUI>().text = TanksMobileMultiplayer.Instance.GetPlayerName()+" :"+playerData.playerKills;
            playerStatsTransform.gameObject.SetActive(true);
        }
    }
}
