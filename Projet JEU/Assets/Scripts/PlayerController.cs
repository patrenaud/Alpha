using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public bool m_EndTurn = false;
    public bool m_MeleeButtonIsPressed = false;
    public bool m_RangeButtonIsPressed = false;
    public Vector3 m_ScaleOfAttackZone;
    public Vector3 m_ScaleOfRangeAttackZone;

    [Header("Player Zones")]
    public GameObject m_MoveZone;
    public GameObject m_AttackZone;
    public GameObject m_RangeAttackZone;
    public Material m_PlayerMaterial;
    public Action m_FinishTurn;
    
    [SerializeField]
    private GameObject m_ProjectilePrefab;
    [SerializeField]
    private NavMeshAgent m_PlayerAgent;

    private Vector3 m_ScaleOfMoveZone;
    private Vector3 m_TargetPosition;

    private bool m_CanMove = false;
    private bool m_CanAttack = false;
    private bool m_CanAbility = false;
    private float m_BulletSpeed = 50f; 
   
    private void Awake()
    {
        PlayerManager.Instance.m_Player = this;
        m_MoveZone.SetActive(false);
    }

    private void Start()
    {
        PlayerManager.Instance.m_MainUI.StartingUI();
        SetZoneStats(); // Sets the attack zones (range of both melee and range)               
    }

    private void Update()
    {
        // Lorsque les bools sont activés par les boutons, les fonctions respectives sont appelées
        if (m_CanMove)
        {
            Move();
        }
        else if (m_CanAttack)
        {
            Attack();
        }
        else if (m_CanAbility)
        {
            Ability();
        }
        else if (m_EndTurn)
        {
            EndTurn();
        }
    }

    private void SetZoneStats()
    {
        m_ScaleOfAttackZone = m_AttackZone.transform.localScale * PlayerManager.Instance.m_PlayerData.MeleeAttackRange;
        m_ScaleOfRangeAttackZone = m_RangeAttackZone.transform.localScale * PlayerManager.Instance.m_PlayerData.RangeAttackRange;
        m_ScaleOfMoveZone.x = m_MoveZone.transform.localScale.x * PlayerManager.Instance.m_PlayerData.MoveDistance;
        m_ScaleOfMoveZone.z = m_MoveZone.transform.localScale.z * PlayerManager.Instance.m_PlayerData.MoveDistance;
        m_AttackZone.transform.localScale = Vector3.zero;
        m_RangeAttackZone.transform.localScale = Vector3.zero;
    }

    public void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hitinfo;

            if (Physics.Raycast(rayon, out Hitinfo, 500f, LayerMask.GetMask("Player")))
            {
                Debug.Log("Invalid Move"); // Le joueur ne peut pas se déplacer sur lui-même

            }

            else if (Physics.Raycast(rayon, out Hitinfo, 500f, LayerMask.GetMask("Enemy")))
            {
                if (Hitinfo.collider.gameObject.GetComponent<EnemyAI>().m_Attackable) // MIGHT NEED TO CHANGE
                {                    
                    AttackEnd(Hitinfo);
                }
            }

            else if (Physics.Raycast(rayon, out Hitinfo, 500f, LayerMask.GetMask("UI")))
            {
                // Si le joueur click à nouveau sur le bouton Move, il annule son mouvement.
                m_CanMove = false;
            }

            // Permet le move seulement si le click est dans la MoveZone et sur le Ground
            else if (Physics.Raycast(rayon, out Hitinfo, 500f, LayerMask.GetMask("Ground")) && Physics.Raycast(rayon, out Hitinfo, 500f, LayerMask.GetMask("MoveZone")))
            {
                m_TargetPosition.x = Hitinfo.point.x;
                m_TargetPosition.z = Hitinfo.point.z;
                m_TargetPosition.y = transform.position.y;

                MovetoPoint(Hitinfo);

                //m_CanMove = false;
                m_MoveZone.SetActive(false);
                PlayerManager.Instance.m_MainUI.DeactivateMove();
            }
        }
    }

    // Permet le déplacment du joueur 
    private void MovetoPoint(RaycastHit Hitinfo)
    {
        m_PlayerAgent.SetDestination(Hitinfo.point);
        // Old Code to turn before moving
        /* float Timer = 0f;
        {           
            transform.LookAt(m_TargetPosition);
            while (Vector3.Distance(m_TargetPosition, transform.position) > 0)
            {
                transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Timer);
                Timer += Time.deltaTime * m_MoveSpeed / 60;
                yield return new WaitForEndOfFrame();
            }
        }*/
    }

    // Permet l'attaque du joueur vers l'ennemi
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hitinfo;

            if (Physics.Raycast(rayon, out Hitinfo, 500f, LayerMask.GetMask("Enemy")))
            {
                if (Hitinfo.collider.gameObject.GetComponent<EnemyAI>().m_Attackable) 
                {
                    ShootProjectile(Hitinfo);
                    //ApplyDamage();
                    AttackEnd(Hitinfo);
                }
            }
            // Doit être niveau 2 du prototype pour attacker le Boss.
            else if (Physics.Raycast(rayon, out Hitinfo, 500f, LayerMask.GetMask("Boss")) && PlayerManager.Instance.m_RangeAttack)
            {
                // This part is the condition to defeat the Boss
                if (Hitinfo.collider.gameObject.GetComponent<EnemyAI>().m_Attackable) 
                {
                    AttackEnd(Hitinfo);
                    ShootProjectile(Hitinfo);
                }
            }
            else if (Physics.Raycast(rayon, out Hitinfo, 500f, LayerMask.GetMask("Boss")))
            {
                Debug.Log("Can't attack Boss yet");
            }
        }
    }

    private void ShootProjectile(RaycastHit i_Hitinfo)
    {
        //   position de l'ennemi          -       position du joueur - la moitié du scale de l'ennemi         .longueur   > rayon du préfab d'attaque
        if (Vector3.Distance(i_Hitinfo.collider.transform.position, transform.position) > m_ScaleOfAttackZone.x / 2 && PlayerManager.Instance.m_RangeAttack)
        {
            GameObject m_BulletInstance = Instantiate(m_ProjectilePrefab, transform.position, Quaternion.identity);
            Projectile script = m_BulletInstance.GetComponent<Projectile>();
            script.InitSpeed(m_BulletSpeed, (i_Hitinfo.collider.transform.position - transform.position).normalized);
        }
    }

    private void AttackEnd(RaycastHit i_Hitinfo)
    {
        // The Enemy takes Damage from distance or from melee
        if (Vector3.Distance(i_Hitinfo.collider.transform.position, transform.position) > m_ScaleOfAttackZone.x / 2 && PlayerManager.Instance.m_RangeAttack)
        {
            i_Hitinfo.collider.gameObject.GetComponent<EnemyAI>().TakeDamage(PlayerManager.Instance.PlayerRangeDamage());
        }
        else
        {
            i_Hitinfo.collider.gameObject.GetComponent<EnemyAI>().TakeDamage(PlayerManager.Instance.PlayerMeleeDamage());
        }

        DeactivateAttackZones();
        PlayerManager.Instance.m_MainUI.OnPlayerAttackEnd();

        m_CanAttack = false;
        m_MeleeButtonIsPressed = false;
        m_RangeButtonIsPressed = false;
    }


    public void Ability()
    {
        PlayerManager.Instance.m_MainUI.ActivateAbilityButtons();
    }

    public void EndTurn()
    {
        DeactivateAttackZones();

        // Les capsules sont détectées malgré leur scale de Vector3.zero. Il faut donc le désactiver entre les tours.
        m_AttackZone.GetComponent<CapsuleCollider>().enabled = false;
        m_MoveZone.GetComponent<CapsuleCollider>().enabled = false;
        m_RangeAttackZone.GetComponent<CapsuleCollider>().enabled = false;

        if (m_FinishTurn != null)
        {
            m_FinishTurn();
        }
    }

    // Désactive la zone d'attack et de range attack
    private void DeactivateAttackZones()
    {
        m_AttackZone.transform.localScale = Vector3.zero;
        m_RangeAttackZone.transform.localScale = Vector3.zero;
        PlayerManager.Instance.m_MainUI.DeactivateAttackChoice();
    }

    // Cette région permet aux boutons d'appaler ces fonctions. Les Booleens sont activés et permettent les Move/Attack/Ability/EndTurn
    #region Activatables
    public void ActivateMove()
    {
        if (m_CanMove)
        {
            m_MoveZone.SetActive(false);
            m_CanMove = false;
        }
        else if (!m_CanMove)
        {
            m_CanMove = true;
            m_MoveZone.SetActive(true);
        }
    }

    public void ActivateAttack()
    {
        if (!PlayerManager.Instance.m_RangeAttack)
        {
            if (m_CanAttack)
            {
                m_CanAttack = false;
                m_AttackZone.transform.localScale = Vector3.zero;
            }
            else if (!m_CanAttack)
            {
                m_CanAttack = true;
                m_AttackZone.transform.localScale = m_ScaleOfAttackZone;
            }
        }
        else
        {
            if (m_CanAttack)
            {
                m_CanAttack = false;
                PlayerManager.Instance.m_MainUI.DeactivateAttackChoice();
            }
            else if (!m_CanAttack)
            {
                m_CanAttack = true;
                PlayerManager.Instance.m_MainUI.ActivateAttackChoice();
            }
        }
    }



    public void ActivateHabilty()
    {
        if (m_CanAbility)
        {
            m_CanAbility = false;
            PlayerManager.Instance.m_MainUI.DeactivateAbilityButtons();
        }
        else if (!m_CanAbility)
        {
            m_CanAbility = true;
            PlayerManager.Instance.m_MainUI.ActivateAbilityButtons();
        }
    }

    public void ActivateAbility1()
    {
        // Has to be filled with Abilities
    }

    public void ActivateAbility2()
    {
        // Has to be filled with Abilities
    }

    public void ActivateAbility3()
    {
        // Has to be filled with Abilities
    }

    public void ActivateAbility4()
    {
        PlayerManager.Instance.m_MainUI.OnActivateAbility4(PlayerManager.Instance.m_HealthRegenAbility);
    }
    #endregion
    

    // Lors de la fin du tour des ennemies, le UI des boutons et des zones sont Reset
    public void ActivateActions()
    {
        m_CanAbility = false;
        m_CanAttack = false;
        m_CanMove = false;
        m_AttackZone.GetComponent<CapsuleCollider>().enabled = true;
        m_MoveZone.GetComponent<CapsuleCollider>().enabled = true;
        m_RangeAttackZone.GetComponent<CapsuleCollider>().enabled = true;
    }
}
