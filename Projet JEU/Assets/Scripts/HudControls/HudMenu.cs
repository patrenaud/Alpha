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

    public void SwitchToMainMenu()
    {
        LevelManager.Instance.ChangeLevel("ApplicationLauncher");
    }

    public void SwitchToResults()
    {
        LevelManager.Instance.ChangeLevel("Results");
    }

    public void CloseBackGround()
    {
        m_BackGround.SetActive(false);
    }

    
}
