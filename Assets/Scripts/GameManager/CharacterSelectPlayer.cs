using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPlayer : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject readyGameobject;
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private Button kickButton;
    [SerializeField] private TextMeshPro playerNameText;
    private void Awake()
    {
        kickButton.onClick.AddListener(() =>
        {
            PlayerData playerData = TanksMobileMultiplayer.Instance.GetPlayerDataFromPlayerIndex(playerIndex);
            MobileTanksLobby.Instance.KickPlayer(playerData.playerId.ToString());
            TanksMobileMultiplayer.Instance.KickPlayer(playerData.clientId);
        });
    }
    private void Start()
    {
        TanksMobileMultiplayer.Instance.OnPlayerDataNetworkListChanged += TanksMobileMultiplayer_OnPlayerDataNetworkListChanged;
        CharacterSelectReady.instance.OnReadyChange += CharacterSelectReady_OnReadyChanged;
        kickButton.gameObject.SetActive(NetworkManager.Singleton.IsServer);
        UpdatePlayer();
    }

    private void CharacterSelectReady_OnReadyChanged(object sender, EventArgs e)
    {
        UpdatePlayer();
    }

    private void TanksMobileMultiplayer_OnPlayerDataNetworkListChanged(object sender, EventArgs e)
    {
        UpdatePlayer();
    }
    private void UpdatePlayer()
    {
        if (TanksMobileMultiplayer.Instance.isPlayerIndexConnected(playerIndex))
        {
            Show();

            PlayerData playerData = TanksMobileMultiplayer.Instance.GetPlayerDataFromPlayerIndex(playerIndex);
            readyGameobject.SetActive(CharacterSelectReady.instance.isPlayerReady(playerData.clientId));

            playerNameText.text = playerData.playerName.ToString();

            playerVisual.SetPlayerColor(TanksMobileMultiplayer.Instance.GetPlayerColor(playerData.colorId));
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        TanksMobileMultiplayer.Instance.OnPlayerDataNetworkListChanged -= TanksMobileMultiplayer_OnPlayerDataNetworkListChanged;
    }
}
