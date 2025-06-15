using System.Collections;
using UnityEngine;

public class LipSyncCharacter : MonoBehaviour
{
    [Header("Audio & Mulut")]
    public AudioSource audioSource;
    public SpriteRenderer mouthRenderer;
    public Sprite[] mouthSprites;

    private float[] spectrum = new float[64];

    public void PlayVoiceOver(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;
        audioSource.clip = clip;
        audioSource.Play();
    }

    void Update()
    {
        if (audioSource == null || mouthRenderer == null || mouthSprites == null || mouthSprites.Length == 0)
            return;

        if (!audioSource.isPlaying)
            return;

        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        float sum = 0f;
        for (int i = 0; i < 10; i++)
        {
            sum += spectrum[i];
        }

        float average = sum / 10f;

        int spriteIndex = Mathf.Clamp(Mathf.FloorToInt(average * 1000), 0, mouthSprites.Length - 1);

        mouthRenderer.sprite = mouthSprites[spriteIndex];
    }
}
