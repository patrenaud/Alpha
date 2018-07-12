using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private LevelData m_Data;
    [SerializeField]
    private Transform m_PlayerSpawnPoint;
    [SerializeField]
    private Transform m_BossSpawnPoint;
    [SerializeField]
    private List<Transform> m_EnemySpawnPoints;
    private List<EnemyAI> m_Enemies;

    private int m_TurnIndex = 0;

    private void Awake()
    {
		m_Enemies = m_Data.m_EnemyListData;
    }

    private void Start()
    {
        GenerateEnemies();
        GenerateBoss();
        GeneratePlayer();
    }

    private void GenerateEnemies()
    {
        // Place les enemies du m_Data au hazard des points m_EnemySpawnPoints
        // On ajoute OnEnemyDeath à l'action des enemies (m_OnDeath)
        for (int i = 0; i < m_Enemies.Count; i++)
        {
            m_Enemies[i].gameObject.transform.position = m_EnemySpawnPoints[Random.Range(0, m_EnemySpawnPoints.Count)].transform.position;
            m_Enemies[i].m_OnDeath += OnEnemyDeath;
            // NEED TO INSTANTIATE
        }
    }

    private void GenerateBoss()
    {
        
    }

    private void GeneratePlayer()
    {
        GameObject prefab = Resources.Load("Prefabs/Player") as GameObject;

        // Instantiate((GameObject)prefab, m_PlayerSpawnPoint.position, Quaternion.identity).GetComponent<PlayerController>().m_FinishTurn += OnPlayerDone;
        GameObject go = Instantiate((GameObject)prefab, m_PlayerSpawnPoint.position, Quaternion.identity);
        PlayerController player = go.GetComponent<PlayerController>();
        player.m_FinishTurn += OnPlayerDone;
    }

    private void OnPlayerDone()
    {
        Debug.Log("Player finished turn");
        PlayerManager.Instance.m_MainUI.DeactivateUI();
        NextTurn();
    }

    private void OnEnemyDone()
    {
        m_TurnIndex++;
        NextTurn();
    }

    private void NextTurn()
    {
        // si turnIndex < enemyList.count -> tour de l'ennemi 
        if(m_TurnIndex < m_Enemies.Count)
        {
            m_Enemies[m_TurnIndex - 1].m_IsPlaying = false;
            m_Enemies[m_TurnIndex].m_IsPlaying = true;
        }
        else
        {
            PlayerManager.Instance.m_MainUI.ActivatePlayerUiOnTurnBegin();
            m_TurnIndex = 0;
        } 
    }

    private void OnEnemyDeath(EnemyAI aEnemy)
    {
        m_Enemies.Remove(aEnemy);
    }
}
