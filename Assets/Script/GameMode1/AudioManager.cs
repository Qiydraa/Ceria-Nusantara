using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSourcePrefab;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float bgmVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private string currentBgmName = "";
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (clip == null || clip.name == currentBgmName) return;

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
        currentBgmName = clip.name;
    }

    public void StopBGM()
    {
        bgmSource.Stop();
        currentBgmName = "";
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        AudioSource sfx = Instantiate(sfxSourcePrefab, transform);
        sfx.clip = clip;
        sfx.volume = sfxVolume;
        sfx.Play();
        Destroy(sfx.gameObject, clip.length);
    }

    public AudioClip LoadClip(string name)
    {
        if (audioClips.ContainsKey(name)) return audioClips[name];

        AudioClip clip = Resources.Load<AudioClip>(name);
        if (clip != null)
        {
            audioClips[name] = clip;
        }
        else
        {
            Debug.LogWarning("AudioClip not found: " + name);
        }
        return clip;
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }
}
