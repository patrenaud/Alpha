using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerController m_Player;

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

    [SerializeField]
    private PlayerData m_PlayerData;

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

    public float PlayerMeleeDamage()
    {
        float Damage = m_PlayerData.AttackDamage + m_PlayerData.AttackDamagePerLevel * m_PlayerStrenght;
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
}
