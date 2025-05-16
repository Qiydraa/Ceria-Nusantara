using UnityEngine;
using System.Collections.Generic;

public class DropZone1 : MonoBehaviour
{
    public static List<DropZone1> AllZones = new List<DropZone1>();

    public string region; // Contoh: "Bali"
    public string slot;   // Contoh: "Head"

    private void OnEnable()
    {
        AllZones.Add(this);
    }

    private void OnDisable()
    {
        AllZones.Remove(this);
    }
}
