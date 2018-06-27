using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrades : MonoBehaviour
{
    public PlayerController m_PlayerController;

    // Seulement le premier Upgrade a été fait
    public void ActivateRangedAttack()
    {
        m_PlayerController.m_RangeAttack = true;
    }

    // Cette fonction est appalé après le choix du joueur pour fermer le HUD d'upgrades.
    public void ReturnToGame()
    {
        m_PlayerController.m_UpgradeCanvas.gameObject.SetActive(false);
        m_PlayerController.m_LevelUpButton.gameObject.SetActive(false);
        m_PlayerController.m_XpBar.value = 0f;
    }
}
