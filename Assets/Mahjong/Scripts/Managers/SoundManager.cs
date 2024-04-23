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

    [Header("Global Sound Effects")]
    [SerializeField] private AudioClip matchMade;
    [SerializeField] private AudioClip victory;
    [SerializeField] private AudioClip fail;
    private void Start()
    {
        GameManager.Instance.OnGameOver += EndGameSound;
        GameManager.Instance.OnMatchMade += MatchSoundEffect;
    }
    void EndGameSound(bool win)
    {
        if(win)
        {
            PlayCustomOneShot(victory);
        }
        else
        {
            PlayCustomOneShot(fail);
        }
    }

    void MatchSoundEffect()
    {
        PlayCustomOneShot(matchMade);
    }
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
        if (!clip) return;

        mainFx.PlayOneShot(clip);
    }
}
