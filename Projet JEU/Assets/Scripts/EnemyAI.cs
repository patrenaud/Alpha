﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BehaviorState
{
    Idle,
    Patrol,
    MoveToPlayer,
    Attack
}

public class EnemyAI : MonoBehaviour
{
    public Action m_FinishTurn;
    public Action<EnemyAI> m_OnDeath;

    private Material m_EnemyMaterial;

    public bool m_Attackable = false;
    public bool m_IsPlaying = false;
    public Vector3 m_PatrolPos;

    private BehaviorState m_State;
    private Vector3 m_InitialePos;
    Vector3 m_PatrolDestination = new Vector3();

    [SerializeField]
    private EnemyData m_EnemyData;

    private float m_CurrentHealth;
    private NavMeshAgent m_EnemyAgent;
    private float m_CurrentTime = 0;

    private void Start()
    {
        m_State = BehaviorState.Idle;
        m_InitialePos = transform.position;
        m_PatrolPos = m_InitialePos  + new Vector3(2,0,0);
        m_PatrolDestination = m_PatrolPos;
        m_EnemyAgent = GetComponent<NavMeshAgent>();
        m_CurrentHealth = m_EnemyData.EnemyMaxHealth;
        m_EnemyMaterial = GetComponent<Renderer>().material;
    }

    private bool CompareState(BehaviorState i_State)
    {
        return i_State == m_State;
    }

    private void Update()
    {
        if (m_IsPlaying)
        {
            if (CompareState(BehaviorState.Idle))
            {
                UpdateIdle();
            }
            if (CompareState(BehaviorState.Patrol))
            {
                UpdatePatrol();
            }
            if (CompareState(BehaviorState.MoveToPlayer))
            {
                UpdateMovetoPlayer();
            }
            if (CompareState(BehaviorState.Attack))
            {
                UpdateAttack();
            }
        }
        if(m_CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateIdle()
    {
        if (m_IsPlaying)
        {
            if ((PlayerManager.Instance.m_Player.gameObject.transform.position - transform.position).magnitude < m_EnemyData.EnemyMeleeAttackRange)
            {
                ChangeState(BehaviorState.Attack);

            }
            else if ((PlayerManager.Instance.m_Player.gameObject.transform.position - transform.position).magnitude < m_EnemyData.EnemySight)
            {
                ChangeState(BehaviorState.MoveToPlayer);
            }
            else
            {
                ChangeState(BehaviorState.Patrol);
            }
        }
    }

    private void UpdatePatrol()
    {
        m_EnemyAgent.SetDestination(m_PatrolDestination);
        m_CurrentTime += Time.deltaTime;

        if (m_EnemyAgent.destination == m_EnemyAgent.transform.position && m_PatrolDestination != m_InitialePos)
        {
            m_PatrolDestination = m_InitialePos;
        }
        if (m_EnemyAgent.destination == m_EnemyAgent.transform.position && m_PatrolDestination != m_PatrolPos)
        {
            m_PatrolDestination = m_PatrolPos;
        }

        if ((PlayerManager.Instance.m_Player.gameObject.transform.position - transform.position).magnitude < m_EnemyData.EnemySight)
        {
            ChangeState(BehaviorState.MoveToPlayer);
        }

        if (m_CurrentTime >= 2f)
        {
            m_EnemyAgent.SetDestination(transform.position);
            m_CurrentTime = 0;

            ChangeState(BehaviorState.Idle);
            EndTurn();
        }
    }

    private void UpdateMovetoPlayer()
    {
        m_EnemyAgent.SetDestination(PlayerManager.Instance.m_Player.gameObject.transform.position);
        m_CurrentTime += Time.deltaTime;

        if ((PlayerManager.Instance.m_Player.gameObject.transform.position - transform.position).magnitude < m_EnemyData.EnemyMeleeAttackRange)
        {
            m_EnemyAgent.SetDestination(transform.position);
            m_CurrentTime = 0;
            ChangeState(BehaviorState.Attack);
        }

        if (m_CurrentTime >= 2f)
        {
            m_EnemyAgent.SetDestination(transform.position);
            m_CurrentTime = 0;

            ChangeState(BehaviorState.Idle);
            EndTurn();
        }
    }

    private void UpdateAttack()
    {
        
        ChangeState(BehaviorState.Idle);
        EndTurn();
    }

    private void ChangeState(BehaviorState i_State)
    {
        switch (i_State)
        {
            case BehaviorState.Attack:
                {
                    if (m_State != BehaviorState.Attack)
                    {
                        // Activate Anim / Camera
                        // Attack();
                        PlayerManager.Instance.m_Player.m_CurrentHealth -= m_EnemyData.MeleeAttackDamage;
                        PlayerManager.Instance.m_MainUI.m_HealthBar.value = PlayerManager.Instance.m_Player.m_CurrentHealth / PlayerManager.Instance.m_Player.m_MaxHealth;
                    }
                    break;
                }
            case BehaviorState.Patrol:
                {
                    if (m_State != BehaviorState.Patrol)
                    {
                        // Move to random position around the map
                        // if (in range) -> m_State = Attack
                        // else -> m_State = Idle
                        // m_TurnManager.Instance.m_SwitchCharacter = true;
                    }
                    break;
                }
            case BehaviorState.MoveToPlayer:
                {
                    if (m_State != BehaviorState.MoveToPlayer)
                    {
                        m_EnemyAgent.SetDestination(PlayerManager.Instance.m_Player.gameObject.transform.position);
                    }
                }
                break;
            case BehaviorState.Idle:
                {
                    if (m_State != BehaviorState.Idle)
                    {
                        // Reset Idle Anmiation
                    }
                }
                break;
        }
        m_State = i_State;
    }

    private void EndTurn()
    {
        m_IsPlaying = false;
        m_FinishTurn();        
    }

    public void PlayTurn()
    {
        m_IsPlaying = true;
    }

    public void TakeDamage(float aDamage)
    {
        m_CurrentHealth -= aDamage;
    }

    private void Die()
    {
        if(m_OnDeath != null)
        {
            m_OnDeath(this);
        }
        Destroy(gameObject);
    }

    // Les changements de couleurs et le changement du bool se font lorsque la zone d'attaque du joueur entre en collision avec les ennemis
    private void OnTriggerStay(Collider a_Other)
    {
        if (a_Other.gameObject.layer == LayerMask.NameToLayer("Interractible"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            m_Attackable = true;
        }
    }

    // Lorsque la zone d'attaque quitte l'ennemi, il reprend sa couleur d'avant
    private void OnTriggerExit(Collider a_Other)
    {
        if (a_Other.gameObject.layer == LayerMask.NameToLayer("Interractible"))
        {
            gameObject.GetComponent<Renderer>().material.color = m_EnemyMaterial.color;
            m_Attackable = false;
        }
    }
}

