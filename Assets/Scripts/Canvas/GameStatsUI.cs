using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class GameStatsUI : NetworkBehaviour
{
    
    private bool isShowing;
    [SerializeField] private Transform playerStatsContainer;
    [SerializeField] private Transform playerStatsTemplater;
    [SerializeField] private Button backToMenuButton;
    private void Awake()
    {
        backToMenuButton.onClick.AddListener(() => {
            MobileTanksLobby.Instance.LeaveLobby();
            SceneLoader.Load(SceneLoader.Scene.MainMenuScene);
        });
        gameObject.SetActive(false);
        isShowing = false;
        //UpdatePlayerStats();
    }
    private void Start()
    {
        GameSettings.instance.OnGettingKill += GameSettings_OnGettingKill;
    }

    private void GameSettings_OnGettingKill(object sender, EventArgs e)
    {
       // UpdatePlayerStats();
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

    //[ServerRpc(RequireOwnership = false)]
    //public void GetPlayerStatsServerRpc()
    //{
    //    // Отримання статистики гравців
    //    List<PlayerData> playerStats = new List<PlayerData>();
    //    foreach (var clientId in NetworkManager.Singleton.ConnectedClients.Keys)
    //    {
    //        PlayerData playerData = TanksMobileMultiplayer.Instance.GetPlayerDataFromClientId(clientId);
    //        playerStats.Add(playerData);
    //    }

    //    // Виклик RPC на клієнті для передачі статистики
    //    RpcUpdatePlayerStatsClientRpc(playerStats);
    //}
    //[ClientRpc]
    //private void RpcUpdatePlayerStatsClientRpc(List<PlayerData> playerStats)
    //{
    //    foreach (Transform child in playerStatsContainer)
    //    {
    //        if (child == playerStatsTemplater) continue;
    //        Destroy(child.gameObject);
    //    }

    //    foreach (var playerData in playerStats)
    //    {
    //        Transform playerStatsTransform = Instantiate(playerStatsTemplater, playerStatsContainer);
    //        playerStatsTransform.GetComponent<TextMeshProUGUI>().text = playerData.playerName + ": " + playerData.playerKills;
    //        playerStatsTransform.gameObject.SetActive(true);
    //    }
    //}

    //public void UpdatePlayerStats()
    //{
    //    if (IsServer)
    //    {
    //        // Якщо це сервер, то викликайте метод GetPlayerStatsServerRpc()
    //        GetPlayerStatsServerRpc();
    //    }
    //    else
    //    {
    //        GetPlayerStatsServerRpc();
    //    }
    //}

    
}
