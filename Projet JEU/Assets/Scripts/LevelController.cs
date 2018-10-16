using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private List<LevelData> m_Data;    
    [SerializeField]
    private Transform m_PlayerSpawnPoint;
    [SerializeField]
    private Transform m_BossSpawnPoint;
    [SerializeField]
    private List<Transform> m_EnemySpawnPoints;
    [SerializeField]
    private List<Transform> m_EnemyPatrolPoints;
    private List<int> m_RandomIndex = new List<int>();
    private List<EnemyAI> m_Enemies = new List<EnemyAI>();
    private int m_CurrentLevel;
    private bool m_CanLoadScene = true;

    private int m_TurnIndex = 0;

    private void Awake()
    {
        
    }

    private void Start()
    {
        m_CurrentLevel = LevelManager.Instance.GetLevelIndex();
        GeneratePlayer();
        GenerateEnemies();
        GenerateBoss();
        LevelManager.Instance.SetLevelIndex();
        m_CanLoadScene = true;

        // Si le joueur est mort, il peut recommencer le même niveau. (FreezeLevelIndex)
    }

    private void GenerateEnemies()
    {
        for (int i = 0; i < m_EnemySpawnPoints.Count; i++)
        {
            m_RandomIndex.Add(i);
        }

        for (int i = 0; i < m_Data[m_CurrentLevel].m_EnemyListData.Count; i++)
        {
            // Le SpawnIndex sert à ce que les enemies ne se spawn pas l'un par dessu l'autre. Merci Raph !! :)
            int Spawnindex = m_RandomIndex[Random.Range(0, m_RandomIndex.Count)];

            // Ceci représente ce que GeneratePlayer fait en 3 lignes. On instancie la liste d'enemy dans les spawnpoitnts au hazard
            GameObject enemy = Instantiate(m_Data[m_CurrentLevel].m_EnemyListData[i].gameObject, m_EnemySpawnPoints[Spawnindex].transform.position, Quaternion.identity);
            
            EnemyAI enemyAi = enemy.GetComponent<EnemyAI>();
    
            m_Enemies.Add(enemyAi);
            m_Enemies[i].GetComponent<EnemyAI>().m_PatrolDestination = m_EnemyPatrolPoints[i];

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
        GameObject prefab = Resources.Load("Prefabs/RealPlayer") as GameObject;
        // Ceci est le OneLiner
        // Instantiate((GameObject)prefab, m_PlayerSpawnPoint.position, Quaternion.identity).GetComponent<PlayerController>().m_FinishTurn += OnPlayerDone;
        GameObject go = Instantiate((GameObject)prefab, m_PlayerSpawnPoint.position, Quaternion.identity);
        // This is to set the Player's health after Levels (Constitution) have been set up
        PlayerManager.Instance.m_MaxHealth = PlayerManager.Instance.PlayerHP();
        PlayerManager.Instance.m_MainUI.m_HealthBar.value = 1;        
        PlayerController player = go.GetComponent<PlayerController>();

        player.m_FinishTurn += OnPlayerDone; // Action
    }

    private void OnPlayerDone()
    {
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
        if (m_TurnIndex < m_Enemies.Count)
        {
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
        PlayerManager.Instance.m_MainUI.m_XpBar.value += 0.35f;
        m_Enemies.Remove(aEnemy);

        if (m_Enemies.Count == 0 && m_CanLoadScene)
        {
            m_CanLoadScene = false;
            LevelManager.Instance.ChangeLevel("Results");
        }
    }
}
