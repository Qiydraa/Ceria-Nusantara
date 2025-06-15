using UnityEngine;
using System.Collections;

public class ContentManager : MonoBehaviour
{
    [Header("List konten budaya (Canvas sub-panel drag-drop)")]
    public GameObject[] Contents;

    [Header("Panel Info & LipSync per konten")]
    public GameObject[] InfoPanels;          // Panel penjelasan per konten
    public LipSyncCharacter[] LipSyncs;      // LipSyncCharacter per konten

    [Header("Audio Penjelasan per konten")]
    public AudioClip[] ExplanationClips;     // Audio sesuai dengan urutan konten

    [Header("VO Awal & Akhir")]
    public AudioClip voAwal;
    public LipSyncCharacter lipSyncAwal;

    public AudioClip voAkhir;
    public LipSyncCharacter lipSyncAkhir;

    private int currentContentIndex = 0;

    private void Start()
    {
        // Matikan semua panel info dan lip sync di awal
        foreach (var panel in InfoPanels)
        {
            if (panel != null) panel.SetActive(false);
        }

        foreach (var lip in LipSyncs)
        {
            if (lip != null)
            {
                lip.enabled = false;
                if (lip.audioSource != null) lip.audioSource.Stop();
            }
        }

        // Cek kecocokan data
        if (Contents.Length != InfoPanels.Length || Contents.Length != LipSyncs.Length || Contents.Length != ExplanationClips.Length)
        {
            Debug.LogWarning("Jumlah Contents, InfoPanels, LipSyncs, dan ExplanationClips harus sama.");
        }

        // Mulai dengan VO Awal
        StartCoroutine(PlayIntroVO());
    }

    private IEnumerator PlayIntroVO()
    {
        if (voAwal != null && lipSyncAwal != null && lipSyncAwal.audioSource != null)
        {
            Debug.Log("▶️ Memutar VO Awal...");
            lipSyncAwal.enabled = true;
            lipSyncAwal.audioSource.clip = voAwal;
            lipSyncAwal.audioSource.Play();

            yield return new WaitWhile(() => lipSyncAwal.audioSource.isPlaying);

            lipSyncAwal.enabled = false;
        }

        ShowContent(0); // Lanjut ke konten pertama
    }

    private void ShowContent(int index)
    {
        for (int i = 0; i < Contents.Length; i++)
        {
            Contents[i].SetActive(i == index);
        }

        currentContentIndex = index;
    }

    public void CheckCompletion()
    {
        GameObject currentContent = Contents[currentContentIndex];
        DropZone1[] zones = currentContent.GetComponentsInChildren<DropZone1>();

        foreach (DropZone1 zone in zones)
        {
            if (zone.transform.childCount == 0)
                return;

            Transform child = zone.transform.GetChild(0);
            DragAndDropUI1 item = child.GetComponent<DragAndDropUI1>();

            if (item == null ||
                item.itemRegion != zone.region ||
                item.targetSlot != zone.slot)
                return;
        }

        Debug.Log("✅ Semua cocok untuk konten " + currentContentIndex);

        // Tampilkan panel info
        if (InfoPanels.Length > currentContentIndex && InfoPanels[currentContentIndex] != null)
            InfoPanels[currentContentIndex].SetActive(true);

        // Aktifkan lip sync dan mainkan audio
        if (LipSyncs.Length > currentContentIndex && LipSyncs[currentContentIndex] != null)
        {
            var lip = LipSyncs[currentContentIndex];
            lip.enabled = true;

            if (lip.audioSource != null && ExplanationClips.Length > currentContentIndex && ExplanationClips[currentContentIndex] != null)
            {
                lip.audioSource.clip = ExplanationClips[currentContentIndex];
                lip.audioSource.Play();
            }

            StartCoroutine(WaitForLipSyncToEnd(lip));
        }
        else
        {
            Invoke("NextContent", 2f); // Jika tidak ada lipsync, tunggu sebentar
        }
    }

    private IEnumerator WaitForLipSyncToEnd(LipSyncCharacter lipSync)
    {
        while (lipSync.audioSource != null && lipSync.audioSource.isPlaying)
        {
            yield return null;
        }

        if (InfoPanels.Length > currentContentIndex && InfoPanels[currentContentIndex] != null)
            InfoPanels[currentContentIndex].SetActive(false);

        if (lipSync != null)
            lipSync.enabled = false;

        NextContent();
    }

    private void NextContent()
    {
        int nextIndex = currentContentIndex + 1;

        if (nextIndex < Contents.Length)
        {
            ShowContent(nextIndex);
        }
        else
        {
            Debug.Log("🎉 Semua konten selesai! Memutar VO akhir...");
            StartCoroutine(PlayOutroVO());
        }
    }

    private IEnumerator PlayOutroVO()
    {
        if (voAkhir != null && lipSyncAkhir != null && lipSyncAkhir.audioSource != null)
        {
            lipSyncAkhir.enabled = true;
            lipSyncAkhir.audioSource.clip = voAkhir;
            lipSyncAkhir.audioSource.Play();

            yield return new WaitWhile(() => lipSyncAkhir.audioSource.isPlaying);

            lipSyncAkhir.enabled = false;
        }

        Debug.Log("✅ VO Akhir selesai. Tambahkan panel akhir atau transisi.");
        // Tambahkan aksi setelah VO akhir, misal: pindah scene, tampilkan panel akhir, dsb.
    }
}
