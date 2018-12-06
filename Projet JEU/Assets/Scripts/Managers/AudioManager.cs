using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : DontDestroyOnLoad
{
    // Static = On peut y accéder de partout et tout le monde
    private static AudioManager m_Instance;
    public static AudioManager Instance
    {
        // On fait un get public pour que les autres classes puissent accéder au Singleton sans pouvoir l'assigner (set)
        get
        {
            return m_Instance;
        }
    }
    [SerializeField]
    private SFXAudio m_SFXAudioPrefab;    
    
    [SerializeField]
    private AudioSource m_AudioSourceMusic;

    // Ceci est les 4 différentes musique d'ambiance dans le jeu. Elles changent selon la scene.
    public AudioClip m_Breathing;
    public AudioClip m_Cavern;    
    public AudioClip m_Ravens;
    public AudioClip m_Death;

    // Voici la liste des sons
    public List<AudioClip> m_SoundList = new List<AudioClip>();

    protected override void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
        }
        base.Awake(); // On doit appeler le parent pour que le DontDestroy se fasse.
    }

    private void Start()
    {
        PlaySFX(m_Ravens, transform.position);
    }

    private IEnumerator FadeOutFadeInMusic(float i_Duration, AudioClip i_NextClip) // i_Duration est le temps que le volume se rendre au maximum
    {
        if (i_Duration <= 0) // Ici on instaure un WARNING si la duration est trop faible 
        {
            Debug.LogError("Error Duration <= 0 so don't do it you piece of shit, ASK PAT IF YOU SEE THIS"); // Laisse des traces en équipes pour débug
            yield break; // Sort de l'énumérateur.
        }
        while (m_AudioSourceMusic.volume > 0f)
        {
            m_AudioSourceMusic.volume -= Time.deltaTime / i_Duration;
            yield return null;
        }

        m_AudioSourceMusic.clip = i_NextClip;
        m_AudioSourceMusic.Play();

        while (m_AudioSourceMusic.volume < 1f)
        {
            m_AudioSourceMusic.volume += Time.deltaTime / i_Duration;
        }
    }

    public void SwitchMusic(float i_Duration, AudioClip nextClip/*AudioClip i_music1, AudioClip i_music2*/)
    {
        //AudioClip nextClip = m_AudioSourceMusic.clip == i_music1 ? i_music2 : i_music1;
        StartCoroutine(FadeOutFadeInMusic(i_Duration, nextClip));
    }

    public void PlaySFX(AudioClip i_Clip, Vector3 i_Position) // au lieu du vector, un transform et un bool. Le son se fait sur le character et bool le suit ou pas
    {
        SFXAudio audio = Instantiate(m_SFXAudioPrefab, i_Position, Quaternion.identity);
        if (i_Clip == m_Ravens || i_Clip == m_Cavern || i_Clip == m_Breathing)
        {
            audio.GetComponentInChildren<AudioSource>().loop = true;
            audio.Loop(i_Clip);
            audio.Play();
        }
        else
        {
            audio.Setup(i_Clip);
            audio.Play();
        }
    }

}