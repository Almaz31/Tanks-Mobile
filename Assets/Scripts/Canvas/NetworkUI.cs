using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField]
    private Button HostBut;
    [SerializeField]
    private Button ClientBut;

    private void Awake()
    {
        HostBut.onClick.AddListener(() =>
        {
            TanksMobileMultiplayer.Instance.StartHost();
            Hide();
        });
        ClientBut.onClick.AddListener(() =>
        {
            TanksMobileMultiplayer.Instance.StartClient();
            Hide();
        });
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
}
