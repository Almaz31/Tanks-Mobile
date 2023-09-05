using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum State
    {
        Starting,
        NewRound,
        Game,
        Ending
    }
    public static State currentState;
    public void SetState(State targetState)
    {
        GameState.currentState = targetState;
    }
}
