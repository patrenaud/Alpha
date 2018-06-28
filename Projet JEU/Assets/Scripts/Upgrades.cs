using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    [Header("Upgrade Button List")]
    public Button m_ArcherButton1;
    public Button m_ArcherButton2;
    public Button m_ArcherButton3;
    public Button m_ArcherButton4;
    public Button m_ArcherButton5;

    public Button m_AthleteButton1;
    public Button m_AthleteButton2;
    public Button m_AthleteButton3;
    public Button m_AthleteButton4;
    public Button m_AthleteButton5;

    public Button m_BruteButton1;
    public Button m_BruteButton2;
    public Button m_BruteButton3;
    public Button m_BruteButton4;
    public Button m_BruteButton5;

    public Button m_ArcanistButton1;
    public Button m_ArcanistButton2;
    public Button m_ArcanistButton3;
    public Button m_ArcanistButton4;
    public Button m_ArcanistButton5;

    [Space(20)]
    public PlayerController m_PlayerController; // Needs to go away
    public UpgradeManager m_UpgradeManager; // HOW TO GET STUFF WITHOUT REFERENCE ??


    // Cette fonction est appelée après le choix du joueur pour fermer le HUD d'upgrades.
    public void ReturnToGame()
    {
        m_PlayerController.m_UpgradeCanvas.gameObject.SetActive(false);
        m_PlayerController.m_LevelUpButton.gameObject.SetActive(false);
        m_PlayerController.m_XpBar.value = 0f;
    }


    // Seulement le premier Upgrade a été fait
    public void ActivateRangedAttack()
    {
        m_PlayerController.m_RangeAttack = true;
        m_ArcherButton1.interactable = false;
        m_ArcherButton2.interactable = true;
        ReturnToGame();
    }

    public void Archer2()
    {
        m_UpgradeManager.m_PlayerPrecision += 1;
        m_UpgradeManager.m_PlayerPerception += 1;
        m_ArcherButton2.interactable = false;
        m_ArcherButton3.interactable = true;
        ReturnToGame();
    }
    public void Archer3()
    {
        m_UpgradeManager.m_PlayerPrecision += 1;
        m_UpgradeManager.m_PlayerPerception += 1;
        m_ArcherButton3.interactable = false;
        m_ArcherButton4.interactable = true;
        ReturnToGame();
    }
    public void Archer4()
    {
        m_UpgradeManager.m_PlayerRange += 1;
        m_ArcherButton4.interactable = false;
        m_ArcherButton5.interactable = true;
        ReturnToGame();
    }
    public void Archer5()
    {
        m_UpgradeManager.m_PlayerPrecision += 1;
        m_UpgradeManager.m_PlayerPerception += 1;
        m_UpgradeManager.m_PlayerRange += 1;
        ReturnToGame();
    }

    public void Brute1()
    {
        m_UpgradeManager.m_PlayerStrenght += 1;
        m_BruteButton1.interactable = false;
        m_BruteButton2.interactable = true;
        ReturnToGame();
    }
    public void Brute2()
    {
        m_UpgradeManager.m_PlayerConstitution += 1;
        m_BruteButton2.interactable = false;
        m_BruteButton3.interactable = true;
        ReturnToGame();
    }
    public void Brute3()
    {
        m_UpgradeManager.m_PlayerConstitution += 1;
        m_UpgradeManager.m_PlayerStrenght += 1;
        m_BruteButton3.interactable = false;
        m_BruteButton4.interactable = true;
        ReturnToGame();
    }
    public void Brute4()
    {
        // Activate : Extreme Force
        m_UpgradeManager.m_PlayerConstitution += 1;
        m_BruteButton4.interactable = false;
        m_BruteButton5.interactable = true;
        ReturnToGame();
    }
    public void Brute5()
    {
        m_UpgradeManager.m_PlayerConstitution += 1;
        m_UpgradeManager.m_PlayerStrenght += 1;
        ReturnToGame();
    }

    public void Athlete1()
    {
        m_UpgradeManager.m_PlayerPrecision += 1;
        m_AthleteButton1.interactable = false;
        m_AthleteButton2.interactable = true;
        ReturnToGame();
    }
    public void Athlete2()
    {
        m_UpgradeManager.m_PlayerDexterity += 1;
        m_AthleteButton2.interactable = false;
        m_AthleteButton3.interactable = true;
        ReturnToGame();
    }
    public void Athlete3()
    {
        m_UpgradeManager.m_PlayerStrenght += 1;
        m_AthleteButton3.interactable = false;
        m_AthleteButton4.interactable = true;
        ReturnToGame();
    }
    public void Athlete4()
    {
        // Activate : Precise hit
        m_UpgradeManager.m_PlayerDexterity += 1;
        m_AthleteButton4.interactable = false;
        m_AthleteButton5.interactable = true;
        ReturnToGame();
    }
    public void Athlete5()
    {
        m_UpgradeManager.m_PlayerPrecision += 1;
        m_UpgradeManager.m_PlayerDexterity += 1;
        m_UpgradeManager.m_PlayerConstitution += 1;
        ReturnToGame();
    }

    public void Arcanist1()
    {
        // Activate : Root
        m_ArcanistButton1.interactable = false;
        m_ArcanistButton2.interactable = true;
        ReturnToGame();
    }
    public void Arcanist2()
    {
        // Boosts Root Damage
        m_ArcanistButton2.interactable = false;
        m_ArcanistButton3.interactable = true;
        ReturnToGame();
    }
    public void Arcanist3()
    {
        m_UpgradeManager.m_PlayerConstitution += 1;
        m_ArcanistButton3.interactable = false;
        m_ArcanistButton4.interactable = true;
        ReturnToGame();
    }
    public void Arcanist4()
    {
        // Activate : Regenerate
        m_ArcanistButton4.interactable = false;
        m_ArcanistButton5.interactable = true;
        ReturnToGame();
    }
    public void Arcanist5()
    {
        // Boosts Root Damage
        // Boosts Regenerate Heal
        m_UpgradeManager.m_PlayerStrenght += 1;
        ReturnToGame();
    }
}
