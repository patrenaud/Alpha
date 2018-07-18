using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data", fileName = "PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Base Player Stats")]
    [SerializeField]
    private float m_MeleeAttackRange = 1;
    public float MeleeAttackRange
    {
        get { return m_MeleeAttackRange; }
    }

    [SerializeField]
    private float m_MoveSpeed = 0.5f;
    public float MoveSpeed
    {
        get { return m_MoveSpeed; }
    }

    [SerializeField]
    private float m_HealthRegenAbility = 0.25f;
    public float HealthRegenAbility
    {
        get { return m_HealthRegenAbility; }
    }

    [Header("Strenght")]
    [SerializeField]
    private float m_AttackDamage = 30;
    public float AttackDamage
    {
        get { return m_AttackDamage; }
    }

    [SerializeField]
    private float m_AttackDamagePerLevel = 10;
    public float AttackDamagePerLevel
    {
        get { return m_AttackDamagePerLevel; }
    }

    [Header("Dexterity")]
    [SerializeField]
    private float m_MoveDistance = 1;
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

    [Header("Perception")]
    [SerializeField]
    private float m_RangeAttackDamage = 25f;
    public float RangeAttackDamage
    {
        get { return m_RangeAttackDamage; }
    }

    [SerializeField]
    private float m_RangeAttackDamagePerLevel = 10f;
    public float RangeAttackDamagePerLevel
    {
        get { return m_RangeAttackDamagePerLevel; }
    }

    [Header("Range")]
    [SerializeField]
    private float m_RangeAttackRange = 1;
    public float RangeAttackRange
    {
        get { return m_RangeAttackRange; }
    }

    [SerializeField]
    private float m_RangeAttackRangePerLevel = 0.1f;
    public float RangeAttackRangePerLevel
    {
        get { return m_RangeAttackRangePerLevel; }
    }

    [Header("Constitution")]
    [SerializeField]
    private float m_MaxHealth = 100;
    public float MaxHealth
    {
        get { return m_MaxHealth; }
    }

    [SerializeField]
    private float m_HealthPerLevel = 10;
    public float HealthPerLevel
    {
        get { return m_HealthPerLevel; }
    }

    // NEED TO ADD FOR ALPHA

    //  Force	      Augmente les dégâts lors d’une attaque au corps à corps                                       DONE
    //  Dextérité	  Augmente la chance d’éviter les attaques ennemies ET le déplacement du joueur                 % ?
    //  Constitution  Augmente les points de vie du personnage                                                      DONE
    //  Perception	  Augmente les dégâts avec l’arc à flèche                                                       DONE
    //  Précision	  Augmente la chance de toucher l’ennemi lors d’une attaque à distance et en corps à corps      % ?
    //  Portée	      Augmente la portée de l’arc à flèche                                                          DONE

}