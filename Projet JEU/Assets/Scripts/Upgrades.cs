using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    [Header("Upgrade Button List")]
    [SerializeField] private Button m_ArcherButton1;
    [SerializeField] private Button m_ArcherButton2;
    [SerializeField] private Button m_ArcherButton3;
    [SerializeField] private Button m_ArcherButton4;
    [SerializeField] private Button m_ArcherButton5;
    [SerializeField] private Button m_AthleteButton1;
    [SerializeField] private Button m_AthleteButton2;
    [SerializeField] private Button m_AthleteButton3;
    [SerializeField] private Button m_AthleteButton4;
    [SerializeField] private Button m_AthleteButton5;
    [SerializeField] private Button m_BruteButton1;
    [SerializeField] private Button m_BruteButton2;
    [SerializeField] private Button m_BruteButton3;
    [SerializeField] private Button m_BruteButton4;
    [SerializeField] private Button m_BruteButton5;
    [SerializeField] private Button m_ArcanistButton1;
    [SerializeField] private Button m_ArcanistButton2;
    [SerializeField] private Button m_ArcanistButton3;
    [SerializeField] private Button m_ArcanistButton4;
    [SerializeField] private Button m_ArcanistButton5;

    // Cette fonction est appelée après le choix du joueur pour fermer le HUD d'upgrades.
    public void ReturnToGame()
    {
        PlayerManager.Instance.m_MainUI.m_UpgradeCanvas.gameObject.SetActive(false);
        PlayerManager.Instance.m_MainUI.m_LevelUpButton.gameObject.SetActive(false);
        PlayerManager.Instance.m_MainUI.m_XpBar.value = 0f;
    }

    // Seulement le premier Upgrade a été fait
    public void ActivateRangedAttack()
    {
        PlayerManager.Instance.m_RangeAttack = true;
        m_ArcherButton1.interactable = false;
        m_ArcherButton2.interactable = true;
        ReturnToGame();
    }

    public void Archer2()
    {
        PlayerManager.Instance.m_PlayerPrecision += 1;
        PlayerManager.Instance.m_PlayerPerception += 1;
        m_ArcherButton2.interactable = false;
        m_ArcherButton3.interactable = true;
        ReturnToGame();
    }
    public void Archer3()
    {

        PlayerManager.Instance.m_PlayerPrecision += 1;
        PlayerManager.Instance.m_PlayerPerception += 1;
        m_ArcherButton3.interactable = false;
        m_ArcherButton4.interactable = true;
        ReturnToGame();
    }
    public void Archer4()
    {
        PlayerManager.Instance.m_PlayerRange += 1;
        PlayerManager.Instance.m_Player.SetNewZoneStats();
        m_ArcherButton4.interactable = false;
        m_ArcherButton5.interactable = true;
        ReturnToGame();
    }
    public void Archer5()
    {
        PlayerManager.Instance.m_PlayerPrecision += 1;
        PlayerManager.Instance.m_PlayerPerception += 1;
        PlayerManager.Instance.m_PlayerRange += 1;
        PlayerManager.Instance.m_Player.SetNewZoneStats();
        m_ArcherButton5.interactable = false;
        ReturnToGame();
    }

    public void Brute1()
    {
        PlayerManager.Instance.m_PlayerStrenght += 1;
        m_BruteButton1.interactable = false;
        m_BruteButton2.interactable = true;
        ReturnToGame();
    }
    public void Brute2()
    {
        PlayerManager.Instance.m_PlayerConstitution += 1;
        m_BruteButton2.interactable = false;
        m_BruteButton3.interactable = true;
        ReturnToGame();
    }
    public void Brute3()
    {
        PlayerManager.Instance.m_PlayerConstitution += 1;
        PlayerManager.Instance.m_PlayerStrenght += 1;
        m_BruteButton3.interactable = false;
        m_BruteButton4.interactable = true;
        ReturnToGame();
    }
    public void Brute4()
    {
        // This is to activate Extreme force ability
        PlayerManager.Instance.ActivateAbility2();
        PlayerManager.Instance.m_MainUI.m_Ability2.GetComponentInChildren<Text>().color = Color.white;
        PlayerManager.Instance.m_MainUI.DeactivateAbilityButtons();
        
        m_BruteButton4.interactable = false;
        m_BruteButton5.interactable = true;
        ReturnToGame();
    }
    public void Brute5()
    {
        PlayerManager.Instance.m_PlayerConstitution += 1;
        PlayerManager.Instance.m_PlayerStrenght += 1;
        m_BruteButton5.interactable = false;
        ReturnToGame();
    }

    public void Athlete1()
    {
        PlayerManager.Instance.m_PlayerConstitution += 1;
        m_AthleteButton1.interactable = false;
        m_AthleteButton2.interactable = true;
        ReturnToGame();
    }
    public void Athlete2()
    {
        PlayerManager.Instance.m_PlayerDexterity += 1;
        PlayerManager.Instance.m_Player.SetNewZoneStats();
        m_AthleteButton2.interactable = false;
        m_AthleteButton3.interactable = true;
        ReturnToGame();
    }
    public void Athlete3()
    {
        PlayerManager.Instance.m_PlayerStrenght += 1;
        m_AthleteButton3.interactable = false;
        m_AthleteButton4.interactable = true;
        ReturnToGame();
    }
    public void Athlete4()
    {
        // Activate : Ninja Strike
        PlayerManager.Instance.ActivateAbility3();
        PlayerManager.Instance.m_MainUI.m_Ability3.GetComponentInChildren<Text>().color = Color.white;
        PlayerManager.Instance.m_PlayerDexterity += 1;
        PlayerManager.Instance.m_Player.SetNewZoneStats();
        m_AthleteButton4.interactable = false;
        m_AthleteButton5.interactable = true;
        ReturnToGame();
    }
    public void Athlete5()
    {
        PlayerManager.Instance.m_PlayerPrecision += 1;
        PlayerManager.Instance.m_PlayerDexterity += 1;
        PlayerManager.Instance.m_PlayerConstitution += 1;
        m_AthleteButton5.interactable = false;
        ReturnToGame();
    }

    public void Arcanist1()
    {
        // This is to activate Regenerate ability
        PlayerManager.Instance.ActivateAbility4();
        PlayerManager.Instance.m_MainUI.m_Ability4.GetComponentInChildren<Text>().color = Color.white;
        PlayerManager.Instance.m_MainUI.DeactivateAbilityButtons();

        m_ArcanistButton1.interactable = false;
        m_ArcanistButton2.interactable = true;
        ReturnToGame();
    }
    public void Arcanist2()
    {
        // This is to activate Root ability
        PlayerManager.Instance.ActivateAbility1();
        PlayerManager.Instance.m_MainUI.m_Ability1.GetComponentInChildren<Text>().color = Color.white;
        PlayerManager.Instance.m_MainUI.DeactivateAbilityButtons();

        m_ArcanistButton2.interactable = false;
        m_ArcanistButton3.interactable = true;
        ReturnToGame();
    }
    public void Arcanist3()
    {
        PlayerManager.Instance.m_PlayerConstitution += 1;
        m_ArcanistButton3.interactable = false;
        m_ArcanistButton4.interactable = true;
        ReturnToGame();
    }
    public void Arcanist4()
    {
        PlayerManager.Instance.ActivateAbility1();
        m_ArcanistButton4.interactable = false;
        m_ArcanistButton5.interactable = true;
        ReturnToGame();
    }
    public void Arcanist5()
    {
        // Boosts Root Damage
        // Boosts Regenerate Heal
        PlayerManager.Instance.m_PlayerStrenght += 1;
        m_ArcanistButton5.interactable = false;
        ReturnToGame();
    }
}
