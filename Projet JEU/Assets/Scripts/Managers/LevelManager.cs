﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : DontDestroyOnLoad
{
    private int m_LevelIndex = 0;

    [SerializeField]
    private GameObject m_LoadingScreen;
    [SerializeField]
    private GameObject m_BackGround;
    [SerializeField]
    private Button m_StartButton;
    [SerializeField]
    private Button m_QuitButton;
    [SerializeField]
    private GameObject m_DeathImage;

    private static LevelManager m_Instance;
    public static LevelManager Instance
    {
        get { return m_Instance; }
    }

    protected override void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        base.Awake();

        // Sets main screen UI
        m_LoadingScreen.SetActive(false);
        m_BackGround.SetActive(true);
        m_StartButton.gameObject.SetActive(true);
        m_DeathImage.SetActive(false);
        m_QuitButton.gameObject.SetActive(false);
    }

    private void StartLoading()
    {
        PlayerManager.Instance.m_MainUI.DeactivateSelf();
        m_LoadingScreen.SetActive(true);
        m_BackGround.SetActive(false);
        m_StartButton.gameObject.SetActive(false);
    }

    private void OnLoadingDone(Scene i_Scene, LoadSceneMode i_Mode)
    {
        //We remove the function from the action/event list
        SceneManager.sceneLoaded -= OnLoadingDone;
        if (m_LoadingScreen != null)
        {
            m_LoadingScreen.SetActive(false);
        }
        if(i_Scene.name == "Main")
        {
            PlayerManager.Instance.m_MainUI.ActivateSelf();            
        }
        if(i_Scene.name == "Results" && PlayerManager.Instance.m_PlayerDied)
        {
            m_DeathImage.SetActive(true);
            m_QuitButton.gameObject.SetActive(true);
        }
    }

    public void ChangeLevel(string i_Scene)
    {
        StartLoading();
        
        StartCoroutine(LoadSceneTimer(i_Scene));

        SceneManager.sceneLoaded += OnLoadingDone;
    }

    private IEnumerator LoadSceneTimer(string i_Scene)
    {
        CheckSound(i_Scene);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(i_Scene);
    }

    private void CheckSound(string i_Scene)
    {
        if (i_Scene == "Main")
        {
            AudioManager.Instance.SwitchMusic(3f, AudioManager.Instance.m_Cavern);
        }
        if (i_Scene == "Results" && PlayerManager.Instance.m_PlayerDied)
        {
            AudioManager.Instance.SwitchMusic(3f, AudioManager.Instance.m_Death);
        }
        else if (i_Scene == "Results")
        {
            AudioManager.Instance.SwitchMusic(3f, AudioManager.Instance.m_Breathing);
        }
        if(i_Scene == "Application Launcher")
        {
            AudioManager.Instance.SwitchMusic(3f, AudioManager.Instance.m_Ravens);
        }
    }

    public void SetLevelIndex()
    {
        m_LevelIndex++;
    }

    public void RestartLevelIndex()
    {
        m_LevelIndex--;
    }

    public int GetLevelIndex()
    {
        return m_LevelIndex;
    }

    public void RestartMain()
    {
        m_BackGround.SetActive(true);
        m_StartButton.gameObject.SetActive(true);
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
