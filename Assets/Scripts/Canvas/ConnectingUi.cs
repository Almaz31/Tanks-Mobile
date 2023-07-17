using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingUi : MonoBehaviour
{

    private void Start()
    {
        TanksMobileMultiplayer.Instance.OnTryingToJoinGame += TanksMobileMultiplayer_OnTryingToJoinGame;
        TanksMobileMultiplayer.Instance.OnFailedToJoinGame += TanksMobileMultiplayer_OnFailedToJoinGame;
        Hide();
    }

    private void TanksMobileMultiplayer_OnFailedToJoinGame(object sender, EventArgs e)
    {
        Hide();
    }

    private void TanksMobileMultiplayer_OnTryingToJoinGame(object sender, EventArgs e)
    {
        Show();
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
        TanksMobileMultiplayer.Instance.OnTryingToJoinGame -= TanksMobileMultiplayer_OnTryingToJoinGame;
        TanksMobileMultiplayer.Instance.OnFailedToJoinGame -= TanksMobileMultiplayer_OnFailedToJoinGame;
    }
}
