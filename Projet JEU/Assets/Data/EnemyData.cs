using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data", fileName = "EnemyData", order = 1)]
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

    [Space(10)]
    [Header("Warrior Stats")]
    [Header("Warrior Strenght")]
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
    [Header("Warrior Dexterity")]
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
    [Header("Warrior Constitution")]
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

    [Space(10)]
    [Header("Tank Stats")]
    [Header("Tank Strenght")]
    [SerializeField]
    private float m_TankAttackDamage = 10;
    public float TankAttackDamage
    {
        get { return m_TankAttackDamage; }
    }

    [SerializeField]
    private float m_TankAttackDamagePerLevel = 5;
    public float TankAttackDamagePerLevel
    {
        get { return m_TankAttackDamagePerLevel; }
    }

    [Header("Tank Dexterity")]
    [SerializeField]
    private float m_TankMoveDistance = 0.9f;
    public float TankMoveDistance
    {
        get { return m_TankMoveDistance; }
    }

    [SerializeField]
    private float m_MoveDistancePerLevel = 0.1f;
    public float MoveDistancePerLevel
    {
        get { return m_MoveDistancePerLevel; }
    }

    [Header("Tank Constitution")]
    [SerializeField]
    private float m_TankMaxHealth = 100;
    public float TankMaxHealth
    {
        get { return m_TankMaxHealth; }
    }

    [SerializeField]
    private float m_TankHealthPerLevel = 5;
    public float TankHealthPerLevel
    {
        get { return m_TankHealthPerLevel; }
    }

    [Space(10)]
    [Header("Archer Stats")]
    [Header("Archer Dexterity")]
    [SerializeField]
    private float m_ArcherMoveDistance = 1.1f;
    public float ArcherMoveDistance
    {
        get { return m_ArcherMoveDistance; }
    }
    [SerializeField]
    private float m_ArcherMoveDistancePerLevel = 0.1f;
    public float ArcherMoveDistancePerLevel
    {
        get { return m_ArcherMoveDistancePerLevel; }
    }

    [Header("Archer Constitution")]
    [SerializeField]
    private float m_ArcherMaxHealth = 25;
    public float ArcherMaxHealth
    {
        get { return m_ArcherMaxHealth; }
    }

    [SerializeField]
    private float m_ArcherHealthPerLevel = 5;
    public float ArcherHealthPerLevel
    {
        get { return m_ArcherHealthPerLevel; }
    }

    [Header("Archer Perception")]
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

    [Header("Archer Range")]
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

//  Force	      Augmente les dégâts lors d’une attaque au corps à corps                                       DONE
//  Dextérité	  Augmente la chance d’éviter les attaques ennemies ET le déplacement du joueur                 % ?
//  Constitution  Augmente les points de vie du personnage                                                      DONE
//  Perception	  Augmente les dégâts avec l’arc à flèche                                                       DONE
//  Précision	  Augmente la chance de toucher l’ennemi lors d’une attaque à distance et en corps à corps      % ?
//  Portée	      Augmente la portée de l’arc à flèche                                                          DONE