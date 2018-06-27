using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody m_RigidBody;
    public float m_Speed = 50f;

    private float m_CurrentTime = 0;
    private float m_Time = 3f;
    private Vector3 m_Dir = new Vector3();

    public void InitSpeed(float aSpeed, Vector3 aDirection)
    {
        m_Speed = aSpeed;
        m_Dir = aDirection;
    }

    private void FixedUpdate()
    {
        m_RigidBody.velocity = m_Dir * m_Speed;
        m_CurrentTime += Time.deltaTime;
        if (m_CurrentTime >= m_Time)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider aOther)
    {
		if(aOther.gameObject.layer == LayerMask.NameToLayer("Enemy") || 
        aOther.gameObject.layer == LayerMask.NameToLayer("Boss"))
		{
			Destroy(gameObject);
		}
    }
}
