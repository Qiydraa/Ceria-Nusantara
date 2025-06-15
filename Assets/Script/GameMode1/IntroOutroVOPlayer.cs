using UnityEngine;

public class IntroOutroVOPlayer : MonoBehaviour
{
    [Header("Audio VO")]
    public AudioSource audioSource;
    public AudioClip voAwal;
    public AudioClip voAkhir;

    [Header("Referensi Content Manager")]
    public ContentManager contentManager;

    private bool introPlayed = false;
    private bool outroPlayed = false;

    private void Start()
    {
        // Mainkan VO awal saat scene dimulai
        if (voAwal != null && audioSource != null)
        {
            audioSource.clip = voAwal;
            audioSource.Play();
            introPlayed = true;
        }
    }

    private void Update()
    {
        if (!outroPlayed && IsLastContentCompleted())
        {
            PlayOutroVO();
        }
    }

    private void PlayOutroVO()
    {
        if (voAkhir != null && audioSource != null)
        {
            audioSource.clip = voAkhir;
            audioSource.Play();
            outroPlayed = true;
        }
    }

    private bool IsLastContentCompleted()
    {
        if (contentManager == null || contentManager.Contents.Length == 0)
            return false;

        int lastIndex = contentManager.Contents.Length - 1;

        // Cek keamanan akses array
        if (lastIndex >= contentManager.LipSyncs.Length || contentManager.LipSyncs[lastIndex] == null)
            return false;

        // Jika LipSync terakhir sudah selesai (disabled), artinya konten terakhir selesai
        return !contentManager.LipSyncs[lastIndex].enabled;
    }
}
