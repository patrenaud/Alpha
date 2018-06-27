using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float m_AttackDamage = 10f;
    public TurnManager m_Turnmanager;
    public GameObject m_Player;
    public GameObject m_AttackZonePrefab;
    public bool m_Attackable = false;
    [HideInInspector]
    public bool m_IsPlaying = false;
    public Material m_EnemyMaterial;

    private Vector3 m_NormalAttackZone;

    private void Start()
    {
        m_EnemyMaterial.color = gameObject.GetComponent<Renderer>().material.color;
        m_NormalAttackZone = m_AttackZonePrefab.transform.localScale;
        m_AttackZonePrefab.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (m_IsPlaying)
        {
            m_AttackZonePrefab.transform.localScale = m_NormalAttackZone;
            EnemyAttack();
            // Si l'ennemi ne peut pas attaquer, 
            // il pourra bouger par la suite et réessayer pour son attaque.
        }
    }

    private void EnemyAttack()
    {
        m_IsPlaying = false; // Termine l'activation des actions de l'ennemi
        StartCoroutine(EnemyAttackCoroutine());

        // Cette ligne permet de faire un Raycast de la grandeur du scale m'attackzoneprefab entre le player et l'ennemi.  Le size/2 trouve le rayon.
        if (Physics.Raycast(transform.position, (m_Player.transform.position - transform.position), m_AttackZonePrefab.transform.localScale.z / 2, LayerMask.GetMask("Player")))
        { 
            m_Player.GetComponent<PlayerController>().ApplyDamage(m_AttackDamage);
        }
    }    
    private IEnumerator EnemyAttackCoroutine()
    {
        yield return new WaitForSeconds(2f);
        m_AttackZonePrefab.transform.localScale = Vector3.zero;
        m_Turnmanager.m_SwitchCharacter = true; // Termine le tour de l'ennemi et envoi le tour au prochain du TurnManager
    }


    // Les changements de couleurs et le changement du bool se font lorsque la zone d'attaque du joueur entre en collision avec les ennemis
    private void OnTriggerStay(Collider a_Other)
    {
        if (a_Other.gameObject.layer == LayerMask.NameToLayer("Interractible") )
        {            
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            m_Attackable = true;
        }
    }
    
    // Lorsque la zone d'attaque quitte l'ennemi, il reprend sa couleur d'avant
    private void OnTriggerExit(Collider a_Other)
    {
        if (a_Other.gameObject.layer == LayerMask.NameToLayer("Interractible"))
        {
            gameObject.GetComponent<Renderer>().material.color =  m_EnemyMaterial.color;
            m_Attackable = false;
        }
    }
}
