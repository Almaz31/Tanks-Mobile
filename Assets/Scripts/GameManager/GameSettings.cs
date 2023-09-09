using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : NetworkBehaviour
{
    public static GameSettings instance;
    [SerializeField] private Transform playerPrefab;
    public event EventHandler OnGettingKill;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SceneManager_OnLoadEventComplete;
        }
    }
    private void SceneManager_OnLoadEventComplete(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            Transform playerTransform = Instantiate(playerPrefab,SpawnPosition.instance.GetSpawnPoint(),Quaternion.identity);
            playerTransform.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId,true); 
        }

    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        //Do something when client disconnect;
    }
   
    public void PlayerGetKill(ulong playerId)
    {
        OnGettingKill?.Invoke(this, EventArgs.Empty);
        PlayerData playerData = TanksMobileMultiplayer.Instance.GetPlayerDataFromClientId(playerId);
        playerData.playerKills++;
        GameRules.Instance.SomeoneWasKilled();
        Debug.Log(playerData.playerKills);
    }
}
