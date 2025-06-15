using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SFXButton : MonoBehaviour, IPointerClickHandler
{
    public AudioClip clickSound;
    public float delayBeforeAction = 0.3f; // Durasi delay sebelum aksi

    [Header("Aksi Setelah SFX")]
    public UnityEvent onClickDelayed;

    private static AudioSource sharedAudioSource;

    private void Awake()
    {
        if (sharedAudioSource == null)
        {
            GameObject audioObj = GameObject.Find("SFXAudioSource");
            if (audioObj == null)
            {
                audioObj = new GameObject("SFXAudioSource");
                sharedAudioSource = audioObj.AddComponent<AudioSource>();
                DontDestroyOnLoad(audioObj);
            }
            else
            {
                sharedAudioSource = audioObj.GetComponent<AudioSource>();
                if (sharedAudioSource == null)
                    sharedAudioSource = audioObj.AddComponent<AudioSource>();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 🔊 Langsung bunyikan SFX
        if (clickSound != null && sharedAudioSource != null)
        {
            sharedAudioSource.PlayOneShot(clickSound);
        }

        // ⏱ Jalankan aksi setelah delay
        if (onClickDelayed != null)
        {
            Invoke(nameof(ExecuteDelayedAction), delayBeforeAction);
        }
    }

    private void ExecuteDelayedAction()
    {
        onClickDelayed.Invoke();
    }
}
