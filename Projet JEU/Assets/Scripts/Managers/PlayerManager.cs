using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public MainUI m_MainUI;
    public PlayerController m_Player;

    [HideInInspector]
    public float m_CurrentHealth;
    public float m_MaxHealth;
    public string m_PlayerPath = "Assets/Prefabs/Player";
    public PlayerData m_PlayerData;
    public float m_HealthRegenAbility;
    public bool m_RangeAttack = false;
    public bool m_PlayerDied = false;

    [HideInInspector]
    public int m_PlayerStrenght = 0;
    [HideInInspector]
    public int m_PlayerDexterity = 0;
    [HideInInspector]
    public int m_PlayerConstitution = 0;
    [HideInInspector]
    public int m_PlayerPerception = 0;
    [HideInInspector]
    public int m_PlayerPrecision = 0;
    [HideInInspector]
    public int m_PlayerRange = 0;

    public bool m_ActivateAvility1 = false;
    public bool m_ActivateAvility2 = false;
    public bool m_ActivateAvility3 = false;
    public bool m_ActivateAvility4 = false;

    //private float m_MoveSpeed;

    private static PlayerManager m_Instance;
    public static PlayerManager Instance
    {
        get { return m_Instance; }
    }

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //m_MoveSpeed = m_PlayerData.MoveSpeed;
        m_MaxHealth = m_PlayerData.MaxHealth;
        m_CurrentHealth = m_MaxHealth;
        m_HealthRegenAbility = m_PlayerData.HealthRegenAbility;
        m_MainUI.StartHpAndExp();

    }

    private void Update()
    {
        if (m_MainUI.m_XpBar.value >= 1)
        {
            m_MainUI.m_LevelUpButton.gameObject.SetActive(true);
        }

        if (Instance.m_MainUI.m_HealthBar.value <= 0)
        {
            Instance.m_MainUI.m_HealthBar.value = 1;
            m_PlayerDied = true;
            LevelManager.Instance.ChangeLevel("Results"); // THIS IS WHERE THE PLAYER DIES
            
        }
    }

    public float PlayerMeleeDamage()
    {
        float Damage = m_PlayerData.AttackDamage + (m_PlayerData.AttackDamagePerLevel * m_PlayerStrenght);
        return Damage;
    }

    public float PlayerRangeDamage()
    {
        float Damage = m_PlayerData.RangeAttackDamage + m_PlayerData.RangeAttackDamagePerLevel * m_PlayerPerception;
        return Damage;
    }

    public float PlayerMoveDistanceMultiplier()
    {
        float Distance = m_PlayerData.MoveDistance + m_PlayerData.MoveDistancePerLevel * m_PlayerDexterity;
        return Distance;
    }

    public float PlayerRangeMultiplier()
    {
        float Range = m_PlayerData.RangeAttackRange + m_PlayerData.RangeAttackRangePerLevel * m_PlayerRange;
        return Range;
    }

    public float PlayerHP()
    {
        float HP = m_PlayerData.MaxHealth + m_PlayerData.HealthPerLevel * m_PlayerConstitution;
        return HP;
    }

    public void TakeDamage(float aDamage)
    {
        m_CurrentHealth -= aDamage;
        m_MainUI.m_HealthBar.value = m_CurrentHealth / m_PlayerData.MaxHealth;
    }

    public void LevelUp()
    {
        m_MainUI.m_UpgradeCanvas.gameObject.SetActive(true);
    }

    public void ActivateMeleeAttack()
    {
        m_Player.m_MeleeButtonIsPressed = !m_Player.m_MeleeButtonIsPressed;
        // Operateur ternaire determine le scale de la zone du MeleeAttack lorsque l'on appui sur le bouton Attack
        m_Player.m_AttackZone.transform.localScale = m_Player.m_MeleeButtonIsPressed ? m_Player.m_ScaleOfAttackZone : Vector3.zero;
    }

    public void ActivateRangeAttack()
    {
        m_Player.m_RangeButtonIsPressed = !m_Player.m_RangeButtonIsPressed;
        // Operateur ternaire determine le scale de la zone ddu RangeAttack lorsque l'on appui sur le bouton Attack
        m_Player.m_RangeAttackZone.transform.localScale = m_Player.m_RangeButtonIsPressed ? m_Player.m_ScaleOfRangeAttackZone : Vector3.zero;
    }

    public void ActivateAbility1()
    {
        m_ActivateAvility1 = true;
    }

    public void ActivateAbility2()
    {
        m_ActivateAvility2 = true;
    }

    public void ActivateAbility3()
    {
        m_ActivateAvility3 = true;
    }

    public void ActivateAbility4()
    {
        m_ActivateAvility4 = true;
    }
}
