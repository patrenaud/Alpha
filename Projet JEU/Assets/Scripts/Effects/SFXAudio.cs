using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource; // Serialized pour voir ce qui joueé

    private float m_Counter;
    private float m_Duration;

    public void Setup(AudioClip i_Clip)
    {
		m_AudioSource.clip = i_Clip;
		m_Duration = i_Clip.length;
    }

    public void Loop(AudioClip i_Clip)
    {
        m_AudioSource.clip = i_Clip;
        m_Duration = i_Clip.length*100;
    }

    public void Play()
    {
        m_AudioSource.Play();
    }

    private void Update()
    {
        m_Counter += Time.deltaTime;
        if (m_Counter >= m_Duration)
        {
            Destroy(gameObject); // On détruit l'object pour qu'il ne reste pas dans le projet après le son terminé
        }
    }
}
