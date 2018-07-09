﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    public TurnManager m_Turnmanager;
    [HideInInspector]
    public bool m_CanMove = false;
    [HideInInspector]
    public bool m_CanAttack = false;
    [HideInInspector]
    public bool m_CanAbility = false;
    [HideInInspector]
    public bool m_EndTurn = false;
    public bool m_RangeAttack = false;

    [HideInInspector]
    public float m_CurrentHealth;
    public float m_MaxHealth;

    [Header("Player Zones")]
    public GameObject m_MoveZone;
    public GameObject m_AttackZone;
    public GameObject m_RangeAttackZone;
    public Material m_PlayerMaterial;

    [SerializeField]
    private PlayerData m_PlayerData;
    [SerializeField]
    private GameObject m_ProjectilePrefab;
    [SerializeField]
    private NavMeshAgent m_PlayerAgent;
    private Vector3 m_ScaleOfAttackZone;
    private Vector3 m_ScaleOfRangeAttackZone;
    // private Vector3 m_ScaleOfMoveZone;
    private Vector3 m_TargetPosition;
    private bool m_MeleeButtonIsPressed = false;
    private bool m_RangeButtonIsPressed = false;
    private float m_BulletSpeed = 50f;
    private float m_MoveSpeed;

    private float m_HealthRegenAbility;

    private void Awake()
    {
        PlayerManager.Instance.m_Player = this;
        m_MoveZone.SetActive(false);
    }

    private void Start()
    {
        TurnManager.Instance.m_MainUI.StartingUI();

        m_ScaleOfAttackZone = m_AttackZone.transform.localScale * m_PlayerData.MeleeAttackRange;
        m_ScaleOfRangeAttackZone = m_RangeAttackZone.transform.localScale * m_PlayerData.RangeAttackRange;
        // m_ScaleOfMoveZone = m_MoveZone.transform.localScale * m_PlayerData.MoveDistance;
        m_AttackZone.transform.localScale = Vector3.zero;
        m_RangeAttackZone.transform.localScale = Vector3.zero;

        m_MoveSpeed = m_PlayerData.MoveSpeed;
        m_MaxHealth = m_PlayerData.MaxHealth;
        m_HealthRegenAbility = m_PlayerData.HealthRegenAbility;

        m_CurrentHealth = m_MaxHealth;
        TurnManager.Instance.m_MainUI.m_HealthBar.value = 1;
        TurnManager.Instance.m_MainUI.m_XpBar.value = 0f;
        PlayerManager.Instance.m_Player = this;
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

        if (TurnManager.Instance.m_MainUI.m_XpBar.value >= 1)
        {
            TurnManager.Instance.m_MainUI.m_LevelUpButton.gameObject.SetActive(true);
        }

        if (TurnManager.Instance.m_MainUI.m_HealthBar.value <= 0)
        {
            LevelManager.Instance.ChangeLevel("Main");
        }
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
                if (Hitinfo.collider.gameObject.GetComponent<EnemyController>().m_Attackable)
                {
                    //ApplyDamage(); // Le joueur peut attaquer un ennemi dans sa zone de move seulement si elle est dans la zone d'attaque aussi
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

                m_CanMove = false;
                TurnManager.Instance.m_MainUI.m_MoveButton.interactable = false;
                m_MoveZone.SetActive(false);

            }
        }
    }

    // Permet le déplacment du joueur 
    private void MovetoPoint(RaycastHit Hitinfo)
    {
        m_PlayerAgent.SetDestination(Hitinfo.point);
        // Old Code
        /* float Timer = 0f;
        {
            // ****** Ce céplacement devra être changé par le NavMesh. Si le click est hors zone, ne confirmation sera demandée pour la distance max
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
                if (Hitinfo.collider.gameObject.GetComponent<EnemyController>().m_Attackable)
                {
                    ShootProjectile(Hitinfo);
                    //ApplyDamage();
                    AttackEnd(Hitinfo);
                }
            }
            // Doit être niveau 2 du prototype pour attacker le Boss.
            else if (Physics.Raycast(rayon, out Hitinfo, 500f, LayerMask.GetMask("Boss")) && m_RangeAttack)
            {
                // This part is the condition to defeat the Boss
                if (Hitinfo.collider.gameObject.GetComponent<EnemyController>().m_Attackable)
                {
                    ShootProjectile(Hitinfo);
                    //ApplyDamage();
                    AttackEnd(Hitinfo);
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
        if ((i_Hitinfo.collider.transform.position - transform.position - i_Hitinfo.collider.transform.localScale).magnitude > m_ScaleOfAttackZone.x / 2 && m_RangeAttack)
        {
            GameObject m_BulletInstance = Instantiate(m_ProjectilePrefab, transform.position, Quaternion.identity);
            Projectile script = m_BulletInstance.GetComponent<Projectile>();
            script.InitSpeed(m_BulletSpeed, (i_Hitinfo.collider.transform.position - transform.position).normalized);
        }
    }

    private void AttackEnd(RaycastHit i_Hitinfo)
    {
        StartCoroutine(DestroyEnemy(i_Hitinfo)); // This is to call Death Animation for Enemies

        TurnManager.Instance.m_MainUI.m_XpBar.value += 0.40f;

        m_CanAttack = false;
        TurnManager.Instance.m_MainUI.m_AttackButton.interactable = false;
        m_AttackZone.transform.localScale = Vector3.zero;
        m_RangeAttackZone.transform.localScale = Vector3.zero;
        TurnManager.Instance.m_MainUI.m_MeleeAttackButton.gameObject.SetActive(false);
        TurnManager.Instance.m_MainUI.m_RangeAttackButton.gameObject.SetActive(false);
        m_MeleeButtonIsPressed = false;
        m_RangeButtonIsPressed = false;
    }

    // Ceci est pour un feedback de la mort de l'ennemi
    private IEnumerator DestroyEnemy(RaycastHit i_Hitinfo)
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 9; i > 0; i--)
        {
            i_Hitinfo.collider.gameObject.transform.localScale -= new Vector3(0.1F, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        m_Turnmanager.m_Characters.Remove(i_Hitinfo.collider.gameObject);
        Destroy(i_Hitinfo.collider.gameObject);
    }

    public void ApplyDamage(float i_AttackDamage)
    {
        // Place to Apply damage
        m_CurrentHealth -= i_AttackDamage;
        TurnManager.Instance.m_MainUI.m_HealthBar.value = m_CurrentHealth / m_MaxHealth;

        StartCoroutine(ApplyDamageFeedback());
    }

    private IEnumerator ApplyDamageFeedback()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Renderer>().material.color = m_PlayerMaterial.color;
    }

    public void Ability()
    {
        TurnManager.Instance.m_MainUI.ActivateAbilityButtons();
    }

    public void EndTurn()
    {
        // Ici on reset les buttons du joueur
        TurnManager.Instance.m_MainUI.DeactivateUI();

        // Les capsules sont détectées malgré leur scale de Vector3.zero. Il faut donc le désactiver entre les tours.
        m_AttackZone.transform.localScale = Vector3.zero;
        m_RangeAttackZone.transform.localScale = Vector3.zero;
        m_AttackZone.GetComponent<CapsuleCollider>().enabled = false;
        m_MoveZone.GetComponent<CapsuleCollider>().enabled = false;
        m_RangeAttackZone.GetComponent<CapsuleCollider>().enabled = false;
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
        if (!m_RangeAttack)
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
                TurnManager.Instance.m_MainUI.DeactivateAttackChoice();
            }
            else if (!m_CanAttack)
            {
                m_CanAttack = true;
                TurnManager.Instance.m_MainUI.ActivateAttackChoice();
            }
        }
    }

    public void ActivateMeleeAttack()
    {
        m_MeleeButtonIsPressed = !m_MeleeButtonIsPressed;
        // Operateur ternaire determine le scale de la zone du MeleeAttack lorsque l'on appui sur le bouton Attack
        m_AttackZone.transform.localScale = m_MeleeButtonIsPressed ? m_ScaleOfAttackZone : Vector3.zero;
    }

    public void ActivateRangeAttack()
    {
        m_RangeButtonIsPressed = !m_RangeButtonIsPressed;
        // Operateur ternaire determine le scale de la zone ddu RangeAttack lorsque l'on appui sur le bouton Attack
        m_RangeAttackZone.transform.localScale = m_RangeButtonIsPressed ? m_ScaleOfRangeAttackZone : Vector3.zero;
    }

    public void ActivateHabilty()
    {
        if (m_CanAbility)
        {
            m_CanAbility = false;
            TurnManager.Instance.m_MainUI.DeactivateAbilityButtons();            
        }
        else if (!m_CanAbility)
        {
            m_CanAbility = true;
            TurnManager.Instance.m_MainUI.ActivateAbilityButtons();
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
        TurnManager.Instance.m_MainUI.m_HealthBar.value += m_HealthRegenAbility;
        TurnManager.Instance.m_MainUI.m_Ability4.interactable = false;
    }
    #endregion

    public void LevelUp()
    {
        TurnManager.Instance.m_MainUI.m_UpgradeCanvas.gameObject.SetActive(true);
    }
}
