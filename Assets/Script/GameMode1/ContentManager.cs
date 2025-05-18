using UnityEngine;

public class ContentManager : MonoBehaviour
{
    [Header("List konten budaya (Canvas sub-panel)")]
    public GameObject[] Contents;

    private int currentContentIndex = 0;

    private void Start()
    {
        ShowContent(0);
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
            {
                return; // Masih ada yang kosong
            }

            Transform child = zone.transform.GetChild(0);
            DragAndDropUI1 item = child.GetComponent<DragAndDropUI1>();

            if (item == null ||
                item.itemRegion != zone.region ||
                item.targetSlot != zone.slot)
            {
                return; // Ada yang salah pasang
            }
        }

        // ✅ Semua cocok
        Debug.Log("✅ Semua cocok untuk konten " + currentContentIndex);

        Invoke("NextContent", 2f); // Lanjut setelah 2 detik
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
            Debug.Log("🎉 Semua konten selesai!");
            // Bisa tambahkan transisi ke akhir game di sini
        }
    }
}
