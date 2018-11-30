using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum BehaviorState
{
    Idle,
    Patrol,
    MoveToPlayer,
    Attack
}

public class EnemyAI : MonoBehaviour
{
    private BehaviorState m_State;
    [SerializeField]
    private EnemyData m_EnemyData;
    [SerializeField]
    private Slider m_HealthBar;

    private float m_CurrentTime = 0;
    public float m_CurrentHealth;
    [SerializeField]
    private int m_RootRoundCountdown = 4;
    private NavMeshAgent m_EnemyAgent;

    private Vector3 m_ScaleOfAttackZone;
    private Vector3 m_ScaleOfRangeAttackZone;
    private Vector3 m_InitialePos;
    [SerializeField]
    private Text m_Text;
    private bool m_DestinationReached = false;
    private bool m_Rooted = false;
    private bool m_IsDead;

    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private GameObject m_RingOfFire;

    public Action m_FinishTurn;
    public Action<EnemyAI> m_OnDeath;

    public GameObject m_AttackZone;
    public GameObject m_RangeAttackZone;

    // Player accesses these when collided with player attack zones
    public bool m_Attackable = false;
    public bool m_IsPlaying = false;

    public Transform m_PatrolDestination;
    public Image m_SpellActive;
    public Image m_Targetable;

    private void Start()
    {
        m_Targetable.enabled = false;
        m_SpellActive.enabled = false;
        m_Text.enabled = false;
        m_IsDead = false;

        // Position
        m_State = BehaviorState.Idle;
        m_InitialePos = transform.position;
        m_EnemyAgent = GetComponent<NavMeshAgent>();

        // Health
        m_CurrentHealth = m_EnemyData.EnemyMaxHealth;
        m_HealthBar.value = 1;
        m_HealthBar.gameObject.SetActive(false);

        SetZoneStats();
        m_Animator.SetTrigger("Idle");

        // Starting pos feedback
        GameObject Ring = Instantiate(m_RingOfFire, transform, false);
        StartCoroutine(EndRing(Ring));
    }

    private IEnumerator EndRing(GameObject ring)
    {
        yield return new WaitForSeconds(4.0f);
        ring.SetActive(false);
    }

    private void SetZoneStats()
    {
        // Gets proper scale for zones depending on stats
        m_ScaleOfAttackZone = m_AttackZone.transform.localScale * m_EnemyData.EnemyMeleeAttackRange;
        m_ScaleOfRangeAttackZone = m_RangeAttackZone.transform.localScale * m_EnemyData.EnemyRange;

        m_AttackZone.transform.localScale = Vector3.zero;
        m_RangeAttackZone.transform.localScale = Vector3.zero;
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
        if (m_CurrentHealth <= 0 && m_IsDead == false)
        {
            m_IsDead = true;
            Die();
        }
    }

