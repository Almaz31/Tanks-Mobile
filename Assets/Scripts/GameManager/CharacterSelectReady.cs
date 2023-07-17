using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEditor.PackageManager;
using System;

public class CharacterSelectReady : NetworkBehaviour

{
    public static CharacterSelectReady instance { get; private set; }
    private Dictionary<ulong, bool> playerReadyDictionary;
    public event EventHandler OnReadyChange;

    private void Awake()
    {
        instance = this;
        playerReadyDictionary = new Dictionary<ulong, bool>();
    }
    public void SetPlayerReady()
    {
        SetPlayerReadyServerRpc();
    }
    [ServerRpc(RequireOwnership =false)]
    private void SetPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        SetPlayerReadyClientRpc(serverRpcParams.Receive.SenderClientId);
        playerReadyDictionary[serverRpcParams.Receive.SenderClientId] = true;

        bool allClientsReady = true;
        foreach(ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if(!playerReadyDictionary.ContainsKey(clientId)|| !playerReadyDictionary[clientId])
            {
                allClientsReady= false;
                break;
            }
        }
        if(allClientsReady)
        {
            MobileTanksLobby.Instance.DeleteLobby();
            SceneLoader.LoadNetwork(SceneLoader.Scene.GameScene);
        }
    }
    [ClientRpc]
    private void SetPlayerReadyClientRpc(ulong clientId)
    {
        playerReadyDictionary[clientId] = true;
        OnReadyChange?.Invoke(this,EventArgs.Empty);
    }
    public bool isPlayerReady(ulong clientId)
    {
        return playerReadyDictionary.ContainsKey(clientId)&&playerReadyDictionary[clientId];
    }
}
