using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Button showStats;
    [SerializeField] private GameStatsUI gameStatsUI;
    private void Awake()
    {
        showStats.onClick.AddListener(() =>
        {
            gameStatsUI.HideAndShow();
        });
    }
}
