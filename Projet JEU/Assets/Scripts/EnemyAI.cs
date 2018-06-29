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
    public bool m_IsPlaying = false;

    private BehaviorState m_State;
    private Vector3 m_InitialePos;
    private EnemyData m_EnemyData;


    private void Start()
    {
        m_State = BehaviorState.Idle;
        m_InitialePos = transform.position;
    }

    private bool CompareState(BehaviorState i_State)
    {
        return i_State == m_State;
    }

    private void Update()
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
            UpdatePatrol();
        }
        if (CompareState(BehaviorState.Attack))
        {
            UpdatePatrol();
        }
    }

    private void UpdateIdle()
    {
        if (m_IsPlaying)
        {
            if ((m_Player.transform.position - transform.position).distance < m_EnemyData.EnemyMeleeAttackRange)
            {
                ChangeState(BehaviorState.Attack);
            }
            else if ((m_Player.transform.position - transform.position).distance < m_EnemyData.EnemySight)
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

        if ((m_Player.transform.position - transform.position).distance < m_EnemyData.EnemySight)
        {
            ChangeState(BehaviorState.MoveToPlayer);
        }
        else
        {
            m_IsPlaying = false;
            ChangeState(BehaviorState.Idle);
        }
    }
    private void UpdateMovetoPlayer()
    {

        if ((m_Player.transform.position - transform.position).distance < m_EnemyData.EnemyMeleeAttackRange)
        {
            ChangeState(BehaviorState.Attack);
        }
        else
        {
            m_IsPlaying = false;
            ChangeState(BehaviorState.Idle);
        }
    }
    private void UpdateAttack()
    {
        // Animation d'attaque 
        m_IsPlaying = false;
        ChangeState(BehaviorState.Idle);
    }


    private void ChangeState(BehaviorState i_State)
    {
        switch (i_State)
        {
            case BehaviorState.Attack:
                {
                    if (m_State != BehaviorState.Attack)
                    {
                        // Attack();
                    }
                    break;
                }
            case BehaviorState.Patrol:
                {
                    if (m_State != BehaviorState.Patrol)
                    {
                        // Move to random position around the map
                    }
                    break;
                }
            case BehaviorState.MoveToPlayer:
                {
                    if (m_State != BehaviorState.MoveToPlayer)
                    {
                        // Move to Player
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
}

