using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicToggle : MonoBehaviour
{
    public AudioSource bgmSource;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Image buttonImage;

    private bool isMuted = false;

    void Start()
    {
        // Load mute status dari PlayerPrefs jika ada
        isMuted = PlayerPrefs.GetInt("BGM_Muted", 0) == 1;
        bgmSource.mute = isMuted;
        UpdateIcon();
    }

    public void ToggleBGM()
    {
        isMuted = !isMuted;
        bgmSource.mute = isMuted;
        UpdateIcon();

        // Simpan status mute
        PlayerPrefs.SetInt("BGM_Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    void UpdateIcon()
    {
        buttonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }
}
