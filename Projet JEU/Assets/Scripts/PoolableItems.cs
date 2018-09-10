using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableItems : MonoBehaviour
{
    public EPoolType m_PoolType;
    public int m_Quantity;

    // Ceci est pour les actions du PoolManager
    private void OnEnable()
    {
        if (PoolManager.Instance != null)
        {
            PoolManager.Instance.m_OnChangeScene += ReturnToPool; // Exemple d'action
        }
    }
    private void OnDisable()
    {
        if (PoolManager.Instance != null)
        {

            PoolManager.Instance.m_OnChangeScene -= ReturnToPool;
        }
    }

    public void StartTimer(float a_Time)
    {
        StartCoroutine(WaitAndReturn(a_Time));
    }

    private IEnumerator WaitAndReturn(float a_Time)
    {
        yield return new WaitForSeconds(a_Time);
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        StopAllCoroutines();
        PoolManager.Instance.ReturnToPool(m_PoolType, gameObject);
    }


}
