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
    private float m_EnemySight = 15f;
    public float EnemySight
    {
        get { return m_EnemySight; }
    }

    [Header("Enemy Strenght")]
    [SerializeField]
    private float m_WarriorAttackDamage = 20;
    public float WarriorAttackDamage
    {
        get { return m_WarriorAttackDamage; }
    }
    [SerializeField]
    private float m_WarriorAttackDamagePerLevel = 5;
    public float WarriorAttackDamagePerLevel
    {
        get { return m_WarriorAttackDamagePerLevel; }
    }
    [Header("Enemy Dexterity")]
    [SerializeField]
    private float m_WarriorMoveDistance = 1.1f;
    public float WarriorMoveDistance
    {
        get { return m_WarriorMoveDistance; }
    }
    [SerializeField]
    private float m_WarriorMoveDistancePerLevel = 0.1f;
    public float WarriorMoveDistancePerLevel
    {
        get { return m_WarriorMoveDistancePerLevel; }
    }
    [Header("Enemy Constitution")]
    [SerializeField]
    private float m_WarriorMaxHealth = 50;
    public float WarriorMaxHealth
    {
        get { return m_WarriorMaxHealth; }
    }
    [SerializeField]
    private float m_WarriorHealthPerLevel = 5;
    public float WarriorHealthPerLevel
    {
        get { return m_WarriorHealthPerLevel; }
    }

    [Header("Enemy Perception")]
    [SerializeField]
    private float m_ArcherDamage = 15;
    public float ArcherDamage
    {
        get { return m_ArcherDamage; }
    }

    [SerializeField]
    private float m_ArcherDamagePerLevel = 5;
    public float ArcherDamagePerLevel
    {
        get { return m_ArcherDamagePerLevel; }
    }

    [Header("Enemy Range")]
    [SerializeField]
    private float m_ArcherRange = 1;
    public float ArcherRange
    {
        get { return m_ArcherRange; }
    }

    [SerializeField]
    private float m_ArcherRangePerLevel = 0.05f;
    public float ArcherRangePerLevel
    {
        get { return m_ArcherRangePerLevel; }
    }
}