    private void UpdateIdle()
    {
        if (m_IsPlaying)
        {
            // if the enemy is rooted, at the beginning of the turn it takes damage and creates feedback
            if (m_Rooted)
            {
                m_RootRoundCountdown -= 1;
                TakeDamage(10f);
                if (m_RootRoundCountdown >= 1)
                {
                    StartCoroutine(RootedText());
                }                

                if (m_RootRoundCountdown <= 0)
                {
                    m_Rooted = false;
                    m_SpellActive.enabled = false;
                }
                else
                {
                    EndTurn();
                }
            }

            else if ((Vector3.Distance(PlayerManager.Instance.m_Player.transform.position, transform.position) < m_ScaleOfAttackZone.magnitude * 0.5f))
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
        m_CurrentTime += Time.deltaTime;

        // This if/else is for the patrolling movement of the enemy path.
        if (!m_DestinationReached)
        {
            m_EnemyAgent.SetDestination(m_PatrolDestination.position);

            if (Vector3.Distance(m_EnemyAgent.destination, m_EnemyAgent.transform.position) < 1f)
            {
                m_DestinationReached = true;
            }
        }
        else if (m_DestinationReached)
        {
            m_EnemyAgent.SetDestination(m_InitialePos);

            if (Vector3.Distance(m_EnemyAgent.destination, m_EnemyAgent.transform.position) < 1f)
            {
                m_DestinationReached = false;
            }
        }

        if (Vector3.Distance(PlayerManager.Instance.m_Player.gameObject.transform.position, transform.position) < m_EnemyData.EnemySight)
        {
            ChangeState(BehaviorState.MoveToPlayer);
        }

        if (m_CurrentTime >= 1.5f)
        {
            m_EnemyAgent.SetDestination(transform.position);
            m_CurrentTime = 0;
            
            EndTurn();
        }
    }

    private void UpdateMovetoPlayer()
    {
        m_EnemyAgent.SetDestination(PlayerManager.Instance.m_Player.gameObject.transform.position);
        m_CurrentTime += Time.deltaTime;

        if ((Vector3.Distance(PlayerManager.Instance.m_Player.transform.position, transform.position) < m_ScaleOfAttackZone.magnitude * 0.5f))
        {
            m_EnemyAgent.SetDestination(transform.position);
            m_CurrentTime = 0;
            ChangeState(BehaviorState.Attack);
        }

        else if ((Vector3.Distance(PlayerManager.Instance.m_Player.transform.position, transform.position) < m_ScaleOfRangeAttackZone.magnitude * 0.4f)
            && gameObject.CompareTag("Archer"))
        {
            m_EnemyAgent.SetDestination(transform.position);
            m_CurrentTime = 0;
            ChangeState(BehaviorState.Attack);
        }

        if (m_CurrentTime >= 1.5f)
        {
            m_EnemyAgent.SetDestination(transform.position);
            m_CurrentTime = 0;
                        
            EndTurn();
        }
    }

    private void UpdateAttack()
    {
        if (gameObject.CompareTag("Archer"))
        {
            m_IsPlaying = false;
            StartCoroutine(RangeAttack());
        }
        else if (gameObject.CompareTag("Warrior") || (gameObject.CompareTag("Tank")))
        {
            m_IsPlaying = false;
            StartCoroutine(MeleeAttack());            
        }        

        ChangeState(BehaviorState.Idle);
    }

    private IEnumerator RangeAttack()
    {
        // This is to fit attack anim
        yield return new WaitForSeconds(1.7f);
        PlayerManager.Instance.TakeDamage(m_EnemyData.EnemyRangeDamage);
        EndTurn();
    }

    private IEnumerator MeleeAttack()
    {
        // This is to fit attack anim
        yield return new WaitForSeconds(1.7f);
        PlayerManager.Instance.TakeDamage(m_EnemyData.MeleeAttackDamage);
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
                        // Activate Anim for all different enemies           
                        m_Animator.SetTrigger("Attack");
                    }
                    break;
                }
            case BehaviorState.Patrol:
                {
                    if (m_State != BehaviorState.Patrol)
                    {
                        // Activate Zone as big as sight range for feedback *** TODO
                        m_Animator.SetTrigger("Walk");
                    }
                    break;
                }
            case BehaviorState.MoveToPlayer:
                {
                    if (m_State != BehaviorState.MoveToPlayer)
                    {
                        m_EnemyAgent.SetDestination(PlayerManager.Instance.m_Player.gameObject.transform.position);
                        m_Animator.SetTrigger("Walk");
                    }
                }
                break;
            case BehaviorState.Idle:
                {
                    if (m_State != BehaviorState.Idle)
                    {
                        m_Animator.SetTrigger("Idle");                     
                    }
                }
                break;
        }
        m_State = i_State;
    }

    private void EndTurn()
    {
        m_IsPlaying = false;
        if (gameObject.CompareTag("Archer"))
        {
            m_RangeAttackZone.transform.localScale = Vector3.zero;
        }
        else if (gameObject.CompareTag("Warrior") || (gameObject.CompareTag("Tank")))
        {
            m_AttackZone.transform.localScale = Vector3.zero;
        }
        m_State = BehaviorState.Idle;
        m_Animator.SetTrigger("Idle");
        m_FinishTurn();        
    }

    public void PlayTurn()
    {
        m_IsPlaying = true;
        
        m_AttackZone.transform.localScale = Vector3.zero;
        m_RangeAttackZone.transform.localScale = Vector3.zero;
        if (gameObject.CompareTag("Archer"))
        {
            m_RangeAttackZone.transform.localScale = m_ScaleOfRangeAttackZone;
        }
        else if (gameObject.CompareTag("Warrior") || (gameObject.CompareTag("Tank")))
        {
            m_AttackZone.transform.localScale = m_ScaleOfAttackZone;
        }
    }

    public void TakeDamage(float aDamage)
    {
        if (m_HealthBar.value == 1)
        {
            m_HealthBar.gameObject.SetActive(true);
        }
        m_CurrentHealth -= aDamage;
        m_Animator.SetTrigger("Hit");
        m_HealthBar.value = m_CurrentHealth / m_EnemyData.EnemyMaxHealth;
        m_Animator.SetTrigger("Idle");
    }

    private IEnumerator RootedText()
    {
        m_Text.enabled = true;
        yield return new WaitForSeconds(1);
        m_Text.enabled = false;
    }

    // This is so Camera can get enemy Position
    public Transform SetCameraFocus()
    {
        return transform;
    }

    private void Die()
    {
        if (m_OnDeath != null && m_IsDead)
        {
            m_Animator.SetTrigger("Die");
            AudioManager.Instance.PlaySFX(AudioManager.Instance.m_SoundList[2], transform.position);
            StartCoroutine(DestroyEnemy());
        }
    }    

    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(2.5f);
        m_OnDeath(this);
        Destroy(gameObject);
    }

    public void Rooted()
    {
        m_Rooted = true;
    }

    private void OnTriggerEnter(Collider a_Other)
    {
        m_Attackable = true;
    }


    private void OnTriggerExit(Collider a_Other)
    {
        m_Attackable = false;
    }
}

