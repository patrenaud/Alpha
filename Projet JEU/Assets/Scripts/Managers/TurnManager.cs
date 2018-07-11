using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnManager : DontDestroyOnLoad
{
    public PlayerManager m_PlayerManager;
    public List<GameObject> m_Characters = new List<GameObject>();
    public bool m_SwitchCharacter = false;

    private int m_Turn = 1;

    private static TurnManager m_Instance; // Application d'un Singleton
    public static TurnManager Instance // Application d'un Singleton
    {
        get { return m_Instance; }
    }

    protected override void Awake() // Application d'un Singleton
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_SwitchCharacter = true;
        }

        if (m_SwitchCharacter) // Lorsque SwitchCharacter est appelé, le tour du prochain dans la liste débute.
        {
            if (m_Turn < m_Characters.Count)
            {
                //m_Characters[m_Turn].GetComponent<EnemyController>().m_IsPlaying = true;
                m_Characters[m_Turn].GetComponent<EnemyAI>().m_IsPlaying = true;
                m_Turn++;
            }
            else
            { // Lorsque l'on atteint le bout de la liste, on retourne au Player qui est le premier de la liste
                m_Turn = 1;

                // Les boutons sont activés pour le tour du joueur
                m_PlayerManager.m_MainUI.m_AttackButton.interactable = true;
                m_PlayerManager.m_MainUI.m_MoveButton.interactable = true;
                m_PlayerManager.m_MainUI.m_AbilityButton.interactable = true;
                m_PlayerManager.m_MainUI.m_EndTurnButton.interactable = true;
                m_PlayerManager.m_MainUI.m_MeleeAttackButton.interactable = true;
                m_PlayerManager.m_MainUI.m_RangeAttackButton.interactable = true;
                PlayerManager.Instance.m_Player.m_AttackZone.GetComponent<CapsuleCollider>().enabled = true;
                PlayerManager.Instance.m_Player.m_MoveZone.GetComponent<CapsuleCollider>().enabled = true;
                PlayerManager.Instance.m_Player.m_RangeAttackZone.GetComponent<CapsuleCollider>().enabled = true;
            }
            m_SwitchCharacter = false;
        }

        if (m_Characters.Count <= 1)
        {
            Debug.Log("Win the game");
            LevelManager.Instance.ChangeLevel("Main");
        }
    }

    // Le End Button appel cette focntion pour passer au prochain object de la liste
    public void ActivateSwitchCharacter()
    {
        m_SwitchCharacter = true;
    }
}
