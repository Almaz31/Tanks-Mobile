using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void OpenSetting()
    {
        Debug.Log("Open Settings");
    }
}
