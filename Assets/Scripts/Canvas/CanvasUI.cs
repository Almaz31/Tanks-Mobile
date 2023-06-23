using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasUI : MonoBehaviour
{

    public GameObject Joystick;
    public GameObject FireButton;
    public GameObject ExitMenu;
    private bool isExitDown = false;
    void Start()
    {
        if (IsMobile.instance.isMobile)
        {
            Joystick.SetActive(true);
            FireButton.SetActive(true);
        }
        else
        {
            Joystick.SetActive(false);
            FireButton.SetActive(false);
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!isExitDown)
        {
            ExitMenu.SetActive(true);
            isExitDown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isExitDown)
        {
            ReturnToGame();
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ReturnToGame()
    {
        ExitMenu.SetActive(false);
        isExitDown = false;
    }
}
