using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonManager : MonoBehaviour
{

    public void ActivateMove()
    {
        PlayerManager.Instance.m_Player.ActivateMove();
    }

    public void ActivateAttack()
    {
        PlayerManager.Instance.m_Player.ActivateAttack();
    }

    public void ActivateHability()
    {
        PlayerManager.Instance.m_Player.ActivateHabiltyButton();
    }

    public void EndTurn()
    {
        PlayerManager.Instance.m_Player.EndTurn();
    }

    public void Ability1()
    {
        PlayerManager.Instance.m_Player.ActivateAbility1();
    }

    public void Ability2()
    {
        PlayerManager.Instance.m_Player.ActivateAbility2();
    }

    public void Ability3()
    {
        PlayerManager.Instance.m_Player.ActivateAbility3();
    }

    public void Ability4()
    {
        PlayerManager.Instance.m_Player.ActivateAbility4();
    }

	public void LevelUp()
	{
		PlayerManager.Instance.LevelUp();
	}
}
