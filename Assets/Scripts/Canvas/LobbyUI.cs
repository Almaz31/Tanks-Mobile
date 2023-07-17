using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button joinCodeButton;
    [SerializeField] private LobbyCreateUI lobbyCreateUI;
    [SerializeField] private Transform lobbyContainer;
    [SerializeField] private Transform lobbyTemplater;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private TMP_InputField playerNameInputField;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            MobileTanksLobby.Instance.LeaveLobby();
            SceneLoader.Load(SceneLoader.Scene.MainMenuScene);
        });
        createLobbyButton.onClick.AddListener(() =>
        {
            lobbyCreateUI.Show();
        });
        quickJoinButton.onClick.AddListener(() =>
        {
            MobileTanksLobby.Instance.QuickJoin();
        });
        joinCodeButton.onClick.AddListener(() =>
        {
            MobileTanksLobby.Instance.JoinWithCode(joinCodeInputField.text);
        });
        lobbyTemplater.gameObject.SetActive(false);
    }
    private void Start()
    {
        playerNameInputField.text = TanksMobileMultiplayer.Instance.GetPlayerName();
        playerNameInputField.onValueChanged.AddListener((string newText) =>
        {
            TanksMobileMultiplayer.Instance.SetPlayerName(newText);
        });
        MobileTanksLobby.Instance.OnLobbyListChanged += MobileTanksLobby_OnLobbyListChanged;
        UpdateLobbyList(new List<Lobby>());
    }

    private void MobileTanksLobby_OnLobbyListChanged(object sender, MobileTanksLobby.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.Lobies);
    }

    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        foreach(Transform child in lobbyContainer)
        {
            if (child == lobbyTemplater) continue;
            Destroy(child.gameObject);
        }
        foreach (Lobby lobby in lobbyList)
        {
            Transform lobbyTransform = Instantiate(lobbyTemplater,lobbyContainer);
            lobbyTransform.gameObject.SetActive(true);
            lobbyTransform.GetComponent<LobbyListSingleUI>().SetLobby(lobby);
        }
    }
    private void OnDestroy()
    {
        MobileTanksLobby.Instance.OnLobbyListChanged -= MobileTanksLobby_OnLobbyListChanged;
    }
}
