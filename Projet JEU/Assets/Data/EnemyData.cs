using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyData", fileName = "EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{

    [Header("Base Enemy Stats")]
    [SerializeField]
    private float m_EnemyMeleeAttackRange = 1;
    public float EnemyMeleeAttackRange
    {
        get { return m_EnemyMeleeAttackRange; }
    }

    [SerializeField]
    private float m_EnemyMoveSpeed = 1f;
    public float EnemyMoveSpeed
    {
        get { return m_EnemyMoveSpeed; }
    }

    [SerializeField]
    private float m_EnemyMoveDistance = 8f;
    public float EnemyMoveDistance
    {
        get { return m_EnemyMoveDistance; }
    }

    [SerializeField]
    private float m_EnemySight = 20f;
    public float EnemySight
    {
        get { return m_EnemySight; }
    }

    [Header("Enemy Strenght")]
    [SerializeField]
    private float m_MeleeAttackDamage = 20;
    public float MeleeAttackDamage
    {
        get { return m_MeleeAttackDamage; }
    }
    [SerializeField]
    private float m_MeleeAttackDamagePerLevel = 5;
    public float MeleeAttackDamagePerLevel
    {
        get { return m_MeleeAttackDamagePerLevel; }
    }
    [Header("Enemy Dexterity")]
    [SerializeField]
    private float m_MoveDistance = 1.1f;
    public float MoveDistance
    {
        get { return m_MoveDistance; }
    }
    [SerializeField]
    private float m_MoveDistancePerLevel = 0.1f;
    public float MoveDistancePerLevel
    {
        get { return m_MoveDistancePerLevel; }
    }
    [Header("Enemy Constitution")]
    [SerializeField]
    private float m_EnemyMaxHealth = 50;
    public float EnemyMaxHealth
    {
        get { return m_EnemyMaxHealth; }
    }
    [SerializeField]
    private float m_EnemyHealthPerLevel = 5;
    public float EnemyHealthPerLevel
    {
        get { return m_EnemyHealthPerLevel; }
    }

    [Header("Enemy Perception")]
    [SerializeField]
    private float m_EnemyRangeDamage = 15;
    public float EnemyRangeDamage
    {
        get { return m_EnemyRangeDamage; }
    }

    [SerializeField]
    private float m_EnemyRangeDamagePerLevel = 5;
    public float EnemyRangeDamagePerLevel
    {
        get { return m_EnemyRangeDamagePerLevel; }
    }

    [Header("Enemy Range")]
    [SerializeField]
    private float m_EnemyRange = 1;
    public float EnemyRange
    {
        get { return m_EnemyRange; }
    }

    [SerializeField]
    private float m_ArcherRangePerLevel = 0.05f;
    public float ArcherRangePerLevel
    {
        get { return m_ArcherRangePerLevel; }
    }
}
