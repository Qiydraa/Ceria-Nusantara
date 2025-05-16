using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipSyncCharacter : MonoBehaviour
{

    public AudioSource audioSource;
    public SpriteRenderer mouthRenderer;
    public Sprite[] mouthSprites; 

    private float[] spectrum = new float[64];

    void Update() {
        if (!audioSource.isPlaying) return;

        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        float sum = 0f;
        for (int i = 0; i < 10; i++) {
            sum += spectrum[i];
        }

        float average = sum / 10f;

        int spriteIndex = Mathf.Clamp(Mathf.FloorToInt(average * 1000), 0, mouthSprites.Length - 1);

        mouthRenderer.sprite = mouthSprites[spriteIndex];

        Debug.Log("Amplitude: " + average + " -> Sprite: " + spriteIndex);
    }
}
