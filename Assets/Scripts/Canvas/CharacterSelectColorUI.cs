using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectColorUI : MonoBehaviour
{
    [SerializeField] private int colorId;
    [SerializeField] private Image  image;
    [SerializeField] private GameObject SelectedGameObject;


     private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            TanksMobileMultiplayer.Instance.ChangePlayerColor(colorId);
        });
    }
    private void Start()
    {
        TanksMobileMultiplayer.Instance.OnPlayerDataNetworkListChanged += TanksMobileMultiplayer_OnPlayerDataNetworkListChanged;
        image.color=TanksMobileMultiplayer.Instance.GetPlayerColor(colorId);
        UpdateSelected();
    }

    private void TanksMobileMultiplayer_OnPlayerDataNetworkListChanged(object sender, EventArgs e)
    {
        UpdateSelected();
    }

    private void UpdateSelected()
    {
        if (TanksMobileMultiplayer.Instance.GetPlayerData().colorId == colorId)
        {
            SelectedGameObject.SetActive(true);
        }
        else
        {
            SelectedGameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        TanksMobileMultiplayer.Instance.OnPlayerDataNetworkListChanged -= TanksMobileMultiplayer_OnPlayerDataNetworkListChanged;
    }
}
