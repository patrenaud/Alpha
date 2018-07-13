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
        PlayerManager.Instance.m_Player.ActivateHabilty();
    }

    public void EndTurn()
    {
        PlayerManager.Instance.m_Player.EndTurn();
    }

    public void Ability1()
    {

    }

    public void Ability2()
    {

    }

    public void Ability3()
    {

    }

    public void Ability4()
    {
        PlayerManager.Instance.m_Player.ActivateAbility4();
    }

	public void LevelUp()
	{
		PlayerManager.Instance.m_Player.LevelUp();
	}
}
