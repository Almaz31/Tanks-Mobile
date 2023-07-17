using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button closeButton;
    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
    }
    private void Start()
    {
        TanksMobileMultiplayer.Instance.OnFailedToJoinGame += TanksMobileMultiplayer_OnFailedToJoinGame;
        MobileTanksLobby.Instance.OnCreateLobbyStarted += TanksMobileLobby_OnCreateLobbyStarted;
        MobileTanksLobby.Instance.OnCreateLobbyStarted += TanksMobileLobby_OnCreateLobbyFailed;
        MobileTanksLobby.Instance.OnJoinStarted += TanksMobileLobby_OnJoinStarted;
        MobileTanksLobby.Instance.OnJoinFailed+= TanksMobileLobby_OnJoinFailed;
        MobileTanksLobby.Instance.OnQuickJoinFailed += TanksMobileLobby_OnQuickJoinFailed;
        Hide();
    }

    private void TanksMobileLobby_OnQuickJoinFailed(object sender, EventArgs e)
    {
        ShowMessage("Could not find a Lobby to Quick Join");
    }

    private void TanksMobileLobby_OnJoinStarted(object sender, EventArgs e)
    {
        ShowMessage("Joining lobby...");
    }

    private void TanksMobileLobby_OnJoinFailed(object sender, EventArgs e)
    {
        ShowMessage("Failed to join lobby");
    }

    private void TanksMobileLobby_OnCreateLobbyFailed(object sender, EventArgs e)
    {
        ShowMessage("Failed to create lobby");
    }

    private void TanksMobileLobby_OnCreateLobbyStarted(object sender, EventArgs e)
    {
        ShowMessage("Creating Lobby...");
    }

    private void TanksMobileMultiplayer_OnFailedToJoinGame(object sender, EventArgs e)
    {
        if (NetworkManager.Singleton.DisconnectReason == "")
        {
            ShowMessage("Failed to connect");
        }
        else
        {
            ShowMessage(NetworkManager.Singleton.DisconnectReason);
        }
    }
    private void ShowMessage(string message)
    {
        Show();
        messageText.text = message;
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void OnDestroy()
    {
        TanksMobileMultiplayer.Instance.OnFailedToJoinGame -= TanksMobileMultiplayer_OnFailedToJoinGame;
        MobileTanksLobby.Instance.OnCreateLobbyStarted -= TanksMobileLobby_OnCreateLobbyStarted;
        MobileTanksLobby.Instance.OnCreateLobbyStarted -= TanksMobileLobby_OnCreateLobbyFailed;
        MobileTanksLobby.Instance.OnJoinStarted -= TanksMobileLobby_OnJoinStarted;
        MobileTanksLobby.Instance.OnJoinFailed -= TanksMobileLobby_OnJoinFailed;
        MobileTanksLobby.Instance.OnQuickJoinFailed -= TanksMobileLobby_OnQuickJoinFailed;
    }
}
