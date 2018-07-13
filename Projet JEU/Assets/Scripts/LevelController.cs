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
    private List<int> m_RandomIndex = new List<int>();
    private List<EnemyAI> m_Enemies = new List<EnemyAI>();

    private int m_TurnIndex = 0;

    private void Awake()
    {
        //m_Enemies = m_Data.m_EnemyListData;
    }

    private void Start()
    {
        GeneratePlayer();
        GenerateEnemies();
        GenerateBoss();        
    }

    private void GenerateEnemies()
    {
        for (int i = 0; i < m_EnemySpawnPoints.Count; i++)
        {
            m_RandomIndex.Add(i);
        }

        for (int i = 0; i < m_Data.m_EnemyListData.Count; i++)
        {
            // Le SpawnIndex sert à ce que les enemies ne se spawn pas l'un par dessu l'autre. Merci Raph !! :)
            int Spawnindex = m_RandomIndex[Random.Range(0, m_RandomIndex.Count)];

            // Ceci représente ce que GeneratePlayer fait en 3 lignes. On instancie la liste d'enemy dans les spawnpoitnts au hazard
            GameObject enemy = Instantiate(m_Data.m_EnemyListData[i].gameObject, m_EnemySpawnPoints[Spawnindex].transform.position, Quaternion.identity);
            EnemyAI enemyAi = enemy.GetComponent<EnemyAI>();
            m_Enemies.Add(enemyAi);
            enemyAi.m_FinishTurn += OnEnemyDone; // Action
            enemyAi.m_OnDeath += OnEnemyDeath; // Action
           
            m_RandomIndex.Remove(Spawnindex);

            enemyAi.m_IsPlaying = false;
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
        player.m_FinishTurn += OnPlayerDone; // Action
    }

    private void OnPlayerDone()
    {
        Debug.Log("Player finished turn");
        PlayerManager.Instance.m_MainUI.DeactivateUI();
        NextTurn();
    }

    private void OnEnemyDone()
    {
        //m_Enemies[m_TurnIndex].GetComponent<EnemyAI>().m_IsPlaying = false;
        m_TurnIndex++;
        NextTurn();
    }

    private void NextTurn()
    {
        if (m_TurnIndex < m_Enemies.Count)
        {
            /*Debug.Log("First Step");
            if (m_TurnIndex != 0)
            {
                Debug.Log("Not First Enemy");
                m_Enemies[m_TurnIndex].GetComponent<EnemyAI>().m_IsPlaying = true;
                m_Enemies[m_TurnIndex - 1].GetComponent<EnemyAI>().m_IsPlaying = false;
            }
            else
            {
                Debug.Log("First Enemy");
                m_Enemies[m_TurnIndex].GetComponent<EnemyAI>().m_IsPlaying = true;
            }*/
            m_Enemies[m_TurnIndex].PlayTurn();
        }
        else
        {
            PlayerManager.Instance.m_MainUI.ActivatePlayerUiOnTurnBegin();
            PlayerManager.Instance.m_Player.ActivateActions();
            m_TurnIndex = 0;
        }
    }

    private void OnEnemyDeath(EnemyAI aEnemy)
    {
        m_Enemies.Remove(aEnemy);
    }
}
