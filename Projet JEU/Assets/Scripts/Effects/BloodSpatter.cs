using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpatter : MonoBehaviour 
{

	[SerializeField]
	private float m_Timer = 1.5f;
	private float m_CurrentTime;
	
	void Start () 
	{
		m_CurrentTime = 0.0f;
	}
	

	void Update () 
	{
		m_CurrentTime += Time.deltaTime;
		if(m_CurrentTime >= m_Timer)
		{
			Destroy(gameObject);
		}
	}
}
