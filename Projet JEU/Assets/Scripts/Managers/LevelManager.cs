using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : DontDestroyOnLoad
{
    [SerializeField]
    private GameObject m_LoadingScreen;
    [SerializeField]
    private GameObject m_BackGround;
    [SerializeField]
    private Button m_StartButton;

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
        m_LoadingScreen.SetActive(false);
        m_BackGround.SetActive(true);
        m_StartButton.gameObject.SetActive(true);
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
        PlayerManager.Instance.m_MainUI.ActivateSelf();
    }

    public void ChangeLevel(string i_Scene)
    {
        StartLoading();

        StartCoroutine(LoadSceneTimer(i_Scene));

        SceneManager.sceneLoaded += OnLoadingDone;
    }

    private IEnumerator LoadSceneTimer(string i_Scene)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(i_Scene);
    }
}
