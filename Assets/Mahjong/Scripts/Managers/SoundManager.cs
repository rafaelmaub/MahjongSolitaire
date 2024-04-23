using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Initialization
    public static SoundManager Instance => instance;
    private static SoundManager instance;
    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    #endregion

    [SerializeField] private AudioSource mainFx;
    [SerializeField] private AudioSource mainMusic;


    public void StartMusic()
    {
        mainMusic.mute = true;
    }
    public void StopMusic()
    {
        mainMusic.mute = false;
    }

    public void PlayCustomOneShot(AudioClip clip)
    {
        mainFx.PlayOneShot(clip);
    }
}
