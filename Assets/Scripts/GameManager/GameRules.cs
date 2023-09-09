
using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class GameRules : NetworkBehaviour
{
public static GameRules Instance { get; private set; }
    private int numberOfRounds=10;
    private int currentRound;
    private int playersAlive;
    private GameStatsUI gameStatsUI;

    private void Awake()
    {
        Instance = this;
    }
    public void NewGame()
    {
        currentRound = 0;
    }
    public void NewRound()
    {
        Time.timeScale = 1f;
        playersAlive =TanksMobileMultiplayer.Instance.GetNumberOfPlayers();
        if (playersAlive <= 1)
            EndGame();
        currentRound++;
    }
    public void EndRound()
    {
        StartCoroutine("ILastSeconds");
        StartCoroutine("IStartNewRound");

    }
    private IEnumerable ILastSeconds()
    {
        
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
        gameStatsUI.HideAndShow();

    }
    private IEnumerable IStartNewRound()
    {

        yield return new WaitForSeconds(2f);
        NewRound();

    }
    public void EndGame()
    {
        Time.timeScale=0;
        gameStatsUI.HideAndShow();
    }
    public void SomeoneWasKilled()
    {
        playersAlive--;
        if(playersAlive <= 1)
        {
            if(currentRound<=numberOfRounds-1)
            {
                NewRound();

            }
            else
            {
                EndGame();
            }
        }
    }
}
