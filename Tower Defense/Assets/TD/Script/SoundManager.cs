using UnityEngine;
using System.Collections;
/*
 * This is SoundManager
 * In other script, you just need to call SoundManager.PlaySfx(AudioClip) to play the sound
*/
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioClip beginSoundInMainMenu;
    [Tooltip("Play music clip when start")]
    public AudioClip musicsGame;
    [Range(0, 1)]
    public float musicsGameVolume = 0.5f;

    [Tooltip("Place the sound in this to call it in another script by: SoundManager.PlaySfx(soundname);")]
    public AudioClip soundClick;

    [Header("Game State")]
    public AudioClip soundVictory;
    public AudioClip soundFail;
    public AudioClip soundPause;
    public AudioClip soundShowStagePanel;

    [Header("Shop")]
    public AudioClip soundPurchased;
    public AudioClip soundUpgrade;
    public AudioClip soundNotEnoughCoin;

    [Header("Victory")]
    public AudioClip soundVictoryPanel;
    public AudioClip soundStar1;
    public AudioClip soundStar2;
    public AudioClip soundStar3;

    private AudioSource musicAudio;
    private AudioSource soundFx;

    [Header("BOOST ITEM")]
    public AudioClip BTsoundOpen;
    public AudioClip BTsoundHide;
    public AudioClip BTsoundUseBoost;

    #region  MonoBehaviour
    void Awake()
    {
        Instance = this;

        musicAudio = gameObject.AddComponent<AudioSource>();
        musicAudio.loop = true;
        musicAudio.volume = 0.5f;

        soundFx = gameObject.AddComponent<AudioSource>();
    }
    void Start()
    {
        PlayMusic(musicsGame, musicsGameVolume);
    }
    #endregion
    public void PauseMusic(bool isPause)
    {
        if (isPause)
        {
            Instance.musicAudio.mute = true;
        }
        else
        {
            Instance.musicAudio.mute = false;
        }
    }
    public static float MusicVolume
    {

        set { Instance.musicAudio.volume = value; }
        get { return Instance.musicAudio.volume; }
    }
    public static float SoundVolume
    {
        set { Instance.soundFx.volume = value; }
        get { return Instance.soundFx.volume; }
    }
    public static void Click()
    {
        PlaySfx(Instance.soundClick);
    }

    public void ClickBut()
    {
        PlaySfx(soundClick);
    }

    public static void PlaySfx(AudioClip clip)
    {
        if (Instance != null)
        {
            Instance.PlaySound(clip, Instance.soundFx);
        }
    }

    public static void PlaySfx(AudioClip clip, float volume)
    {
        if (Instance != null)
        {
            Instance.PlaySound(clip, Instance.soundFx, volume);
        }
    }

    public static void PlaySfx(AudioClip[] clips)
    {
        if (Instance != null && clips.Length > 0)
        {
            Instance.PlaySound(clips[Random.Range(0, clips.Length)], Instance.soundFx);
        }
    }

    public static void PlaySfx(AudioClip[] clips, float volume)
    {
        if (Instance != null && clips.Length > 0)
        {
            Instance.PlaySound(clips[Random.Range(0, clips.Length)], Instance.soundFx, volume);
        }
    }

    public static void PlayMusic(AudioClip clip)
    {
        Instance.PlaySound(clip, Instance.musicAudio);
    }

    public static void PlayMusic(AudioClip clip, float volume)
    {
        Instance.PlaySound(clip, Instance.musicAudio, volume);
    }

    private void PlaySound(AudioClip clip, AudioSource audioOut)
    {
        if (clip == null)
        {
            return;
        }

        if (Instance == null)
        {
            return;
        }

        if (audioOut == musicAudio)
        {
            audioOut.clip = clip;
            audioOut.Play();
        }
        else
        {
            audioOut.PlayOneShot(clip, SoundVolume);
        }
    }

    private void PlaySound(AudioClip clip, AudioSource audioOut, float volume)
    {
        if (clip == null)
        {
            return;
        }

        if (audioOut == musicAudio)
        {
            audioOut.volume = GlobalValue.isMusic ? volume : 0;
            audioOut.clip = clip;
            audioOut.Play();
        }
        else
        {
            if (!GlobalValue.isSound)
            {
                return;
            }
            audioOut.PlayOneShot(clip, SoundVolume * volume);
        }
    }
}
