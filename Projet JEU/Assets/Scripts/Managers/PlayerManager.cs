using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float m_XPGainedPerKill = 1;

    [HideInInspector]
    public int m_PlayerStrenght = 1;
    [HideInInspector]
    public int m_PlayerDexterity = 1;
    [HideInInspector]
    public int m_PlayerConstitution = 1;
    [HideInInspector]
    public int m_PlayerPerception = 1;
    [HideInInspector]
    public int m_PlayerPrecision = 1;
    [HideInInspector]
    public int m_PlayerRange = 1;

    public bool m_ActivateAbility1 = false;
    public bool m_ActivateAbility2 = false;
    public bool m_ActivateAbility3 = false;
    public bool m_ActivateAbility4 = false;



    // Cheat variables for Cheat UI
    private bool m_CheatsActivated = false;
    [SerializeField]
    private GameObject m_CheatCanvas;
    [SerializeField]
    private Text m_InfiniteHPText;
    private bool m_HPCheat = false;
    [SerializeField]
    private Text m_InfiniteAbilitiesText;
    public bool m_AbilityCheat = false;
    [SerializeField]
    private Text m_InfiniteMoveText;
    public bool m_MoveCheat = false;
    [SerializeField]
    private Text m_InfiniteAttacksText;
    public bool m_AttackCheat = false;
    


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

#if UNITY_CHEATS
        m_CheatCanvas.SetActive(true);
        m_CheatsActivated = true;
#endif
#if !UNITY_CHEATS
        m_CheatCanvas.SetActive(false);
        m_CheatsActivated = false;
#endif

    }

    private void Update()
    {
        // When exp bar is filled, level up is activated
        if (m_MainUI.m_XpBar.value >= 1)
        {
            m_MainUI.m_LevelUpButton.gameObject.SetActive(true);
        }

        // THIS IS WHERE THE PLAYER DIES
        if (Instance.m_MainUI.m_HealthBar.value <= 0)
        {
            Instance.m_MainUI.m_HealthBar.value = 1;
            m_PlayerDied = true;
            m_Player.m_Animator.SetTrigger("Die");

            StartCoroutine(DeathDelay());        
        }


#if UNITY_CHEATS
        if(Input.GetKeyDown(KeyCode.C))
        {
            m_CheatsActivated = !m_CheatsActivated;
            m_CheatCanvas.SetActive(m_CheatsActivated);
        }
        
        if(m_CheatsActivated)
        {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LevelUp();
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            if(!m_HPCheat)
            {
                m_InfiniteHPText.text = "ON";
                m_HPCheat = !m_HPCheat;
            }
            else if (m_HPCheat)
            {
                m_InfiniteHPText.text = "OFF";
                m_HPCheat = !m_HPCheat;
            }
            
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            if(!m_AbilityCheat)
            {
                m_InfiniteAbilitiesText.text = "ON";
                m_AbilityCheat = !m_AbilityCheat;
            }
            else if (m_AbilityCheat)
            {
                m_InfiniteAbilitiesText.text = "OFF";
                m_AbilityCheat = !m_AbilityCheat;
            }
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(!m_MoveCheat)
            {
                m_InfiniteMoveText.text = "ON";
                m_MoveCheat = !m_MoveCheat;
            }
            else if (m_MoveCheat)
            {
                m_InfiniteMoveText.text = "OFF";
                m_MoveCheat = !m_MoveCheat;
            }
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            if(!m_AttackCheat)
            {
                m_InfiniteAttacksText.text = "ON";
                m_AttackCheat = !m_AttackCheat;
            }
            else if (m_AttackCheat)
            {
                m_InfiniteAttacksText.text = "OFF";
                m_AttackCheat = !m_AttackCheat;
            }
        }
        }
#endif

    }
    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2.5f);
        LevelManager.Instance.ChangeLevel("Results");
    }

    public float PlayerMeleeDamage()
    {
        float Damage = m_PlayerData.AttackDamage + (m_PlayerData.AttackDamagePerLevel * m_PlayerStrenght);
        if (m_Player.m_ExtremeForce)
        {
            Damage += 30;
        }
        return Damage;
    }

    public float PlayerRangeDamage()
    {
        float Damage = m_PlayerData.RangeAttackDamage + m_PlayerData.RangeAttackDamagePerLevel * m_PlayerPerception;
        if (m_Player.m_ExtremeForce)
        {
            Damage += 30;
        }
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
#if UNITY_CHEATS
        if(m_HPCheat)
        {
            m_CurrentHealth += aDamage;
            m_MainUI.m_HealthBar.value = m_PlayerData.MaxHealth;
        }
#endif
    }

    public void ResetHealth()
    {
        m_CurrentHealth = m_MaxHealth;
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

    public void SetAnimator()
    {
        m_Player.m_Animator.SetTrigger("Idle");
    }

    public void SetWalkAnim()
    {
        m_Player.m_Animator.SetTrigger("Walk");
        StartCoroutine(EndWalkAnim());
    }

    private IEnumerator EndWalkAnim()
    {
        yield return new WaitForSeconds(1.8f);
        m_Player.m_Animator.SetTrigger("Idle");
    }

    public void SetAttackAnim()
    {

        m_Player.m_Animator.SetTrigger("Attack");

    }

    public void ActivateAbility1()
    {
        m_ActivateAbility1 = true;
        m_MainUI.m_Ability1.interactable = true;
    }

    public void ActivateAbility2()
    {
        m_ActivateAbility2 = true;
        m_MainUI.m_Ability2.interactable = true;
    }

    public void ActivateAbility3()
    {
        m_ActivateAbility3 = true;
        m_MainUI.m_Ability3.interactable = true;
    }

    public void ActivateAbility4()
    {
        m_ActivateAbility4 = true;
        m_MainUI.m_Ability4.interactable = true;

    }
}
