using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudMenu : MonoBehaviour
{
    public GameObject m_BackGround;

    public void SwitchToGamePlay()
    {
        LevelManager.Instance.ChangeLevel("Main");
        gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        LevelManager.Instance.ChangeLevel("Main");
        LevelManager.Instance.RestartLevelIndex();
    }

    public void SwitchToMainMenu()
    {
        LevelManager.Instance.ChangeLevel("ApplicationLauncher");
        LevelManager.Instance.RestartMain();
    }

    public void SwitchToResults()
    {
        LevelManager.Instance.ChangeLevel("Results");
    }

    public void CloseBackGround()
    {
        m_BackGround.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
