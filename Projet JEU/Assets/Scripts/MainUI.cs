using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{


    public Slider m_HealthBar;
    public Slider m_XpBar;
    public Button m_AttackButton;
    public Button m_MeleeAttackButton;
    public Button m_RangeAttackButton;
    public Button m_MoveButton;
    public Button m_AbilityButton;
    public Button m_EndTurnButton;
    public Button m_Ability1;
    public Button m_Ability2;
    public Button m_Ability3;
    public Button m_Ability4;
    public Button m_LevelUpButton;
    public GameObject m_UpgradeCanvas;

    private void Awake()
    {
        TurnManager.Instance.m_MainUI = this;
    }

    public void StartingUI()
    {
        TurnManager.Instance.m_MainUI.m_UpgradeCanvas.gameObject.SetActive(false);
        TurnManager.Instance.m_MainUI.m_Ability1.gameObject.SetActive(false);
        TurnManager.Instance.m_MainUI.m_Ability2.gameObject.SetActive(false);
        TurnManager.Instance.m_MainUI.m_Ability3.gameObject.SetActive(false);
        TurnManager.Instance.m_MainUI.m_Ability4.gameObject.SetActive(false);
        TurnManager.Instance.m_MainUI.m_LevelUpButton.gameObject.SetActive(false);
        TurnManager.Instance.m_MainUI.m_RangeAttackButton.gameObject.SetActive(false);
        TurnManager.Instance.m_MainUI.m_MeleeAttackButton.gameObject.SetActive(false);
    }

    public void ActivateAbilityButtons()
    {
        TurnManager.Instance.m_MainUI.m_Ability1.gameObject.SetActive(true);
        TurnManager.Instance.m_MainUI.m_Ability2.gameObject.SetActive(true);
        TurnManager.Instance.m_MainUI.m_Ability3.gameObject.SetActive(true);
        TurnManager.Instance.m_MainUI.m_Ability4.gameObject.SetActive(true);
    }

    public void DeactivateAbilityButtons()
    {
        TurnManager.Instance.m_MainUI.m_Ability1.gameObject.SetActive(false);
        TurnManager.Instance.m_MainUI.m_Ability2.gameObject.SetActive(false);
        TurnManager.Instance.m_MainUI.m_Ability3.gameObject.SetActive(false);
        TurnManager.Instance.m_MainUI.m_Ability4.gameObject.SetActive(false);
    }

    public void DeactivateUI()
    {
        TurnManager.Instance.m_MainUI.m_AttackButton.interactable = false;
        TurnManager.Instance.m_MainUI.m_MoveButton.interactable = false;
        TurnManager.Instance.m_MainUI.m_AbilityButton.interactable = false;
        TurnManager.Instance.m_MainUI.m_EndTurnButton.interactable = false;
        TurnManager.Instance.m_MainUI.m_MeleeAttackButton.interactable = false;
        TurnManager.Instance.m_MainUI.m_RangeAttackButton.interactable = false;
    }

    public void ActivateAttackChoice()
    {
        TurnManager.Instance.m_MainUI.m_MeleeAttackButton.gameObject.SetActive(true);
        TurnManager.Instance.m_MainUI.m_RangeAttackButton.gameObject.SetActive(true);
    }

    public void DeactivateAttackChoice()
    {
        TurnManager.Instance.m_MainUI.m_MeleeAttackButton.gameObject.SetActive(false);
        TurnManager.Instance.m_MainUI.m_RangeAttackButton.gameObject.SetActive(false);
    }

    


    private void OnDestroy()
    {
        TurnManager.Instance.m_MainUI = null;
    }

}
