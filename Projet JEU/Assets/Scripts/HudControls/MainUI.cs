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
        PlayerManager.Instance.m_MainUI = this;
    }

    public void StartingUI()
    {
        m_UpgradeCanvas.gameObject.SetActive(false);
        m_Ability1.gameObject.SetActive(false);
        m_Ability2.gameObject.SetActive(false);
        m_Ability3.gameObject.SetActive(false);
        m_Ability4.gameObject.SetActive(false);
        m_LevelUpButton.gameObject.SetActive(false);
        m_RangeAttackButton.gameObject.SetActive(false);
        m_MeleeAttackButton.gameObject.SetActive(false);
    }

    public void ResetAbilities()
    {
        if (PlayerManager.Instance.m_ActivateAbility1)
        {
            m_Ability1.interactable = true;
            m_Ability1.GetComponentInChildren<Text>().color = Color.white;
        }
        if (PlayerManager.Instance.m_ActivateAbility2)
        {
            m_Ability2.interactable = true;
            m_Ability2.GetComponentInChildren<Text>().color = Color.white;
        }
        if (PlayerManager.Instance.m_ActivateAbility3)
        {
            m_Ability3.interactable = true;
            m_Ability3.GetComponentInChildren<Text>().color = Color.white;
        }
        if (PlayerManager.Instance.m_ActivateAbility4)
        {
            m_Ability4.interactable = true;
            m_Ability4.GetComponentInChildren<Text>().color = Color.white;
        }
    }

    public void ActivateAbilityButtons()
    {           
        m_Ability1.gameObject.SetActive(true);
        m_Ability2.gameObject.SetActive(true);
        m_Ability3.gameObject.SetActive(true);
        m_Ability4.gameObject.SetActive(true);
        
    }

    public void DeactivateAbilityButtons()
    {
        m_Ability1.gameObject.SetActive(false);
        m_Ability2.gameObject.SetActive(false);
        m_Ability3.gameObject.SetActive(false);
        m_Ability4.gameObject.SetActive(false);
    }

    public void DeactivateUI()
    {
        m_AttackButton.interactable = false;
        m_MoveButton.interactable = false;
        m_AbilityButton.interactable = false;
        m_EndTurnButton.interactable = false;
        m_MeleeAttackButton.interactable = false;
        m_RangeAttackButton.interactable = false;
    }

    public void ActivateAttackChoice()
    {
        m_MeleeAttackButton.gameObject.SetActive(true);
        m_RangeAttackButton.gameObject.SetActive(true);
    }

    public void DeactivateAttackChoice()
    {
        m_MeleeAttackButton.gameObject.SetActive(false);
        m_RangeAttackButton.gameObject.SetActive(false);
    }

    public void ActivatePlayerUiOnTurnBegin()
    {
        m_AttackButton.interactable = true;
        m_MoveButton.interactable = true;
        m_AbilityButton.interactable = true;
        m_EndTurnButton.interactable = true;        
        if (PlayerManager.Instance.m_RangeAttack)
        {
            m_MeleeAttackButton.interactable = true;
            m_RangeAttackButton.interactable = true;
        }
    }

    public void OnPlayerAttackEnd()
    {
        m_AttackButton.interactable = false;
        m_RangeAttackButton.interactable = false;
        m_MeleeAttackButton.interactable = false;        
        PlayerManager.Instance.m_Player.m_MeleeButtonIsPressed = false;
        PlayerManager.Instance.m_Player.m_RangeButtonIsPressed = false;
        PlayerManager.Instance.m_Player.m_Animator.SetTrigger("Idle");
        PlayerManager.Instance.m_Player.m_AttackZone.transform.localScale = Vector3.zero;
        PlayerManager.Instance.m_Player.m_RangeAttackZone.transform.localScale = Vector3.zero;

    }

    private void OnDestroy()
    {
        PlayerManager.Instance.m_MainUI = null;
    }

    public void StartHpAndExp()
    {
        m_HealthBar.value = 1;
        m_XpBar.value = 0;
    }

    public void OnActivateAbility1()
    {
        m_Ability1.interactable = false;
        m_Ability1.GetComponentInChildren<Text>().color = Color.grey;
#if UNITY_CHEATS
        if (PlayerManager.Instance.m_AbilityCheat)
        {
            PlayerManager.Instance.ActivateAbility1();

        }
#endif
    }

    public void OnActivateAbility2()
    {
        m_Ability2.interactable = false;
        m_Ability2.GetComponentInChildren<Text>().color = Color.grey;
#if UNITY_CHEATS
        if (PlayerManager.Instance.m_AbilityCheat)
        {
            PlayerManager.Instance.ActivateAbility2();

        }
#endif
    }

    public void OnActivateAbility3()
    {
        m_Ability3.interactable = false;
        m_Ability3.GetComponentInChildren<Text>().color = Color.grey;
#if UNITY_CHEATS
        if (PlayerManager.Instance.m_AbilityCheat)
        {
            PlayerManager.Instance.ActivateAbility3();

        }
#endif
    }

    public void OnActivateAbility4(float aRegenHealth)
    {
        m_Ability4.interactable = false;
        m_HealthBar.value += aRegenHealth;
        m_Ability4.GetComponentInChildren<Text>().color = Color.grey;
#if UNITY_CHEATS
        if (PlayerManager.Instance.m_AbilityCheat)
        {
            PlayerManager.Instance.ActivateAbility4();

        }
#endif
    }

    public void DeactivateMove()
    {
        PlayerManager.Instance.m_MainUI.m_MoveButton.interactable = false;
    }

    public void ActivateSelf()
    {
        gameObject.SetActive(true);
        ReactivateAbilityButtons();
        ActivatePlayerUiOnTurnBegin();
    }

    public void DeactivateSelf()
    {
        gameObject.SetActive(false);
    }

    public void ReactivateAbilityButtons()
    {
        if(m_Ability1.interactable == false  && PlayerManager.Instance.m_ActivateAbility1)
        {
            m_Ability1.interactable = true;
        }
        if (m_Ability2.interactable == false && PlayerManager.Instance.m_ActivateAbility2)
        {
            m_Ability2.interactable = true;
        }
        if (m_Ability3.interactable == false && PlayerManager.Instance.m_ActivateAbility3)
        {
            m_Ability3.interactable = true;
        }
        if (m_Ability4.interactable == false && PlayerManager.Instance.m_ActivateAbility4)
        {
            m_Ability4.interactable = true;
        }
    }
}
