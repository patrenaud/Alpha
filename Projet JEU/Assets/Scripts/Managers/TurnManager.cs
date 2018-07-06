using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public List<GameObject> m_Characters = new List<GameObject>();
    #region ButtonsAndPrefabs
    public Button m_AttackButton;
    public Button m_MoveButton;
    public Button m_AbilityButton;
    public Button m_EndTurnButton;
    public Button m_MeleeAttackButton;
    public Button m_RangeAttackButton;
    public GameObject m_AttackZone;
    public GameObject m_MoveZone;
    public GameObject m_RangeAttackZone;
    #endregion
    public bool m_SwitchCharacter = false;


    private int m_Turn = 1;

    private static TurnManager m_Instance; // Application d'un Singleton
    public static TurnManager Instance // Application d'un Singleton
    {
        get { return m_Instance; }
    }

    private void Awake() // Application d'un Singleton
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
                m_AttackButton.interactable = true;
                m_MoveButton.interactable = true;
                m_AbilityButton.interactable = true;
                m_EndTurnButton.interactable = true;
                m_MeleeAttackButton.interactable = true;
                m_RangeAttackButton.interactable = true;
                m_AttackZone.GetComponent<CapsuleCollider>().enabled = true;
                m_MoveZone.GetComponent<CapsuleCollider>().enabled = true;
                m_RangeAttackZone.GetComponent<CapsuleCollider>().enabled = true;
            }
            m_SwitchCharacter = false;
        }

        if (m_Characters.Count <= 1)
        {
            Debug.Log("Win the game");
            LoadMain();
        }
    }

    // Le End Button appel cette focntion pour passer au prochain object de la liste
    public void ActivateSwitchCharacter()
    {
        m_SwitchCharacter = true;
    }

    // Ceci recommence la scene de combat
    public void LoadMain()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
