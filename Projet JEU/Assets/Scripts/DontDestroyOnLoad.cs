using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour 
{
    protected virtual void Awake()
    {
        // Notre objet ne se détruira pas grâce à la fonction
        DontDestroyOnLoad(gameObject);
    }
}
