using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : NetworkBehaviour
{
    public static GameSettings instance;
    public List<NetworkObject> playerList = new List<NetworkObject>();
    private bool isStarting = true;
    [SerializeField] private Transform playerPrefab;
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
    private void IsLastPlayer()
    {
        if(playerList.Count <=1&&!isStarting) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        Debug.Log(playerList);
    }
    public void AddPlayer(NetworkObject player)
    {
        playerList.Add(player);
        if (playerList.Count > 1) isStarting = false;
        IsLastPlayer();
    }
    public void RemovePlayer(NetworkObject player)
    {
        playerList.Remove(player);
        IsLastPlayer();
    }
    public bool IsGameStarting()
    {
        return isStarting;
    }
}
