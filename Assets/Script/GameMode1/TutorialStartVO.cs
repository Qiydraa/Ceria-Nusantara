using UnityEngine;

public class TutorialStartVO : MonoBehaviour
{
    public LipSyncCharacter lipSync;  // Drag komponen LipSyncCharacter kamu di sini
    public AudioClip voiceClip;       // VO yang ingin dimainkan saat scene dimulai

    void Start()
    {
        if (lipSync != null && voiceClip != null)
        {
            lipSync.PlayVoiceOver(voiceClip);
        }
        else
        {
            Debug.LogWarning("LipSyncCharacter atau VoiceClip belum diisi di Inspector.");
        }
    }
}
