using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingLobbuUi : MonoBehaviour
{
    [SerializeField] private Button createGameButton;
    [SerializeField] private Button joinGameButton;
    private void Awake()
    {
        createGameButton.onClick.AddListener(() =>
        {
            TanksMobileMultiplayer.Instance.StartHost();
            SceneLoader.LoadNetwork(SceneLoader.Scene.CharacterSelectScene);
        });
        joinGameButton.onClick.AddListener(() =>
        {
            TanksMobileMultiplayer.Instance.StartClient();
        });
    }
}
